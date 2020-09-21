

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web;
namespace CMM.Survey
{
	public class SurveyContext
	{
		private static object wr6IGLmNk;
		private string s0aEkT72q;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private int asjR9Bo1P;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Guid iera20Phe;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string ings97FBp;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string ufbW2gPqO;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string itfDW2RLN;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private NameValueCollection Wbi62Z7wI;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.Dictionary<string, object> Nl5HiPlgD;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private ISurveyWriter qCXXxmTCP;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private SurveyFormClass c06SUnQ3L;
		public static SurveyContext Current
		{
			
			get
			{
				SurveyContext surveyContext = HttpContext.Current.Items[SurveyContext.wr6IGLmNk] as SurveyContext;
				if (surveyContext == null)
				{
					surveyContext = new SurveyContext();
					HttpContext.Current.Items[SurveyContext.wr6IGLmNk] = surveyContext;
				}
				return surveyContext;
			}
		}
		public int PageIndex
		{
			
			get;
			
			set;
		}
		public System.Guid FormId
		{
			
			get;
			
			set;
		}
		public string FormName
		{
			
			get;
			
			set;
		}
		public string FormHtml
		{
			
			get;
			
			set;
		}
		public string FormMethod
		{
			
			get;
			
			set;
		}

        public string FormAction
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                string str = string.Empty;
                if (this.QueryString.Count > 0)
                {
                    str = "?";
                    foreach (string str2 in this.QueryString.AllKeys)
                    {
                        string str4 = str;
                        str = str4 + str2 + "=" + HttpUtility.UrlEncode(this.QueryString[str2]) + "&";
                    }
                    str = str.TrimEnd(new char[] { '&' });
                }
                
                return this.s0aEkT72q + str;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                string str;
                this.QueryString.Clear();
                if (value != null)
                {
                    str = value;
                }
                else
                {
                    str = string.Empty;
                }
                int index = str.IndexOf('?');
                if (index == -1)
                {
                    this.s0aEkT72q = value;
                }
                else
                {
                    this.s0aEkT72q = str.Substring(0, index);
                    NameValueCollection c = HttpUtility.ParseQueryString(str.Substring(index + 1));
                    if (c.Count > 0)
                    {
                        this.QueryString.Add(c);
                    }
                }
            }
        }

		public NameValueCollection QueryString
		{
			
			get;
			
			private set;
		}
		public System.Collections.Generic.Dictionary<string, object> Items
		{
			
			get;
			
			private set;
		}
		public ISurveyWriter Writer
		{
			
			get;
			
			set;
		}
		public SurveyFormClass Handler
		{
			
			get;
			
			set;
		}
		
		private SurveyContext()
		{	
			this.FormMethod = "post";
			this.Items = new System.Collections.Generic.Dictionary<string, object>();
			this.QueryString = new NameValueCollection();
		}
		
		static SurveyContext()
		{
			// Note: this type is marked as 'beforefieldinit'.
			
			SurveyContext.wr6IGLmNk = new object();
		}
	}
}
