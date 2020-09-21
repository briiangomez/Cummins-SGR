namespace CMM.Web.Mvc.Html
{
    using CMM;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Linq;

    public static class ValidationExtensions
    {
        private static FieldValidationMetadata ApplyFieldValidationMetadata(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string modelName)
        {           

            FieldValidationMetadata validationMetadataForField = htmlHelper.ViewContext.FormContext.GetValidationMetadataForField(modelName, true);
            foreach (IEnumerable<ModelClientValidationRule> rules in
                (from v in modelMetadata.GetValidators(htmlHelper.ViewContext.Controller.ControllerContext) select v.GetClientValidationRules()))
            {
                foreach (var rule in rules)
                {
                    validationMetadataForField.ValidationRules.Add(rule);
                }
            }
            return validationMetadataForField;
        }

        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string str = null;
            if (!(string.IsNullOrEmpty(System.Web.Mvc.Html.ValidationExtensions.ResourceClassKey) || (httpContext == null)))
            {
                str = httpContext.GetGlobalResourceObject(System.Web.Mvc.Html.ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }
            return (str ?? CMM.SR.System_Web_Mvc_Resources.GetString("Common_ValueNotValidForProperty"));
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!string.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }
            string str = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
            return string.Format(CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), new object[] { str });
        }

        public static IHtmlString ValidationMessage(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, IDictionary<string, object> htmlAttributes)
        {
            htmlAttributes = htmlAttributes ?? new RouteValueDictionary();
            string str = "";
            string modelName = (htmlAttributes["name"] == null) ? modelMetadata.PropertyName : htmlAttributes["name"].ToString();
            if (!string.IsNullOrEmpty(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix))
            {
                modelName = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix + "." + modelName;
            }
            FormContext formContext = null;
            if (htmlHelper.ViewContext.ClientValidationEnabled)
            {
                formContext = htmlHelper.ViewContext.FormContext;
            }
            ModelState modelState = htmlHelper.ViewData.ModelState[modelName];
            ModelErrorCollection errors = (modelState == null) ? null : modelState.Errors;
            ModelError error = ((errors == null) || (errors.Count == 0)) ? null : errors[0];
            if ((error == null) && (formContext == null))
            {
                return null;
            }
            TagBuilder builder = new TagBuilder("span");
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.AddCssClass((error != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);
            if (!string.IsNullOrEmpty(str))
            {
                builder.SetInnerText(str);
            }
            else if (error != null)
            {
                builder.SetInnerText(GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, error, modelState));
            }
            if (formContext != null)
            {
                bool flag = string.IsNullOrEmpty(str);
                FieldValidationMetadata metadata = ApplyFieldValidationMetadata(htmlHelper, modelMetadata, modelName);
                metadata.ReplaceValidationMessageContents = flag;
                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    builder.MergeAttribute("data-valmsg-for", modelName);
                    builder.MergeAttribute("data-valmsg-replace", flag.ToString().ToLowerInvariant());
                }
                else
                {
                    builder.GenerateId(modelName + "_validationMessage");
                    metadata.ValidationMessageId = builder.Attributes["id"];
                }
            }
            return new HtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString ValidationMessage(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, object htmlAttributes)
        {
            return htmlHelper.ValidationMessage(modelMetadata, ((IDictionary<string, object>) new RouteValueDictionary(htmlAttributes)));
        }

        public static IHtmlString ValidationMessageForInput(this HtmlHelper htmlHelper, string inputName, IDictionary<string, object> htmlAttributes)
        {
            string str = "";
            string str2 = inputName;
            if (!htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                throw new CMMException("CMM.Web.Mvc.Html.ValidationExtensions.ValidationMessageForInput only support UnobtrusiveJavaScriptEnabled.");
            }
            ModelState modelState = htmlHelper.ViewData.ModelState[str2];
            ModelErrorCollection errors = (modelState == null) ? null : modelState.Errors;
            ModelError error = ((errors == null) || (errors.Count == 0)) ? null : errors[0];
            TagBuilder builder = new TagBuilder("span");
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.AddCssClass((error != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);
            if (!string.IsNullOrEmpty(str))
            {
                builder.SetInnerText(str);
            }
            else if (error != null)
            {
                builder.SetInnerText(GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, error, modelState));
            }
            bool flag = string.IsNullOrEmpty(str);
            builder.MergeAttribute("data-valmsg-for", str2);
            builder.MergeAttribute("data-valmsg-replace", flag.ToString().ToLowerInvariant());
            return new HtmlString(builder.ToString(TagRenderMode.Normal));
        }
    }
}

