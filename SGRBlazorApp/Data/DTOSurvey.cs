using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace  SGRBlazorApp.Data
{
    public class DTOSurvey
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid UserId { get; set; }
        public List<DTOSurveyItem> SurveyItem { get; set; }
    }
    public class DTOSurveyItem
    {
        public Guid Id { get; set; }
        public string ItemLabel { get; set; }
        public string ItemType { get; set; }
        public string ItemValue { get; set; }
        public int Position { get; set; }
        public int Required { get; set; }
        public int? SurveyChoiceId { get; set; }
        public string AnswerValueString { get; set; }
        public IEnumerable<string> AnswerValueList { get; set; }
        public DateTime? AnswerValueDateTime { get; set; }
        public List<DTOSurveyItemOption> SurveyItemOption { get; set; }
        public List<AnswerResponse> AnswerResponses { get; set; }
    }
    public partial class DTOSurveyItemOption
    {
        public Guid Id { get; set; }
        public string OptionLabel { get; set; }
    }
    public class AnswerResponse
    {
        public string OptionLabel { get; set; }
        public double Responses { get; set; }
    }

    public sealed class Colorss
    {
        private readonly static Colorss _instance = new Colorss();

        public static Colorss Current
        {
            get
            {
                return _instance;
            }
        }

        private Colorss()
        {
            //Implent here the initialization of your singleton
        }

        public List<string> GetAllColors()
        {
            List<Color> allColors = new List<Color>();
            List<string> hexColors = new List<string>();
            foreach (PropertyInfo property in typeof(Color).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    allColors.Add((Color)property.GetValue(null));
                }
            }
            foreach (var color in allColors)
            {
                //var hexString = Int32.Parse(item.Name, NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber);

                //var _bgcolor = Color.FromArgb(hexString);
                if (!color.Name.Contains("Transparent") && !color.Name.Contains("White"))
                {
                    var _bgcolor = ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
                    hexColors.Add(_bgcolor); 
                }
            }

            return hexColors.Where(o => !o.StartsWith("#F")).ToList();
        }
    }
}
