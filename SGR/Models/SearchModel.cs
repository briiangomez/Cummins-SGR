namespace SGR.Communicator.Models
{
    using CMM.Reflection;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;

    public abstract class SearchModel
    {
        protected SearchModel()
        {
        }

        private string ConvertFrom(object value)
        {
            return value.ToString();
        }

        private string ConvertFromArray(Array value)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(',');
                }
                builder.Append(this.ConvertFrom(value.GetValue(i)));
            }
            return builder.ToString();
        }

        private static object ConvertTo(string value, Type type)
        {
            if (type == typeof(Guid))
            {
                return new Guid(value);
            }
            return Convert.ChangeType(value, type);
        }

        private static Array ConvertToArray(string valueStr, Type type)
        {
            string[] strArray = valueStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Type elementType = type.GetElementType();
            Array array = Array.CreateInstance(elementType, strArray.Length);
            for (int i = 0; i < strArray.Length; i++)
            {
                array.SetValue(ConvertTo(strArray[i], elementType), i);
            }
            return array;
        }

        public static T ParseRequest<T>() where T: SearchModel
        {
            T source = Activator.CreateInstance<T>();

            //T source = Activator.CreateInstance<T>();
            //HttpRequest request = HttpContext.Current.Request;
            //DateTime valueDate = new DateTime();
            //foreach (SearchMapping mapping in source.Mappings)
            //{
            //    string valueStr = request.QueryString[mapping.QueryParameterName];
            //    if (valueStr != null)
            //    {
            //        Type propertyType = typeof(T).GetProperty(mapping.PropertyName).PropertyType;
            //        if (propertyType.IsArray)
            //        {
            //            source.SetProperty(mapping.PropertyName, ConvertToArray(valueStr, propertyType));
            //        }
            //        else if (propertyType.IsGenericType && (propertyType.GetGenericTypeDefinition() == typeof(Nullable)))
            //        {
            //            source.SetProperty(mapping.PropertyName, ConvertTo(valueStr, Nullable.GetUnderlyingType(propertyType)));
            //        }
            //        else if(DateTime.TryParse(valueStr,out valueDate))
            //        {
            //            source.SetProperty(mapping.PropertyName, valueDate);
            //        }
            //        else
            //        {
            //            source.SetProperty(mapping.PropertyName, valueStr);
            //        }
            //    }
            //}
            return source;
        }

        public string ToQueryString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (SearchMapping mapping in this.Mappings)
            {
                object property = this.GetType().GetProperty(mapping.PropertyName);
                if (property != null)
                {
                    Type propertyType = base.GetType().GetProperty(mapping.PropertyName).PropertyType;
                    builder.Append("&").Append(mapping.QueryParameterName).Append("=");
                    if (propertyType.IsArray)
                    {
                        builder.Append(this.ConvertFromArray(property as Array));
                    }
                    else
                    {
                        builder.Append(this.ConvertFrom(property));
                    }
                }
            }
            return builder.ToString();
        }

        public abstract IEnumerable<SearchMapping> Mappings { get; }

        public class SearchMapping
        {
            public static IEnumerable<SearchModel.SearchMapping> FromString(string[] strs)
            {
                foreach (string iteratorVariable0 in strs)
                {
                    string[] iteratorVariable1 = iteratorVariable0.Split(new char[] { ',' });
                    SearchModel.SearchMapping iteratorVariable2 = new SearchModel.SearchMapping {
                        QueryParameterName = iteratorVariable1[0],
                        PropertyName = iteratorVariable1[1]
                    };
                    yield return iteratorVariable2;
                }
            }

            public string PropertyName { get; set; }

            public string QueryParameterName { get; set; }

        }
    }
}

