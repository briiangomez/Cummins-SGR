using System.Collections.Generic;

namespace SGR.Controllers
{
    public interface IPropertyProvider
    {
        object GetProperty(string name);
        void SetProperty(string name, object value);
    }

    public class SurveyReportExportModel : IPropertyProvider
    {
        private Dictionary<string, string> collection = new Dictionary<string, string>();

        public SurveyReportExportModel Add(string name, string value)
        {
            this.collection[name] = value;
            return this;
        }

        public object GetProperty(string name)
        {
            if (!this.collection.ContainsKey(name))
            {
                return null;
            }
            return this.collection[name];
        }

        public void SetProperty(string name, object value)
        {
        }
    }
}

