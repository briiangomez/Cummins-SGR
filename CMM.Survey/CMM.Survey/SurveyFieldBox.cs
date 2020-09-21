

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
namespace CMM.Survey
{
	public class SurveyFieldBox : System.Collections.Generic.IEnumerable<ISurveyField>, System.Collections.IEnumerable
	{
		private System.Collections.Generic.List<ISurveyField> wr6IGLmNk;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private int s0aEkT72q;
		public int BreakCount
		{
			
			get;
			
			private set;
		}
		
		public SurveyFieldBox()
		{
			
			
			this.wr6IGLmNk = new System.Collections.Generic.List<ISurveyField>();
		}
		
		public SurveyFieldBox Add(ISurveyField field)
		{
			this.wr6IGLmNk.Add(field);
			if (field is ISurveyBreak)
			{
				this.BreakCount++;
			}
			if (this.Zejy4rWRw(field))
			{
				this.BreakCount++;
			}
			return this;
		}
		
		public void LoopQuestions(System.Action<ISurveyQuestion> action, int? pageIndex = null)
		{
			int num = 0;
			foreach (ISurveyField current in this.wr6IGLmNk)
			{
				if (current is ISurveyBreak)
				{
					num++;
				}
				else
				{
					if (this.Zejy4rWRw(current))
					{
						num++;
					}
					else
					{
						ISurveyQuestion surveyQuestion = current as ISurveyQuestion;
						if (surveyQuestion != null)
						{
							bool arg_BB_0;
							if (pageIndex.HasValue)
							{
								int num2 = num;
								int? num3 = pageIndex;
								arg_BB_0 = (((num2 != num3.GetValueOrDefault()) ? 0 : (num3.HasValue ? 1 : 0)) == 0);
							}
							else
							{
								arg_BB_0 = false;
							}
							if (!arg_BB_0)
							{
								action(surveyQuestion);
							}
						}
					}
				}
			}			
		}
		
		public void Render(System.IO.TextWriter writer, int? pageIndex)
		{
			int num = 0;
			foreach (ISurveyField current in this.wr6IGLmNk)
			{
				if (current is ISurveyBreak)
				{
					num++;
				}
				else
				{
					ISurveyGreeting surveyGreeting = current as ISurveyGreeting;
					if (surveyGreeting != null)
					{
						if (surveyGreeting.ShowGreeting)
						{
							int? num2 = pageIndex;
							if (((num2.GetValueOrDefault() != 0) ? 0 : (num2.HasValue ? 1 : 0)) != 0)
							{
								surveyGreeting.Render(writer);
							}
							num++;
						}
					}
					else
					{
						if (current is ISurveyQuestion)
						{
							bool arg_108_0;
							if (pageIndex.HasValue)
							{
								int num3 = num;
								int? num2 = pageIndex;
								arg_108_0 = (((num3 != num2.GetValueOrDefault()) ? 0 : (num2.HasValue ? 1 : 0)) == 0);
							}
							else
							{
								arg_108_0 = false;
							}
							if (!arg_108_0)
							{
								current.Render(writer);
							}
						}
						else
						{
							if (current is ISurveyThanks)
							{
								int? num2 = pageIndex;
								int num3 = this.BreakCount + 1;
								if (((num2.GetValueOrDefault() != num3) ? 0 : (num2.HasValue ? 1 : 0)) != 0)
								{
									current.Render(writer);
								}
							}
							else
							{
								current.Render(writer);
							}
						}
					}
				}
			}
			
		}
		
		private bool Zejy4rWRw(ISurveyField surveyField)
		{
			ISurveyGreeting surveyGreeting = surveyField as ISurveyGreeting;
			return surveyGreeting != null && surveyGreeting.ShowGreeting;
		}
		
		public System.Collections.Generic.IEnumerator<ISurveyField> GetEnumerator()
		{
			return this.wr6IGLmNk.GetEnumerator();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.wr6IGLmNk.GetEnumerator();
		}
	}
}
