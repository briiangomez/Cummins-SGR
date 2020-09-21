using System;
using System.Data.Objects;
using System.Runtime.CompilerServices;
namespace CMM.Survey.ModelsDb
{
	public class DbSurveyContext : DbSurvey
	{
		private ObjectSet<Survey_Form> g15ynZap8;
		private ObjectSet<Survey_Question> LkxeOZVkQ;
		private ObjectSet<Survey_PostData> x2PI0DgiM;
		private ObjectSet<Survey_Answer> FACGSbJTq;
		private ObjectSet<Survey_Preview> RidRktHXR;
		public ObjectSet<Survey_Form> Survey_Forms
		{
			
			get
			{
				if (this.g15ynZap8 == null)
				{
					this.g15ynZap8 = base.CreateObjectSet<Survey_Form>();
				}
				return this.g15ynZap8;
			}
		}
		public ObjectSet<Survey_Question> Survey_Questions
		{
			
			get
			{
				if (this.LkxeOZVkQ == null)
				{
					this.LkxeOZVkQ = base.CreateObjectSet<Survey_Question>();
				}
				return this.LkxeOZVkQ;
			}
		}
		public ObjectSet<Survey_PostData> Survey_PostDatas
		{
			
			get
			{
				if (this.x2PI0DgiM == null)
				{
					this.x2PI0DgiM = base.CreateObjectSet<Survey_PostData>();
				}
				return this.x2PI0DgiM;
			}
		}
		public ObjectSet<Survey_Answer> Survey_Answers
		{
			
			get
			{
				if (this.FACGSbJTq == null)
				{
					this.FACGSbJTq = base.CreateObjectSet<Survey_Answer>();
				}
				return this.FACGSbJTq;
			}
		}
		public ObjectSet<Survey_Preview> Survey_Previews
		{
			
			get
			{
				if (this.RidRktHXR == null)
				{
					this.RidRktHXR = base.CreateObjectSet<Survey_Preview>();
				}
				return this.RidRktHXR;
			}
		}
		
		public DbSurveyContext()
		{
			
			
		}
		
		public void AddTo_Survey_Form(Survey_Form o)
		{
            base.AddObject("Survey_Form", o);
		}
		
		public void AddTo_Survey_Question(Survey_Question o)
		{
            base.AddObject("Survey_Question", o);
		}
		
		public void AddTo_Survey_PostData(Survey_PostData o)
		{
            base.AddObject("Survey_PostData", o);
		}
		
		public void AddTo_Survey_Answer(Survey_Answer o)
		{
            base.AddObject("Survey_Answer", o);
		}
		
		public void AddTo_Survey_Preview(Survey_Preview o)
		{
            base.AddObject("Survey_Preview", o);
		}
	}
}
