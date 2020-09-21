using CMM.Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace CMM.Survey.ModelsDb
{
	public class DbSurveyService
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass0
		{
			public System.Guid PortalId;
			
			public c__DisplayClass0()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass2
		{
			public System.Guid FormId;
			
			public c__DisplayClass2()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass4
		{
			public System.Guid Id;
			
			public c__DisplayClass4()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass7
		{
			public System.Guid Id;
			
			public c__DisplayClass7()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass9
		{
			public DbSurveyService.c__DisplayClass7 __locals8;
			public Survey_PostData d;
			
			public c__DisplayClass9()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClassc
		{
			public DbSurveyService.c__DisplayClass9 __localsa;
			public DbSurveyService.c__DisplayClass7 __locals8;
			public Survey_Question question;
			
			public c__DisplayClassc()
			{
				
				
			}
			
			public bool GetPostDatab__6(Survey_Answer o)
			{
				return o.QuestionId == this.question.Id;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClasse
		{
			public System.Guid Id;
			
			public c__DisplayClasse()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass10
		{
			public System.Guid id;
			
			public c__DisplayClass10()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass12
		{
			public System.Guid FormId;
			public System.Guid DataId;
			
			public c__DisplayClass12()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass14
		{
			public System.Guid id;
			
			public c__DisplayClass14()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass16
		{
			public DbSurveyService.c__DisplayClass14 __locals15;
			public Survey_Form f;
			
			public c__DisplayClass16()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass18
		{
			public System.Guid Qid;
			
			public c__DisplayClass18()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass1a
		{
			public System.Guid DataId;
			
			public c__DisplayClass1a()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass1c
		{
			public System.Guid FormId;
			
			public c__DisplayClass1c()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass1e
		{
			public System.Guid PortalId;
			
			public c__DisplayClass1e()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass20
		{
			public System.Guid FormId;
			
			public c__DisplayClass20()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass22
		{
			public DbSurveyService.c__DisplayClass20 __locals21;
			public Survey_PostData item;
			
			public c__DisplayClass22()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass24
		{
			public System.Guid FormId;
			
			public c__DisplayClass24()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass26
		{
			public System.Guid PortalId;
			public string name;
			public System.Guid? id;
			
			public c__DisplayClass26()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass28
		{
			public System.Guid? id;
			
			public c__DisplayClass28()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass2c
		{
			public System.Guid? id;
			
			public c__DisplayClass2c()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass2e
		{
			public DbSurveyService.c__DisplayClass2c __locals2d;
			public Survey_Form form;
			
			public c__DisplayClass2e()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass31
		{
			public DbSurveyService.c__DisplayClass2e __locals2f;
			public DbSurveyService.c__DisplayClass2c __locals2d;
			public SurveyQuestion nq;
			
			public c__DisplayClass31()
			{
				
				
			}
			
			public bool SaveHtmlb__2b(Survey_Question o)
			{
				return o.QuestionHtmlId == this.nq.QuestionHtmlId;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass34
		{
			public System.Guid id;
			
			public c__DisplayClass34()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass36
		{
			public System.Guid surveyId;
			public string token;
			
			public c__DisplayClass36()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass3b
		{
			public System.Guid formId;
			public SurveyPost datas;
			
			public c__DisplayClass3b()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass3d
		{
			public DbSurveyService.c__DisplayClass3b __locals3c;
			public Survey_Form form = new Survey_Form();
			
			public c__DisplayClass3d()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass40
		{
			public DbSurveyService.c__DisplayClass3d __locals3e;
			public DbSurveyService.c__DisplayClass3b __locals3c;
			public SurveyQuestion f = new SurveyQuestion();
			
			public c__DisplayClass40()
			{
				
				
			}
			
			public bool SubmitPostDatab__3a(Survey_Question o)
			{
				return o.QuestionHtmlId == this.f.QuestionHtmlId;
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass42
		{
			public System.Guid formId;
			public string password;
			
			public c__DisplayClass42()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass44
		{
			public System.Guid Id;
			
			public c__DisplayClass44()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass46
		{
			public System.Guid formId;
			
			public c__DisplayClass46()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass48
		{
			public System.Guid formId;
			
			public c__DisplayClass48()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass4a
		{
			public System.Guid PortalId;
			
			public c__DisplayClass4a()
			{
				
				
			}
		}
		[System.Runtime.CompilerServices.CompilerGenerated]
		private sealed class c__DisplayClass50
		{
			public System.Guid PortalId;
			
			public c__DisplayClass50()
			{
				
				
			}
		}
		private Func<System.Guid> getPortalId;
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static Func<f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>, SurveyReportItem> BSeIAp6DG;
		
		public DbSurveyService(Func<System.Guid> getPortalId)
		{	
			if (getPortalId == null)
			{
                throw new System.ArgumentNullException("getPortalId can not be empty!");
			}
			this.getPortalId = getPortalId;
		}
		
		public int SurveyCount()
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass0 c__DisplayClass = new DbSurveyService.c__DisplayClass0();
            c__DisplayClass.PortalId = this.getPortalId();
            int result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                IQueryable<Survey_Form> arg_9E_0 = dbSurveyContext.Survey_Forms;
                
                result = arg_9E_0.Count(o => o.PortalId == c__DisplayClass.PortalId);
            }
            return result;
		}
		
		public int PostDataCount(System.Guid FormId)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass2 c__DisplayClass = new DbSurveyService.c__DisplayClass2();
            c__DisplayClass.FormId = FormId;
            int result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_PostData> arg_94_0 = dbSurveyContext.Survey_PostDatas;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6130));
                //result = arg_94_0.Count(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(FormId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                result = dbSurveyContext.Survey_PostDatas.Count(o => o.FormId == FormId);
            }
            return result;
		}
		
		public Survey_Question GetQuestion(System.Guid Id)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass4 c__DisplayClass = new DbSurveyService.c__DisplayClass4();
            //c__DisplayClass.Id = Id;
            Survey_Question result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Question> arg_94_0 = dbSurveyContext.Survey_Questions;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6136));
                //result = arg_94_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Question>();

                result = dbSurveyContext.Survey_Questions.Where(o => o.Id == Id).FirstOrDefault();
            }
            return result;
		}
		
		public Survey_PostData GetPostData(System.Guid Id, bool detail = true)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass7 c__DisplayClass = new DbSurveyService.c__DisplayClass7();
            c__DisplayClass.Id = Id;
            Survey_PostData d;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                DbSurveyService.c__DisplayClass9 c__DisplayClass2 = new DbSurveyService.c__DisplayClass9();
                c__DisplayClass2.__locals8 = c__DisplayClass;
                DbSurveyService.c__DisplayClass9 arg_B3_0 = c__DisplayClass2;

                //IQueryable<Survey_PostData> arg_A9_0 = dbSurveyContext.Survey_PostDatas;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6142));
                //arg_B3_0.d = arg_A9_0.Where(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).First<Survey_PostData>();

                arg_B3_0.d = dbSurveyContext.Survey_PostDatas.Where(o => o.Id == Id).First<Survey_PostData>();

                if (detail)
                {
                    Survey_PostData arg_165_0 = c__DisplayClass2.d;

                    //IQueryable<Survey_Form> arg_15B_0 = dbSurveyContext.Survey_Forms;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6148));
                    //arg_165_0.Survey_Form = arg_15B_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(d))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).First<Survey_Form>();

                    arg_165_0.Survey_Form = dbSurveyContext.Survey_Forms.Where(o => o.Id == c__DisplayClass2.d.FormId).First<Survey_Form>();

                    Survey_PostData arg_1F7_0 = c__DisplayClass2.d;

                    //IQueryable<Survey_Answer> arg_1ED_0 = dbSurveyContext.Survey_Answers;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554485)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6154));
                    //arg_1F7_0.Survey_Answer = arg_1ED_0.Where(Expression.Lambda<Func<Survey_Answer, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDataId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).ToList<Survey_Answer>();

                    arg_1F7_0.Survey_Answer = dbSurveyContext.Survey_Answers.Where(o => o.PostDataId == c__DisplayClass.Id).ToList<Survey_Answer>();

                    Survey_Form arg_2F1_0 = c__DisplayClass2.d.Survey_Form;

                    //IQueryable<Survey_Question> arg_297_0 = dbSurveyContext.Survey_Questions;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6160));
                    //IQueryable<Survey_Question> arg_2E7_0 = arg_297_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(d))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //}));

                    IQueryable<Survey_Question> arg_2E7_0 = dbSurveyContext.Survey_Questions.Where(o => o.FormId == c__DisplayClass2.d.FormId);

                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6166));
                    //arg_2F1_0.Survey_Question = arg_2E7_0.OrderBy(Expression.Lambda<Func<Survey_Question, int>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_QuestionIndex()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).ToList<Survey_Question>();

                    arg_2F1_0.Survey_Question = arg_2E7_0.OrderBy(o => o.QuestionIndex ).ToList<Survey_Question>();


                    using (System.Collections.Generic.List<Survey_Question>.Enumerator enumerator = c__DisplayClass2.d.Survey_Form.Survey_Question.GetEnumerator())
                    {
                        DbSurveyService.c__DisplayClassc c__DisplayClassc = new DbSurveyService.c__DisplayClassc();
                        c__DisplayClassc.__localsa = c__DisplayClass2;
                        c__DisplayClassc.__locals8 = c__DisplayClass;
                        while (enumerator.MoveNext())
                        {
                            c__DisplayClassc.question = enumerator.Current;
                            c__DisplayClassc.question.Survey_Answer = c__DisplayClass2.d.Survey_Answer.Where(new Func<Survey_Answer, bool>(c__DisplayClassc.GetPostDatab__6)).ToList<Survey_Answer>();
                        }
                    }
                }
                d = c__DisplayClass2.d;
            }
            return d;
		}
		
		public Survey_Preview GetPreview(System.Guid Id)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClasse c__DisplayClasse = new DbSurveyService.c__DisplayClasse();
            //c__DisplayClasse.Id = Id;
            Survey_Preview result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Preview> arg_94_0 = dbSurveyContext.Survey_Previews;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554464)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6172));
                //result = arg_94_0.Where(Expression.Lambda<Func<Survey_Preview, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClasse), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Preview>();

                result = dbSurveyContext.Survey_Previews.Where(o => o.Id == Id).FirstOrDefault();
            }
            return result;
		}
		
		public Survey_Form GetSurvey(System.Guid id)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass10 c__DisplayClass = new DbSurveyService.c__DisplayClass10();
            //c__DisplayClass.id = id;
            Survey_Form result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_94_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6178));
                //result = arg_94_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Form>();

                result = dbSurveyContext.Survey_Forms.Where(o => o.Id == id).FirstOrDefault();
            }
            return result;
		}
		
		public bool DeletePostData(System.Guid FormId, System.Guid DataId)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass12 c__DisplayClass = new DbSurveyService.c__DisplayClass12();
            //c__DisplayClass.FormId = FormId;
            //c__DisplayClass.DataId = DataId;
            bool result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_PostData> arg_E8_0 = dbSurveyContext.Survey_PostDatas;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6184));
                //Survey_PostData survey_PostData = arg_E8_0.Where(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(DataId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(FormId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality())))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_PostData>();

                Survey_PostData survey_PostData = dbSurveyContext.Survey_PostDatas.Where(o => o.Id == DataId && o.FormId == FormId).FirstOrDefault<Survey_PostData>();

                if (survey_PostData != null)
                {
                    dbSurveyContext.DeleteObject(survey_PostData);

                    //IQueryable<Survey_Answer> arg_185_0 = dbSurveyContext.Survey_Answers;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554485)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6190));
                    //System.Collections.Generic.List<Survey_Answer> list = arg_185_0.Where(Expression.Lambda<Func<Survey_Answer, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDataId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(DataId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).ToList<Survey_Answer>();

                    System.Collections.Generic.List<Survey_Answer> list = dbSurveyContext.Survey_Answers.Where(o => o.PostDataId == DataId).ToList<Survey_Answer>();

                    foreach (Survey_Answer current in list)
                    {
                        dbSurveyContext.DeleteObject(current);
                    }
                    dbSurveyContext.SaveChanges();
                    result = true;
                    return result;
                }


            }
            result = false;
            return result;
		}
		
		public bool DeleteSurvey(System.Guid id)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass14 c__DisplayClass = new DbSurveyService.c__DisplayClass14();
            c__DisplayClass.id = id;
            bool result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                DbSurveyService.c__DisplayClass16 c__DisplayClass2 = new DbSurveyService.c__DisplayClass16();
                c__DisplayClass2.__locals15 = c__DisplayClass;
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                DbSurveyService.c__DisplayClass16 arg_BC_0 = c__DisplayClass2;

                //IQueryable<Survey_Form> arg_B2_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6196));
                //arg_BC_0.f = arg_B2_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Form>();

                arg_BC_0.f = dbSurveyContext.Survey_Forms.Where(o => o.Id == id).FirstOrDefault<Survey_Form>();

                if (c__DisplayClass2.f != null)
                {
                    //IQueryable<Survey_Question> arg_165_0 = dbSurveyContext.Survey_Questions;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6202));
                    //System.Collections.Generic.List<Survey_Question> list = arg_165_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(f))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).ToList<Survey_Question>();

                    System.Collections.Generic.List<Survey_Question> list = dbSurveyContext.Survey_Questions.Where(o => o.FormId == c__DisplayClass2.f.Id).ToList<Survey_Question>();

                    foreach (Survey_Question current in list)
                    {
                        stringBuilder.AppendFormat(" delete from Survey_Answer where QuestionId='{0}' ", current.Id);
                    }
                    stringBuilder.AppendFormat(" delete from Survey_Question where FormId='{0}' ", c__DisplayClass2.f.Id);
                    stringBuilder.AppendFormat(" delete from Survey_PostData where FormId='{0}' ", c__DisplayClass2.f.Id);
                    stringBuilder.AppendFormat(" delete from Survey_Form where Id='{0}' ", c__DisplayClass2.f.Id);
                    int num = dbSurveyContext.ExecuteStoreCommand(stringBuilder.ToString(), new object[0]);
                    result = (num > 0);
                    return result;
                }
            }
            result = false;
            return result;
		}
		
		public System.Collections.Generic.List<Survey_Answer> AnswerListByQid(System.Guid Qid)
		{
            //INGENIUM

            //DbSurveyService.c__DisplayClass18 c__DisplayClass = new DbSurveyService.c__DisplayClass18();
            //c__DisplayClass.Qid = Qid;
            System.Collections.Generic.List<Survey_Answer> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Answer> arg_97_0 = dbSurveyContext.Survey_Answers;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554485)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6596));
                //IQueryable<Survey_Answer> source = arg_97_0.Where(Expression.Lambda<Func<Survey_Answer, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_QuestionId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Qid))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                result = dbSurveyContext.Survey_Answers.Where(o => o.QuestionId == Qid).ToList<Survey_Answer>();
            }
            return result;

		}
		
		public System.Collections.Generic.List<Survey_Answer> AnswerList(System.Guid DataId)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass1a c__DisplayClass1a = new DbSurveyService.c__DisplayClass1a();
            //c__DisplayClass1a.DataId = DataId;

            System.Collections.Generic.List<Survey_Answer> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Answer> arg_97_0 = dbSurveyContext.Survey_Answers;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554485)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6602));
                //IQueryable<Survey_Answer> source = arg_97_0.Where(Expression.Lambda<Func<Survey_Answer, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDataId()))), Expression.Field(Expression.Constant(c__DisplayClass1a), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(DataId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));
                //result = source.ToList<Survey_Answer>();

                result = dbSurveyContext.Survey_Answers.Where(o => o.PostDataId == DataId).ToList<Survey_Answer>();
            }
            return result;
		}
		
		public System.Collections.Generic.List<Survey_Question> QuestionList(System.Guid FormId)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass1c c__DisplayClass1c = new DbSurveyService.c__DisplayClass1c();
            //c__DisplayClass1c.FormId = FormId;
            System.Collections.Generic.List<Survey_Question> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Question> arg_97_0 = dbSurveyContext.Survey_Questions;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6608));
                //IQueryable<Survey_Question> queryable = arg_97_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass1c), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(FormId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));
                //IQueryable<Survey_Question> arg_E9_0 = queryable;
                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6614));
                //result = arg_E9_0.OrderBy(Expression.Lambda<Func<Survey_Question, int>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_QuestionIndex()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).ToList<Survey_Question>();

                result = dbSurveyContext.Survey_Questions.Where(o => o.FormId == FormId).OrderBy(o => o.QuestionIndex).ToList<Survey_Question>();
            }
            return result;
		}
		
		public System.Collections.Generic.List<Survey_Form> SurveyList(int skip, int take)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass1e c__DisplayClass1e = new DbSurveyService.c__DisplayClass1e();
            c__DisplayClass1e.PortalId = this.getPortalId();
            System.Collections.Generic.List<Survey_Form> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                IQueryable<Survey_Form> arg_A1_0 = dbSurveyContext.Survey_Forms;                
                IQueryable<Survey_Form> arg_F1_0 = arg_A1_0.Where(o => o.PortalId == c__DisplayClass1e.PortalId);                
                IQueryable<Survey_Form> source = arg_F1_0.OrderByDescending(o => o.CreateTime).Skip(skip).Take(take);
                result = source.ToList<Survey_Form>();
            }
            return result;
		}
		
		public System.Collections.Generic.List<Survey_PostData> PostDataReport(System.Guid FormId, bool answers = true)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass20 c__DisplayClass = new DbSurveyService.c__DisplayClass20();
            c__DisplayClass.FormId = FormId;
            System.Collections.Generic.List<Survey_PostData> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_PostData> arg_9A_0 = dbSurveyContext.Survey_PostDatas;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6632));
                //IQueryable<Survey_PostData> queryable = arg_9A_0.Where(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(FormId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                IQueryable<Survey_PostData> queryable = dbSurveyContext.Survey_PostDatas.Where(o => o.FormId == FormId);

                //IQueryable<Survey_PostData> arg_EC_0 = queryable;
                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6638));
                //System.Collections.Generic.List<Survey_PostData> list = arg_EC_0.OrderBy(Expression.Lambda<Func<Survey_PostData, System.DateTime>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDate()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).ToList<Survey_PostData>();

                System.Collections.Generic.List<Survey_PostData> list = queryable.OrderBy(o => o.PostDate).ToList<Survey_PostData>();

                if (answers)
                {
                    using (System.Collections.Generic.List<Survey_PostData>.Enumerator enumerator = list.GetEnumerator())
                    {
                        DbSurveyService.c__DisplayClass22 c__DisplayClass2 = new DbSurveyService.c__DisplayClass22();
                        c__DisplayClass2.__locals21 = c__DisplayClass;
                        while (enumerator.MoveNext())
                        {
                            c__DisplayClass2.item = enumerator.Current;
                            Survey_PostData arg_1CE_0 = c__DisplayClass2.item;

                            //IQueryable<Survey_Answer> arg_1C4_0 = dbSurveyContext.Survey_Answers;
                            //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554485)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6644));
                            //arg_1CE_0.Survey_Answer = arg_1C4_0.Where(Expression.Lambda<Func<Survey_Answer, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDataId()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(item))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                            //{
                            //    parameterExpression
                            //})).ToList<Survey_Answer>();

                            arg_1CE_0.Survey_Answer = dbSurveyContext.Survey_Answers.Where(o => o.PostDataId == c__DisplayClass2.item.Id).ToList<Survey_Answer>();
                        }
                    }
                }
                result = list;
            }
            return result;
		}
		
		public System.Collections.Generic.List<Survey_PostData> PostDataList(System.Guid FormId, int skip, int take)
		{
            //INGENIUM

            //DbSurveyService.c__DisplayClass24 c__DisplayClass = new DbSurveyService.c__DisplayClass24();
            //c__DisplayClass.FormId = FormId;
            System.Collections.Generic.List<Survey_PostData> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_PostData> arg_97_0 = dbSurveyContext.Survey_PostDatas;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6650));
                //IQueryable<Survey_PostData> arg_E7_0 = arg_97_0.Where(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(FormId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));
                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6656));
                //IQueryable<Survey_PostData> source = arg_E7_0.OrderByDescending(Expression.Lambda<Func<Survey_PostData, System.DateTime>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PostDate()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).Skip(skip).Take(take);


                result = dbSurveyContext.Survey_PostDatas.Where(o => o.FormId == FormId).OrderByDescending(o => o.PostDate).Skip(skip).Take(take).ToList<Survey_PostData>();
            }
            return result;
		}
		
		public bool ValidSurveyName(string name, System.Guid? id = null)
		{
            //INGENIUM

            DbSurveyService.c__DisplayClass26 c__DisplayClass = new DbSurveyService.c__DisplayClass26();
            c__DisplayClass.name = name;
            c__DisplayClass.id = id;
            c__DisplayClass.PortalId = getPortalId();
            c__DisplayClass.name = (c__DisplayClass.name ?? string.Empty).ToLower();

            bool result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                if (c__DisplayClass.id.HasValue && c__DisplayClass.id.Value != System.Guid.Empty)
                {
                    //IQueryable<Survey_Form> arg_1C1_0 = dbSurveyContext.Survey_Forms;
                    //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6662));
                    //int num = arg_1C1_0.Count(Expression.Lambda<Func<Survey_Form, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(PortalId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.NotEqual(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Value()), typeof(System.Guid?).TypeHandle)), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Inequality())))), Expression.Equal(Expression.Call(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormName()))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(ToLower())), new Expression[0]), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(name))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality())))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //}));

                    int num = dbSurveyContext.Survey_Forms.Count(o => o.PortalId == c__DisplayClass.PortalId && o.Id == c__DisplayClass.id && o.FormName.ToLower() == c__DisplayClass.name);
                    result = (num == 0);
                }
                else
                {
                    //IQueryable<Survey_Form> arg_2AC_0 = dbSurveyContext.Survey_Forms;
                    //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6668));
                    //int num = arg_2AC_0.Count(Expression.Lambda<Func<Survey_Form, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(PortalId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.Equal(Expression.Call(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormName()))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(ToLower())), new Expression[0]), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(name))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality())))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //}));

                    int num = dbSurveyContext.Survey_Forms.Count(o => o.PortalId == c__DisplayClass.PortalId && o.FormName.ToLower() == c__DisplayClass.name);
                    result = (num == 0);
                }
            }
            return result;
		}
		
		public Survey_Form SaveSurvey(System.Guid? id, string name, string html, System.DateTime? startTime, System.DateTime? endTime, string validatorType, bool? parsed, string joinPassword, bool? respondResult)
		{
            //INGENIUM

            DbSurveyService.c__DisplayClass28 c__DisplayClass = new DbSurveyService.c__DisplayClass28();
            c__DisplayClass.id = id;
            System.Guid portalId = getPortalId();

            Survey_Form result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                Survey_Form survey_Form = null;
                if (c__DisplayClass.id.HasValue && c__DisplayClass.id.Value != System.Guid.Empty)
                {
                    //IQueryable<Survey_Form> arg_F6_0 = dbSurveyContext.Survey_Forms;
                    //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6674));
                    //survey_Form = arg_F6_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Value()), typeof(System.Guid?).TypeHandle)), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).First<Survey_Form>();

                    survey_Form = dbSurveyContext.Survey_Forms.Where(o => o.Id == id).First<Survey_Form>();
                }
                else
                {
                    survey_Form = new Survey_Form();
                    survey_Form.Id = System.Guid.NewGuid();
                    survey_Form.PortalId = portalId;
                    survey_Form.CreateTime = System.DateTime.Now;
                    dbSurveyContext.AddTo_Survey_Form(survey_Form);
                }
                survey_Form.FormName = name;
                survey_Form.StartTime = startTime;
                survey_Form.EndTime = endTime;
                survey_Form.JoinPassword = joinPassword;
                survey_Form.ValidatorType = validatorType;
                if (parsed.HasValue)
                {
                    survey_Form.Paused = parsed;
                }
                if (respondResult.HasValue)
                {
                    survey_Form.RespondResult = respondResult;
                }
                if (html != null)
                {
                    survey_Form.FormHtml = html;
                }
                dbSurveyContext.SaveChanges();
                result = survey_Form;
            }
            return result;
		}
		
		public Survey_Form SaveHtml(System.Guid? id, string html, string name)
		{
            //INGENIUM

            DbSurveyService.c__DisplayClass2c c__DisplayClass2c = new DbSurveyService.c__DisplayClass2c();
            c__DisplayClass2c.id = id;
            System.Guid portalId = getPortalId();
            Survey_Form form;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                DbSurveyService.c__DisplayClass2e c__DisplayClass2e = new DbSurveyService.c__DisplayClass2e();
                c__DisplayClass2e.__locals2d = c__DisplayClass2c;
                c__DisplayClass2e.form = null;
                
                if (c__DisplayClass2c.id.HasValue && c__DisplayClass2c.id.Value != System.Guid.Empty)
                {
                    DbSurveyService.c__DisplayClass2e arg_11D_0 = c__DisplayClass2e;
                    //IQueryable<Survey_Form> arg_113_0 = dbSurveyContext.Survey_Forms;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6680));
                    //arg_11D_0.form = arg_113_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2c), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Value()), typeof(System.Guid?).TypeHandle)), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).First<Survey_Form>();

                    arg_11D_0.form = dbSurveyContext.Survey_Forms.Where(o => o.Id == c__DisplayClass2c.id).First<Survey_Form>();

                    c__DisplayClass2e.form.FormHtml = html;
                    c__DisplayClass2e.form.FormName = name;
                }
                else
                {
                    c__DisplayClass2e.form = new Survey_Form();
                    c__DisplayClass2e.form.Id = System.Guid.NewGuid();
                    c__DisplayClass2e.form.PortalId = portalId;
                    c__DisplayClass2e.form.CreateTime = System.DateTime.Now;
                    c__DisplayClass2e.form.FormHtml = html;
                    c__DisplayClass2e.form.FormName = name;
                    dbSurveyContext.AddTo_Survey_Form(c__DisplayClass2e.form);
                    dbSurveyContext.SaveChanges();
                }

                //IQueryable<Survey_Question> arg_245_0 = dbSurveyContext.Survey_Questions;
                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6686));
                //System.Collections.Generic.List<Survey_Question> source = arg_245_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass2e), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(form))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).ToList<Survey_Question>();

                System.Collections.Generic.List<Survey_Question> source = dbSurveyContext.Survey_Questions.Where(o => o.FormId == c__DisplayClass2e.form.Id).ToList<Survey_Question>();


                SurveyFormClass surveyFormClass = this.ToSurveyForm(c__DisplayClass2e.form);
                System.Collections.Generic.List<SurveyQuestion> questions = surveyFormClass.GetQuestions(-1);
                using (System.Collections.Generic.List<SurveyQuestion>.Enumerator enumerator = questions.GetEnumerator())
                {
                    DbSurveyService.c__DisplayClass31 c__DisplayClass = new DbSurveyService.c__DisplayClass31();
                    c__DisplayClass.__locals2f = c__DisplayClass2e;
                    c__DisplayClass.__locals2d = c__DisplayClass2c;
                    while (enumerator.MoveNext())
                    {
                        c__DisplayClass.nq = enumerator.Current;
                        Survey_Question survey_Question = source.Where(new Func<Survey_Question, bool>(c__DisplayClass.SaveHtmlb__2b)).FirstOrDefault<Survey_Question>();
                        if (survey_Question == null)
                        {
                            dbSurveyContext.AddTo_Survey_Question(new Survey_Question
                            {
                                Id = System.Guid.NewGuid(),
                                FormId = c__DisplayClass2e.form.Id,
                                Survey_Form = c__DisplayClass2e.form,
                                QuestionIndex = c__DisplayClass.nq.QuestionIndex,
                                QuestionText = c__DisplayClass.nq.QuestionText,
                                QuestionHtmlId = c__DisplayClass.nq.QuestionHtmlId,
                                AllowComment = new bool?(c__DisplayClass.nq.AllowComment),
                                IsRequired = new bool?(c__DisplayClass.nq.IsRequired)
                            });
                        }
                        else
                        {
                            survey_Question.AllowComment = new bool?(c__DisplayClass.nq.AllowComment);
                            survey_Question.IsRequired = new bool?(c__DisplayClass.nq.IsRequired);
                            survey_Question.QuestionIndex = c__DisplayClass.nq.QuestionIndex;
                            survey_Question.QuestionText = c__DisplayClass.nq.QuestionText;
                        }
                    }
                }
                dbSurveyContext.SaveChanges();
                form = c__DisplayClass2e.form;
            }
            return form;
		}
		
		public Survey_Preview SavePreview(string name, string html)
		{
			System.Guid portalId = this.getPortalId();
			Survey_Preview result;
			using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
			{
				Survey_Preview survey_Preview = new Survey_Preview
				{
					Id = System.Guid.NewGuid(), 
					PortalId = portalId, 
					CreateDate = System.DateTime.Now, 
					FormHtml = html, 
					FormName = name
				};
				dbSurveyContext.AddTo_Survey_Preview(survey_Preview);
				dbSurveyContext.SaveChanges();
				result = survey_Preview;
			}
			return result;
		}
		
		public string LoadHtml(System.Guid id)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass34 c__DisplayClass = new DbSurveyService.c__DisplayClass34();
            //c__DisplayClass.id = id;
            string result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_94_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6692));
                //IQueryable<Survey_Form> arg_E1_0 = arg_94_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));
                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6698));
                //result = arg_E1_0.Select(Expression.Lambda<Func<Survey_Form, string>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormHtml()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<string>();

                result = dbSurveyContext.Survey_Forms.Where(o => o.Id == id).Select(o => o.FormHtml).FirstOrDefault<string>();
            }
            return result;
            return null;
		}
		
		public bool ExistToken(System.Guid surveyId, string token)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass36 c__DisplayClass = new DbSurveyService.c__DisplayClass36();
            c__DisplayClass.surveyId = surveyId;
            c__DisplayClass.token = token;
            bool result;
            if (string.IsNullOrEmpty(c__DisplayClass.token))
            {
                result = false;
            }
            else
            {
                using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
                {
                    //IQueryable<Survey_PostData> arg_100_0 = dbSurveyContext.Survey_PostDatas;
                    //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6704));
                    //result = (arg_100_0.Count(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(surveyId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_ValidToken()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(token))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality())))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})) > 0);

                    result = (dbSurveyContext.Survey_PostDatas.Count(o => o.FormId == surveyId && o.ValidToken == token) > 0);
                }
            }
            return result;
            //return true;
		}
		
		public SurveyFormClass ToSurveyForm(Survey_Form survey)
		{
			SurveyContext current = SurveyContext.Current;
			current.FormId = survey.Id;
			current.FormName = survey.FormName;
			current.FormHtml = HttpUtility.UrlDecode(survey.FormHtml);
			return new SurveyFormClass(current);
		}
		
		public SurveyFormClass ToSurveyForm(Survey_Preview preview)
		{
			SurveyContext current = SurveyContext.Current;
			current.FormId = preview.Id;
			current.FormName = preview.FormName;
			current.FormHtml = HttpUtility.UrlDecode(preview.FormHtml);
			return new SurveyFormClass(current);
		}
		
		public System.Guid? SubmitPostData(System.Guid formId, SurveyPost datas)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass3b c__DisplayClass3b = new DbSurveyService.c__DisplayClass3b();
            c__DisplayClass3b.formId = formId;
            c__DisplayClass3b.datas = datas;

            System.Guid? result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                DbSurveyService.c__DisplayClass3d c__DisplayClass3d = new DbSurveyService.c__DisplayClass3d();
                c__DisplayClass3d.__locals3c = c__DisplayClass3b;
                DbSurveyService.c__DisplayClass3d form = c__DisplayClass3d;
                //IQueryable<Survey_Form> arg_B4_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6710));
                //arg_BE_0.form = arg_B4_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass3b), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(formId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).First<Survey_Form>();

                form.form = dbSurveyContext.Survey_Forms.Where(o => o.Id == formId).First<Survey_Form>();

                if (c__DisplayClass3d.form.PublishTime.HasValue)
                {
                    //IQueryable<Survey_Question> arg_175_0 = dbSurveyContext.Survey_Questions;
                    //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554531)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6716));
                    //System.Collections.Generic.List<Survey_Question> source = arg_175_0.Where(Expression.Lambda<Func<Survey_Question, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormId()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass3d), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(form))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                    //{
                    //    parameterExpression
                    //})).ToList<Survey_Question>();

                    System.Collections.Generic.List<Survey_Question> source = dbSurveyContext.Survey_Questions.Where(o => o.FormId == formId).ToList<Survey_Question>();

                    using (System.Collections.Generic.List<SurveyQuestion>.Enumerator enumerator = datas.FieldsData.GetEnumerator())
                    {
                        DbSurveyService.c__DisplayClass40 c__DisplayClass = new DbSurveyService.c__DisplayClass40();
                        c__DisplayClass.__locals3e = c__DisplayClass3d;
                        c__DisplayClass.__locals3c = c__DisplayClass3b;

                        //SurveyQuestion surveyQuestion = new SurveyQuestion();

                        while (enumerator.MoveNext())
                        {
                            c__DisplayClass.f = enumerator.Current;
                            Survey_Question survey_Question = source.Where(new Func<Survey_Question, bool>(o => o.QuestionHtmlId == c__DisplayClass.f.QuestionHtmlId)).FirstOrDefault<Survey_Question>();
                            if (survey_Question != null)
                            {
                                survey_Question.AllowComment = new bool?(c__DisplayClass.f.AllowComment);
                                survey_Question.IsRequired = new bool?(c__DisplayClass.f.IsRequired);
                                survey_Question.QuestionIndex = c__DisplayClass.f.QuestionIndex;
                                survey_Question.QuestionText = c__DisplayClass.f.QuestionText;
                                c__DisplayClass.f.QuestionId = survey_Question.Id;
                            }
                            else
                            {
                                Survey_Question survey_Question2 = new Survey_Question
                                {
                                    Id = System.Guid.NewGuid(),
                                    FormId = form.form.Id,
                                    QuestionText = c__DisplayClass.f.QuestionText,
                                    QuestionIndex = c__DisplayClass.f.QuestionIndex,
                                    QuestionHtmlId = c__DisplayClass.f.QuestionHtmlId,
                                    AllowComment = new bool?(c__DisplayClass.f.AllowComment),
                                    IsRequired = new bool?(c__DisplayClass.f.IsRequired)
                                };
                                c__DisplayClass.f.QuestionId = survey_Question2.Id;
                                dbSurveyContext.AddTo_Survey_Question(survey_Question2);
                            }
                        }
                    }
                    bool flag = true;
                    Survey_PostData survey_PostData = null;
                    if (c__DisplayClass3b.datas.DataId != System.Guid.Empty)
                    {
                        //IQueryable<Survey_PostData> arg_3FF_0 = dbSurveyContext.Survey_PostDatas;
                        //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554494)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6722));
                        //survey_PostData = arg_3FF_0.Where(Expression.Lambda<Func<Survey_PostData, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Property(Expression.Field(Expression.Constant(c__DisplayClass3b), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(datas))), (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_DataId()))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                        //{
                        //    parameterExpression
                        //})).FirstOrDefault<Survey_PostData>();

                        survey_PostData = dbSurveyContext.Survey_PostDatas.Where(o => o.Id == datas.DataId).FirstOrDefault<Survey_PostData>();
                    }
                    if (survey_PostData == null)
                    {
                        flag = false;
                        survey_PostData = new Survey_PostData();
                        survey_PostData.Id = System.Guid.NewGuid();
                        survey_PostData.FormId = c__DisplayClass3d.form.Id;
                        survey_PostData.Survey_Form = c__DisplayClass3d.form;                        
                    }
                    survey_PostData.PostDate = System.DateTime.Now;
                    survey_PostData.ContactId = c__DisplayClass3b.datas.ContactId;
                    survey_PostData.ClientIP = c__DisplayClass3b.datas.ClientIP;
                    survey_PostData.ClientBrowser = c__DisplayClass3b.datas.ClientBrowser;
                    survey_PostData.ClientPlatform = c__DisplayClass3b.datas.ClientPlatform;
                    survey_PostData.ValidToken = c__DisplayClass3b.datas.ValidToken;
                    survey_PostData.SourceType = new int?((int)c__DisplayClass3b.datas.SourceType);
                    dbSurveyContext.AddTo_Survey_PostData(survey_PostData);
                    
                    if (c__DisplayClass3b.datas.DataId != System.Guid.Empty && flag)
                    {
                        string commandText = string.Format("delete from Survey_Answer where PostDataId='{0}' ", survey_PostData.Id);
                        dbSurveyContext.ExecuteStoreCommand(commandText, new object[0]);
                    }
                    foreach (SurveyQuestion current in c__DisplayClass3b.datas.FieldsData)
                    {
                        foreach (SurveyAnswer current2 in current.Answers)
                        {
                            
                            dbSurveyContext.AddTo_Survey_Answer(new Survey_Answer
                            {
                                Id = CMM.Data.SequentialGuid.NewSqlCompatibleGuid(),
                                PostDataId = survey_PostData.Id,
                                QuestionId = current.QuestionId,
                                AnswerType = new int?((int)current2.AnswerType),
                                AnswerText = current2.AnswerText,
                                CommentText = current2.CommentText,
                                AnswerHtmlId = current2.AnswerHtmlId
                            });
                        }
                    }
                    dbSurveyContext.SaveChanges();
                    result = new System.Guid?(survey_PostData.Id);
                }
                else
                {
                    result = null;
                }
            }
            return result;            
		}
		
		public bool ValidJoinPassword(System.Guid formId, string password)
		{
            //INGENIUM
            //DbSurveyService.c__DisplayClass42 c__DisplayClass = new DbSurveyService.c__DisplayClass42();
            //c__DisplayClass.formId = formId;
            //c__DisplayClass.password = password;
            bool result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                IQueryable<Survey_Form> arg_121_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6830));
                
                //int num = arg_121_0.Count(Expression.Lambda<Func<Survey_Form, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(formId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.OrElse(Expression.Call(null, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(IsNullOrEmpty())), new Expression[]
                //{
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_JoinPassword())))
                //}), Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_JoinPassword()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(password))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                int num = arg_121_0.Count(o => o.Id == formId && o.JoinPassword == password);
                result = (num == 1);
            }
            return result;
		}
		
		public void PublishForm(System.Guid Id, bool publish)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass44 c__DisplayClass = new DbSurveyService.c__DisplayClass44();
            c__DisplayClass.Id = Id;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                IQueryable<Survey_Form> arg_A8_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6836));
                
                //Survey_Form survey_Form = arg_A8_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(Id))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Form>();

                Survey_Form survey_Form = arg_A8_0.Where(o => o.Id == Id).FirstOrDefault();

                if (survey_Form != null)
                {
                    survey_Form.PublishTime = (publish ? new System.DateTime?(System.DateTime.Now) : null);
                    dbSurveyContext.SaveChanges();
                }
            }		
    
        }
		
		public void PausedSurvey(System.Guid formId, bool parsed)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass46 c__DisplayClass = new DbSurveyService.c__DisplayClass46();
            c__DisplayClass.formId = formId;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_A3_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6842));

                //Survey_Form survey_Form = arg_A3_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(formId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Form>();

                Survey_Form survey_Form = dbSurveyContext.Survey_Forms.Where(o => o.Id == formId).FirstOrDefault();

                if (survey_Form != null)
                {
                    survey_Form.Paused = new bool?(parsed);
                    dbSurveyContext.SaveChanges();
                }
            }			
		}
		
		public void IncreaseVisitCount(System.Guid formId)
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass48 c__DisplayClass = new DbSurveyService.c__DisplayClass48();
            c__DisplayClass.formId = formId;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_A3_0 = dbSurveyContext.Survey_Forms;
                
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6848));
                //Survey_Form survey_Form = arg_A3_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(formId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).FirstOrDefault<Survey_Form>();

                Survey_Form survey_Form = dbSurveyContext.Survey_Forms.Where(o => o.Id == formId).FirstOrDefault();

                if (survey_Form != null)
                {
                    survey_Form.VisitCount++;
                    dbSurveyContext.SaveChanges();
                }
            }			
		}
		[System.Obsolete]
		
		public int ReportCount()
		{
            //INGENIUM
            DbSurveyService.c__DisplayClass4a c__DisplayClass4a = new DbSurveyService.c__DisplayClass4a();
            c__DisplayClass4a.PortalId = this.getPortalId();
            int result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_FA_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6854));
                
                //result = arg_FA_0.Count(Expression.Lambda<Func<Survey_Form, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()))), Expression.Field(Expression.Constant(c__DisplayClass4a), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(PortalId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.NotEqual(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PublishTime()))), Expression.Convert(Expression.Constant(null, typeof(System.DateTime?)), typeof(System.DateTime?)), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Inequality())))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                result = dbSurveyContext.Survey_Forms.Count(o => o.PortalId == this.getPortalId() && o.PublishTime != DateTime.MaxValue);
            }
            return result;
            //return 0;
		}
		[System.Obsolete]
		
		public System.Collections.Generic.List<SurveyReportItem> ReportList(int skip, int take)
		{
            //INGENIUM

            //DbSurveyService.c__DisplayClass50 c__DisplayClass = new DbSurveyService.c__DisplayClass50();
            //c__DisplayClass.PortalId = this.rrfeN1x5h();

            System.Collections.Generic.List<SurveyReportItem> result;
            using (DbSurveyContext dbSurveyContext = new DbSurveyContext())
            {
                //IQueryable<Survey_Form> arg_FE_0 = dbSurveyContext.Survey_Forms;
                //ParameterExpression parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6860));
                //IQueryable<Survey_Form> queryable = arg_FE_0.Where(Expression.Lambda<Func<Survey_Form, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()))), Expression.Field(Expression.Constant(c__DisplayClass), System.Reflection.FieldInfo.GetFieldFromHandle(ldtoken(PortalId))), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Equality()))), Expression.NotEqual(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PublishTime()))), Expression.Convert(Expression.Constant(null, typeof(System.DateTime?)), typeof(System.DateTime?)), false, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(op_Inequality())))), new ParameterExpression[]
                //{
                //    parameterExpression
                //}));

                IQueryable<Survey_Form> query = dbSurveyContext.Survey_Forms.Where(o => o.PortalId == getPortalId() && o.PublishTime != DateTime.MaxValue).OrderByDescending(o => o.CreateTime);

                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6866));
                //queryable = arg_150_0.OrderByDescending(Expression.Lambda<Func<Survey_Form, System.DateTime>>(Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_CreateTime()))), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).Skip(skip).Take(take);

                //IQueryable<Survey_Form> arg_3E0_0 = arg_150_0.Skip(skip).Take(take);

                //parameterExpression = Expression.Parameter(System.Type.GetTypeFromHandle(Kh5pL1XovugZTlf6LN.Do0q3LZsPSvOZ(33554482)), SmJxwdRJ08AsNYTLqT.ingebBwfm(6872));

                //var list = arg_3E0_0.Select(Expression.Lambda(Expression.New((System.Reflection.ConstructorInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(.ctor()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), new Expression[]
                //{
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormName()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_CreateTime()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PublishTime()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_StartTime()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_EndTime()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_SendEmail()))), 
                //    Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_VisitCount()))), 
                //    Expression.Call(null, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(Count())), new Expression[]
                //    {
                //        Expression.Property(parameterExpression, (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Survey_PostData())))
                //    })
                //}, new System.Reflection.MethodInfo[]
                //{
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_Id()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FormName()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_CreateTime()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PublishTime()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_PortalId()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_FromTime()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_ToTime()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_SendEmail()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_VisitCount()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle), 
                //    (System.Reflection.MethodInfo)System.Reflection.MethodBase.GetMethodFromHandle(ldtoken(get_JoinCount()), typeof(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>).TypeHandle)
                //}), new ParameterExpression[]
                //{
                //    parameterExpression
                //})).ToList();

                //var arg_411_0 = list;
                //if (DbSurveyService.BSeIAp6DG == null)
                //{
                //    DbSurveyService.BSeIAp6DG = new Func<f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int>, SurveyReportItem>(DbSurveyService.YlFylqmeg);
                //}

                //result = arg_411_0.Select(DbSurveyService.BSeIAp6DG).ToList<SurveyReportItem>();

                //var list = arg_3E0_0.Select(o => o.CreateTime).ToList();

                result = query.Skip(skip).Take(take).Select(o => new SurveyReportItem
                { 
                    JoinCount = o.Survey_PostData.Count(),
                    Survey = new Survey_Form
			                    {
				                    Id = o.Id, 
				                    FormName = o.FormName, 
				                    CreateTime = o.CreateTime, 
				                    PublishTime = o.PublishTime, 
				                    PortalId = o.PortalId, 
				                    EndTime = o.EndTime, 
				                    SendEmail = o.SendEmail, 
				                    VisitCount = o.VisitCount, 
				                    StartTime = o.StartTime
			                    },
                                                                         
                }).ToList<SurveyReportItem>();

            }
            return result;
            //return null;
		}
		
        //private static SurveyReportItem YlFylqmeg(f__AnonymousType0<System.Guid, string, System.DateTime, System.DateTime?, System.Guid, System.DateTime?, System.DateTime?, bool?, int, int> o)
        //{
        //    SurveyReportItem surveyReportItem;
			
        //    surveyReportItem = new SurveyReportItem();
        //    surveyReportItem.JoinCount = o.JoinCount;
        //    surveyReportItem.Survey = new Survey_Form
        //    {
        //        Id = o.Id, 
        //        FormName = o.FormName, 
        //        CreateTime = o.CreateTime, 
        //        PublishTime = o.PublishTime, 
        //        PortalId = o.PortalId, 
        //        EndTime = o.ToTime, 
        //        SendEmail = o.SendEmail, 
        //        VisitCount = o.VisitCount, 
        //        StartTime = o.FromTime
        //    };
			
        //    return surveyReportItem;
        //}
	}
        
    internal sealed class f__AnonymousType0<Idj__TPar, FormNamej__TPar, CreateTimej__TPar, PublishTimej__TPar, PortalIdj__TPar, FromTimej__TPar, ToTimej__TPar, SendEmailj__TPar, VisitCountj__TPar, JoinCountj__TPar>
    {
        // Fields
        
        private readonly CreateTimej__TPar CreateTimei__Field;
        
        private readonly FormNamej__TPar FormNamei__Field;
        
        private readonly FromTimej__TPar FromTimei__Field;
        
        private readonly Idj__TPar Idi__Field;
        
        private readonly JoinCountj__TPar JoinCounti__Field;
        
        private readonly PortalIdj__TPar PortalIdi__Field;
        
        private readonly PublishTimej__TPar PublishTimei__Field;
        
        private readonly SendEmailj__TPar SendEmaili__Field;
        
        private readonly ToTimej__TPar ToTimei__Field;
        
        private readonly VisitCountj__TPar VisitCounti__Field;

        // Methods
        
        public f__AnonymousType0(Idj__TPar Id, FormNamej__TPar FormName, CreateTimej__TPar CreateTime, PublishTimej__TPar PublishTime, PortalIdj__TPar PortalId,FromTimej__TPar FromTime, ToTimej__TPar ToTime, SendEmailj__TPar SendEmail, VisitCountj__TPar VisitCount, JoinCountj__TPar JoinCount)
        {            
            this.Idi__Field = Id;
            this.FormNamei__Field = FormName;
            this.CreateTimei__Field = CreateTime;
            this.PublishTimei__Field = PublishTime;
            this.PortalIdi__Field = PortalId;
            this.FromTimei__Field = FromTime;
            this.ToTimei__Field = ToTime;
            this.SendEmaili__Field = SendEmail;
            this.VisitCounti__Field = VisitCount;
            this.JoinCounti__Field = JoinCount;
        }

        
        public override bool Equals(object value)
        {           
            var type = value as f__AnonymousType0<Idj__TPar, FormNamej__TPar, CreateTimej__TPar, PublishTimej__TPar, PortalIdj__TPar, FromTimej__TPar, ToTimej__TPar, SendEmailj__TPar, VisitCountj__TPar, JoinCountj__TPar>;
            if ((((type != null) && EqualityComparer<Idj__TPar>.Default.Equals(this.Idi__Field, type.Idi__Field)) && (EqualityComparer<FormNamej__TPar>.Default.Equals(this.FormNamei__Field, type.FormNamei__Field) && EqualityComparer<CreateTimej__TPar>.Default.Equals(this.CreateTimei__Field, type.CreateTimei__Field))) && EqualityComparer<PublishTimej__TPar>.Default.Equals(this.PublishTimei__Field, type.PublishTimei__Field))
            {
            }
            
            return ((((EqualityComparer<PortalIdj__TPar>.Default.Equals(this.PortalIdi__Field, type.PortalIdi__Field) && EqualityComparer<FromTimej__TPar>.Default.Equals(this.FromTimei__Field, type.FromTimei__Field)) && (EqualityComparer<ToTimej__TPar>.Default.Equals(this.ToTimei__Field, type.ToTimei__Field) && EqualityComparer<SendEmailj__TPar>.Default.Equals(this.SendEmaili__Field, type.SendEmaili__Field))) && EqualityComparer<VisitCountj__TPar>.Default.Equals(this.VisitCounti__Field, type.VisitCounti__Field)) && EqualityComparer<JoinCountj__TPar>.Default.Equals(this.JoinCounti__Field, type.JoinCounti__Field));
        }

        
        public override int GetHashCode()
        {
            int num;
            
            num = 0xf776b27;
            num = (-1521134295 * num) + EqualityComparer<Idj__TPar>.Default.GetHashCode(this.Idi__Field);
            num = (-1521134295 * num) + EqualityComparer<FormNamej__TPar>.Default.GetHashCode(this.FormNamei__Field);
            num = (-1521134295 * num) + EqualityComparer<CreateTimej__TPar>.Default.GetHashCode(this.CreateTimei__Field);
            num = (-1521134295 * num) + EqualityComparer<PublishTimej__TPar>.Default.GetHashCode(this.PublishTimei__Field);
            num = (-1521134295 * num) + EqualityComparer<PortalIdj__TPar>.Default.GetHashCode(this.PortalIdi__Field);
            num = (-1521134295 * num) + EqualityComparer<FromTimej__TPar>.Default.GetHashCode(this.FromTimei__Field);
            
            num = (-1521134295 * num) + EqualityComparer<ToTimej__TPar>.Default.GetHashCode(this.ToTimei__Field);
            num = (-1521134295 * num) + EqualityComparer<SendEmailj__TPar>.Default.GetHashCode(this.SendEmaili__Field);
            num = (-1521134295 * num) + EqualityComparer<VisitCountj__TPar>.Default.GetHashCode(this.VisitCounti__Field);
            return ((-1521134295 * num) + EqualityComparer<JoinCountj__TPar>.Default.GetHashCode(this.JoinCounti__Field));
        }

        
        public override string ToString()
        {
            StringBuilder builder;
            
            builder = new StringBuilder();
            builder.Append("{ Id = ");
            builder.Append(this.Idi__Field);
            builder.Append(", FormName = ");
            builder.Append(this.FormNamei__Field);
            builder.Append(", CreateTime = ");
            builder.Append(this.CreateTimei__Field);
            builder.Append(", PublishTime = ");
            builder.Append(this.PublishTimei__Field);
            builder.Append(", PortalId = ");
            builder.Append(this.PortalIdi__Field);
            builder.Append(", FromTime = ");
            
            builder.Append(this.FromTimei__Field);
            builder.Append(", ToTime = ");
            builder.Append(this.ToTimei__Field);
            builder.Append(", SendEmail = ");
            builder.Append(this.SendEmaili__Field);
            builder.Append(", VisitCount = ");
            builder.Append(this.VisitCounti__Field);
            builder.Append(", JoinCount = ");
            builder.Append(this.JoinCounti__Field);
            builder.Append(" }");
            return builder.ToString();
        }

        // Properties
        public CreateTimej__TPar CreateTime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.CreateTimei__Field;
            }
        }

        public FormNamej__TPar FormName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.FormNamei__Field;
            }
        }

        public FromTimej__TPar FromTime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.FromTimei__Field;
            }
        }

        public Idj__TPar Id
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.Idi__Field;
            }
        }

        public JoinCountj__TPar JoinCount
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.JoinCounti__Field;
            }
        }

        public PortalIdj__TPar PortalId
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.PortalIdi__Field;
            }
        }

        public PublishTimej__TPar PublishTime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.PublishTimei__Field;
            }
        }

        public SendEmailj__TPar SendEmail
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.SendEmaili__Field;
            }
        }

        public ToTimej__TPar ToTime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ToTimei__Field;
            }
        }

        public VisitCountj__TPar VisitCount
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.VisitCounti__Field;
            }
        }
    }
}
