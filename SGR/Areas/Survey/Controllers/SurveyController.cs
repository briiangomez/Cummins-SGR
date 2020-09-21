using CMM.Globalization;
using CMM.Survey;
using CMM.Survey.SurveyFields;
using CMM.Survey.SurveyWriters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using CMM.Survey.ModelsDb;
using CMM.Survey.Models;
using SGR.Communicator;
using SGR.Communicator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SGR.Controllers
{
    [Area("Survey")]
    public class SurveyController : CMMController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;           

        private DbSurveyService _surveyService;
        public SurveyController(IWebHostEnvironment webHostEnvironment)
        {
            base.Menu(new string[] { "Survey" });
            _webHostEnvironment = webHostEnvironment;
        }

        [AccessFilter("Survey")]
        public ActionResult Builder(Guid? Id, bool? IsNew)
        {
            if (Id.HasValue && (Id.Value != Guid.Empty))
            {
                Survey_Form survey = SurveyService.GetSurvey(Id.Value);
                var model = new SurveyInfoViewModel
                {
                    SurveyId = survey.Id,
                    ValidatorType = survey.ValidatorType,
                    Published = survey.PublishTime.HasValue,
                    Paused = survey.Paused,
                    JoinPassword = survey.JoinPassword,
                    RespondResult = survey.RespondResult == true,
                    StartTime = survey.StartTime,
                    EndTime = survey.EndTime,
                    SurveyName = survey.FormName,
                    IsNew = IsNew == true
                };
                return base.View(model);
            }
            SurveyInfoViewModel model2 = new SurveyInfoViewModel
            {
                IsNew = IsNew == true
            };
            return base.View(model2);
        }

        [AccessFilter("Survey"), HttpPost]
        public ActionResult Builder(Guid? SurveyId, string SurveyHtml, string SurveyName, bool? IsNew)
        {
            if (IsNew == true)
            {
                SurveyId = null;
            }
            SurveyName = HttpUtility.UrlDecode(SurveyName);
            Survey_Form form = SurveyService.SaveHtml(SurveyId, SurveyHtml, SurveyName);
            SurveyId = new Guid?(form.Id);
            base.Info("La encuesta se ha guardado".Localize(""), 5);
            if (!string.IsNullOrEmpty(base.Request.Form["SubmitPrevious"]))
            {
                return base.RedirectToAction("Detail", new { Id = SurveyId, IsNew = IsNew });
            }
            if (!string.IsNullOrEmpty(base.Request.Form["SubmitNext"]))
            {
                return base.RedirectToAction("Publish", new { Id = SurveyId, IsNew = IsNew });
            }
            return base.RedirectToAction("Builder", new { Id = SurveyId });
        }

        [AccessFilter("Survey")]
        public ActionResult BuilderInner(Guid Id, bool? IsNew)
        {
            base.ViewData["IsNew"] = IsNew;
            base.ViewData["Surveyid"] = Id;
            return base.View();
        }

        private void ClearCache(Guid Id)
        {
            string name = Id + "_postdata";
            //base.Session.Remove(name);
            HttpContext.Session.Remove(name);
        }

        [AccessFilter("Survey")]
        public ActionResult Copy(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return base.RedirectToAction("Index");
            }
            Survey_Form survey = SurveyService.GetSurvey(Id);
            return base.RedirectToAction("Detail", new { Id = Id, IsCopy = true });
        }

        [AccessFilter("Survey")]
        public ActionResult Data(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            base.ViewData["SurveyId"] = survey.Id;
            base.ViewData["SurveyName"] = survey.FormName;
            base.Total = SurveyService.PostDataCount(Id);
            List<Survey_PostData> list = SurveyService.PostDataList(Id, base.PagingParameters.Start, base.PagingParameters.Count);
            ListModel<Survey_PostData> model = new ListModel<Survey_PostData>
            {
                Items = list
            };
            return base.View(model);
        }

        [AccessFilter("Survey")]
        public ActionResult DataDetail(Guid Id)
        {
            Survey_PostData postData = SurveyService.GetPostData(Id, true);
            return base.View(postData);
        }

        [HttpGet, AccessFilter("Survey")]
        public ActionResult DataReview(Guid FormId, Guid DataId, int? PageIndex)
        {
            Survey_Form survey = SurveyService.GetSurvey(FormId);
            int pageIndex = PageIndex.HasValue ? PageIndex.Value : 0;
            SurveyContext context = PrepareSurveyContext(survey, pageIndex);
            context.Writer = new GetWriter();
            context.FormAction = base.Url.Action("DataReview", new { FormId = FormId, DataId = DataId });
            List<Survey_Answer> source = SurveyService.AnswerList(DataId);
            List<SurveyQuestion> datas = (from o in SurveyService.QuestionList(FormId) select new SurveyQuestion { QuestionId = o.Id, QuestionText = o.QuestionText, QuestionIndex = o.QuestionIndex, QuestionHtmlId = o.QuestionHtmlId, AllowComment = o.AllowComment.HasValue ? o.AllowComment.Value : false, IsRequired = o.IsRequired.HasValue ? o.IsRequired.Value : false }).ToList<SurveyQuestion>();
            using (List<SurveyQuestion>.Enumerator enumerator = datas.GetEnumerator())
            {
                Func<Survey_Answer, bool> predicate = null;
                SurveyQuestion q;
                while (enumerator.MoveNext())
                {
                    q = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = o => o.QuestionId == q.QuestionId;
                    }
                    List<Survey_Answer> list3 = source.Where<Survey_Answer>(predicate).ToList<Survey_Answer>();
                    q.Answers.AddRange((from o in list3 select new SurveyAnswer { AnswerType = o.AnswerTypeEnum, AnswerText = o.AnswerText, CommentText = o.CommentText, AnswerHtmlId = o.AnswerHtmlId }).ToList<SurveyAnswer>());
                }
            }
            RuntimeKit.GenerateRuntimeCss();
            return base.View(ProcessSurvey(context, datas));
        }

        [AccessFilter("Survey")]
        public ActionResult DeleteData(Guid FormId, Guid DataId)
        {
            SurveyService.DeletePostData(FormId, DataId);
            return base.RedirectToAction("Data", new { Id = FormId });
        }

        [AccessFilter("Survey")]
        public ActionResult Deletion(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            base.ViewData["SurveyId"] = survey.Id;
            base.ViewData["SurveyName"] = survey.FormName;
            return base.View();
        }

        [AccessFilter("Survey"), HttpPost]
        public ActionResult Deletion(Guid Id, bool? Sure)
        {
            if (!(!Sure.HasValue ? true : !Sure.Value))
            {
                SurveyService.DeleteSurvey(Id);
                base.Info("La encuesta se eliminó correctamente".Localize(""), 5);
                return base.RedirectToAction("Index");
            }
            return base.RedirectToAction("Deletion", new { Id = Id });
        }

        [AccessFilter("Survey"), HttpPost]
        public ActionResult Detail(SurveyInfoViewModel model, Guid? TemplateId, bool? IsCopy)
        {
            DateTime? nullable;
            DateTime? nullable2;
            if ((model.EndTime.HasValue && model.StartTime.HasValue) && (((nullable = model.EndTime).HasValue & (nullable2 = model.StartTime).HasValue) ? (nullable.GetValueOrDefault() <= nullable2.GetValueOrDefault()) : false))
            {
                base.ModelState.AddModelError("StartTime", "La hora de inicio debe ser anterior a la hora de finalización.".Localize(""));
            }
            model.SurveyName = (model.SurveyName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(model.SurveyName))
            {
                base.ModelState.AddModelError("SurveyName", "El nombre de la encuesta es requerido".Localize(""));
            }
            else if (model.SurveyName.Length > 250)
            {
                base.ModelState.AddModelError("SurveyName", "La longitud máxima del nombre de la encuesta es de 250 caracteres".Localize(""));
            }
            Guid guid = (IsCopy == true) ? Guid.Empty : model.SurveyId;
            if (!SurveyService.ValidSurveyName(model.SurveyName, new Guid?(guid)))
            {
                base.ModelState.AddModelError("SurveyName", "El nombre de la encuesta ya existe".Localize(""));
            }
            if (base.ModelState.IsValid)
            {
                string html = null;
                if (IsCopy == true)
                {
                    html = SurveyService.LoadHtml(model.SurveyId);
                }
                else if (TemplateId.HasValue && (TemplateId.Value != Guid.Empty))
                {
                    html = string.Empty;
                }
                Survey_Form form = SurveyService.SaveSurvey(new Guid?(guid), model.SurveyName, html, model.StartTime, model.EndTime, model.ValidatorType, model.Paused, model.JoinPassword, new bool?(model.RespondResult));
                base.Info("La información de la encuesta se ha guardado".Localize(""), 5);
                model.SurveyId = form.Id;
                IsCopy = false;
                if (!string.IsNullOrEmpty(base.Request.Form["SubmitNext"]))
                {
                    return base.RedirectToAction("Builder", new { Id = form.Id, IsNew = model.IsNew });
                }
            }
            base.ViewData["IsCopy"] = IsCopy;
            return base.View(model);
        }

        [AccessFilter("Survey")]
        public ActionResult Detail(Guid? Id, bool? IsCopy, bool? IsNew)
        {
            bool? nullable = IsNew;
            return base.RedirectToAction("Builder", new { Id = Id, IsNew = (!nullable.GetValueOrDefault() ? false : nullable.HasValue) ? true : (!(nullable = IsCopy).GetValueOrDefault() ? false : nullable.HasValue) });
        }

        [AccessFilter("Survey")]
        public ActionResult Distribute(Guid Id)
        {
            var survey = SurveyService.GetSurvey(Id);
            if (!survey.PublishTime.HasValue)
            {
                return base.RedirectToAction("Publish", new { Id = Id });
            }
            SurveyFormClass class2 = SurveyService.ToSurveyForm(survey);
            var writer = new WholeWriter
            {
                VirtualPath = base.Url.Content("~")
            };

            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
       
            new List<string> { Path.Combine(webRootPath, "~/Scripts/survey/runtime/index.css"), Path.Combine(webRootPath, "~/Scripts/survey/runtime/field.css") }.ForEach(
                path => writer.StyleResources.Add(new FileInfo(path)));

            //new List<string> { base.Server.MapPath("~/Scripts/survey/runtime/index.js") }.ForEach(delegate (string path) {
            //    writer.ScriptResources.Add(new FileInfo(path));
            //});
            new List<string> { Path.Combine(webRootPath, "~/Scripts/survey/runtime/index.js") }.ForEach(
                path => writer.ScriptResources.Add(new FileInfo(path)));

            class2.Context.Writer = writer;
            class2.Context.FormAction = RuntimeKit.AbsoluteAction(base.Url.Action("Form", new { s = 3, Id = Id }));
            base.ViewData["SurveyHtml"] = class2.Render();
            var str = RuntimeKit.AbsoluteAction(base.Url.Action("Form", new { s = 2, Id = Id }));
            base.ViewData["IframeHtml"] = "<iframe width=\"780\" height=\"100%\" frameborder=\"0\" style=\"overflow: auto\" src=\"" + str + "\"></iframe>";
            base.ViewData["NormalUrl"] = RuntimeKit.AbsoluteAction(base.Url.Action("Form", new { s = 1, Id = Id }));
            base.ViewData["SurveyName"] = survey.FormName;
            return base.View();
        }

        public enum ValidatorType
        {
            Cookie,
            Email
        }

        private void EndValidate(Survey_Form form)
        {
            ValidatorType type;
            if (Enum.TryParse<ValidatorType>(form.ValidatorType, true, out type))
            {
                if (type == ValidatorType.Cookie)
                {
                     //.SetString(Key, JsonConvert.SerializeObject(dictionary));

                    string str = HttpContext.Session.GetString("__verfyCookie");
                    HttpContext.Session.Remove("__verfyCookie");

                    //string cookie = HttpContext.Request.Cookies["CMM_survey"];

                    //if (cookie == null)
                    //{
                        
                    //    cookie = new HttpContext.Co HttpCookie("CMM_survey");
                    //    HttpContext.
                    //}

                    //cookie.Values[form.Id.ToString()] = str;

                    base.Response.Cookies.Delete("CMM_survey");                    
                    base.Response.Cookies.Append(form.Id.ToString(), str);
                }
                else if (type == ValidatorType.Email)
                {
                    HttpContext.Session.Remove("__verfyEmail");
                }
                //else if (type == ValidatorType.IP)
                //{
                //}
            }
        }

        [AccessFilter("Survey")]
        public ActionResult ExportCsv(Guid Id)
        {
            //List<Survey_PostData> list = SurveyService.PostDataReport(Id, true);
            //List<Survey_Question> list2 = (from o in SurveyService.QuestionList(Id)
            //                               orderby o.QuestionIndex
            //                               select o).ToList<Survey_Question>();
            //List<string> collection = (from o in list2
            //                           orderby o.QuestionIndex
            //                           select o.QuestionText).ToList<string>();
            //List<string> list4 = new List<string> { "PostDate", "PostSource", "ClientIP", "ClientBrowser", "ClientPlatform" };
            //List<SurveyReportExportModel> objs = new List<SurveyReportExportModel>();
            //foreach (Survey_PostData data in list)
            //{
            //    SurveyReportExportModel item = new SurveyReportExportModel();
            //    item.Add("PostDate", data.PostDate.ToString(CultureInfo.InvariantCulture)).Add("PostSource", SurveyInfoViewModel.FormatPostSourceType(data.SourceTypeEnum)).Add("ClientIP", data.ClientIP).Add("ClientBrowser", data.ClientBrowser).Add("ClientPlatform", data.ClientPlatform);
            //    using (List<Survey_Question>.Enumerator enumerator2 = list2.GetEnumerator())
            //    {
            //        Func<Survey_Answer, bool> predicate = null;
            //        Survey_Question q;
            //        while (enumerator2.MoveNext())
            //        {
            //            q = enumerator2.Current;
            //            if (predicate == null)
            //            {
            //                predicate = o => o.QuestionId == q.Id;
            //            }
            //            List<Survey_Answer> source = data.Survey_Answer.Where<Survey_Answer>(predicate).ToList<Survey_Answer>();
            //            List<string> values = (from o in source select RateItems.RemoveSplit(o.AnswerText)).ToList<string>();
            //            if (source.Count > 0)
            //            {
            //                Survey_Answer answer = source.First<Survey_Answer>();
            //                if (!string.IsNullOrEmpty(answer.CommentText))
            //                {
            //                    values.Add(answer.CommentText);
            //                }
            //            }
            //            item.Add(q.QuestionText, string.Join(", ", values));
            //        }
            //    }
            //    objs.Add(item);
            //}
            //MemoryStream stream = new MemoryStream();
            //ExportFormatter<SurveyReportExportModel> formatter = ExportFormatter<SurveyReportExportModel>.Start();
            //Encoding encoding = Encoding.UTF8;
            //CultureInfo cultureInfo = base.CommunicatorContext.Portal.MessageContext.LocalizationInfo.CultureInfo;
            //using (CsvExportWriter writer = new CsvExportWriter(stream, encoding, new char?(cultureInfo.TextInfo.ListSeparator[0])))
            //{
            //    List<string> headers = new List<string>();
            //    headers.AddRange(list4);
            //    headers.AddRange(collection);
            //    writer.WriteHeader(headers);
            //    writer.WriteObjects<SurveyReportExportModel>(objs, headers, formatter);
            //}
            //stream.Position = 0L;
            //return File(stream, "text/csv", "post_data.csv".Localize(""));
            return null;
        }

        public SurveyPostSource GetSourceType(HttpRequest request)
        {
            IFormCollection values = (request != null) ? request.Form : HttpContext.Request.Form;
            IQueryCollection values2 = (request != null) ? request.Query : HttpContext.Request.Query;

         
            string s = string.Empty;
            if (!string.IsNullOrEmpty(values2["s"]))
            {
                s = values2["s"];
            }
            else if (!string.IsNullOrEmpty(values["s"]))
            {
                s = values["s"];
            }
            int result = -1;
            int.TryParse(s, out result);
            return ((result == -1) ? SurveyPostSource.Form : ((SurveyPostSource)result));
        }

        [HttpPost]
        public ActionResult Form(Guid? Id)
        {
            int num = 0;
            int.TryParse(base.Request.Form["__pageIndex"], out num);
            int num2 = 0;
            int.TryParse(base.Request.Form["__pageCount"], out num2);
            if (!Id.HasValue || (Id.Value == Guid.Empty))
            {
                Guid empty = Guid.Empty;
                if (Guid.TryParse(base.Request.Form["__formId"], out empty))
                {
                    Id = new Guid?(empty);
                }
            }

            System.Collections.Specialized.NameValueCollection nvc =  new System.Collections.Specialized.NameValueCollection();

            foreach (var item in HttpContext.Request.Form)
            {
                nvc.Add(item.Key, item.Value);
            }

            int num3 = PostWriter.PageTurning(nvc);

            Survey_Form survey = SurveyService.GetSurvey(Id.Value);
            ActionResult result = StartValidate(survey);
            if (result != null)
            {
                return result;
            }
            SurveyContext context = PrepareSurveyContext(survey, num + num3);
            SurveyPostSource sourceType = GetSourceType(HttpContext.Request);
            context.FormAction = base.Url.Action("Form", new { s = sourceType, Id = Id });
            string key = string.Empty;
            if (!ValidFormError(survey, ref key))
            {
                return base.RedirectToAction("FormError", new { Id = Id, Key = key });
            }
            List<SurveyQuestion> list = RuntimeKit.ProcessData(context.Handler, num);
            Dictionary<int, List<SurveyQuestion>> cache = GetCache(survey.Id);
            cache[num] = list;
            if ((num >= (num2 - 1)) && (num3 == 1))
            {
                SurveyPost datas = RuntimeKit.MergeData(cache, Guid.Empty);
                datas.SourceType = GetSourceType(base.Request);
                datas.ClientIP = GetClientIP(HttpContext);

                string userAgent = Request.Headers?.FirstOrDefault(s => s.Key.ToLower() == "user-agent").Value;

                datas.ClientBrowser = userAgent;
                datas.ClientPlatform = userAgent;
                datas.ContactId = string.Empty;
                if (!string.IsNullOrEmpty(survey.ValidatorType))
                {
                    datas.ValidToken = GetValidToken(survey);
                }
                SurveyService.SubmitPostData(survey.Id, datas);
                ClearCache(survey.Id);
                EndValidate(survey);
            }


            return base.View(ProcessSurvey(context, cache));
        }

        public string GetClientIP(HttpContext req)
        {
            IFeatureCollection values = (req != null) ? req.Features : HttpContext.Features;

            return values.Get<HttpConnectionFeature>()?.RemoteIpAddress.ToString();

            //if (values["HTTP_VIA"] != null)
            //{
            //    return values["HTTP_X_FORWARDED_FOR"].ToString();
            //}
            //return values["REMOTE_ADDR"].ToString();
        }

        private string PrepararHTMLToSave(String FormCompleteHtml)
        {
            int longLabelFormulario = 14;

            FormCompleteHtml = FormCompleteHtml.Replace("\\\"", "");
            FormCompleteHtml = FormCompleteHtml.Replace("\r\n", "");
            FormCompleteHtml = FormCompleteHtml.Substring(0, FormCompleteHtml.IndexOf("<!-- form buttons -->"));

            //Agrego directivas para las hojas de estilo:

            FormCompleteHtml = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>Form</title>" +
                                    "<link href=\"/Scripts/survey/runtime/field.css\" rel=\"stylesheet\" type=\"text/css\" media=\"Screen, Print\"> " +
                                    "<link href=\"/Scripts/survey/runtime/index.css\" rel=\"stylesheet\" type=\"text/css\" media=\"Screen, Print\"> " +
                                "</head><body>" + FormCompleteHtml + "</body></html>";

            //Quito el label del formulario

            FormCompleteHtml = FormCompleteHtml.Substring(0, FormCompleteHtml.IndexOf("</h2>") - longLabelFormulario) + FormCompleteHtml.Substring(FormCompleteHtml.IndexOf("</h2>"));

            return FormCompleteHtml;
        }

        public ActionResult Form(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            ActionResult result = StartValidate(survey);
            if (result != null)
            {
                return result;
            }
            SurveyContext context = PrepareSurveyContext(survey, 0);
            SurveyService.IncreaseVisitCount(Id);
            RuntimeKit.GenerateRuntimeCss();
            return base.View(ProcessSurvey(context, (List<SurveyQuestion>)null));
        }
        public ActionResult FormFancy(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            ActionResult result = StartValidate(survey);
            if (result != null)
            {
                return result;
            }
            SurveyContext context = PrepareSurveyContext(survey, 0);
            SurveyService.IncreaseVisitCount(Id);
            RuntimeKit.GenerateRuntimeCss();
            return base.View(ProcessSurvey(context, (List<SurveyQuestion>)null));
        }

        public ActionResult FormError(Guid Id, string Key)
        {
            //Dictionary<string, string> dictionary = (Dictionary<string, string>)HttpCon.Session[Key];
            
            base.ViewData["Message"] = HttpContext.Session;
            return base.View();
        }

        public ActionResult FormJoined(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            if (survey != null)
            {
                bool? respondResult = survey.RespondResult;
                if (respondResult.GetValueOrDefault() && respondResult.HasValue)
                {
                    respondResult = survey.RespondResult;
                    base.ViewData["RespondResult"] = !respondResult.GetValueOrDefault() ? ((object)0) : ((object)respondResult.HasValue);
                    base.ViewData["ResultUrl"] = base.Url.Action("FormReport", new { Id = survey.Id });
                }
            }
            return base.View();
        }

        public ActionResult FormMissing()
        {
            return base.View();
        }

        public ActionResult FormReport(Guid Id)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            StatisticsReport(survey, base.ViewData);
            base.ViewData["ShowLink"] = "false";
            base.ViewData["ShowPrefix"] = "false";
            return base.View();
        }

        [HttpPost]
        public ActionResult FormVerify(Guid Id, string Pass)
        {
            if (SurveyService.ValidJoinPassword(Id, Pass))
            {
                HttpContext.Session.SetString("__formVerify", true.ToString());
                return base.RedirectToAction("Form", new { Id = Id });
            }
            base.ViewData["SurveyId"] = Id;
            base.ViewData["Incorrect"] = "true";
            return base.View();
        }

        [HttpPost]
        public ActionResult FormVerifyEmail(Guid Id, string Email)
        {
            Regex regex = new Regex(@"\s*[\w.%-+]+@[\w.-]+\.[a-z]{2,4}\s*");
            if (!(!string.IsNullOrEmpty(Email) && regex.IsMatch(Email)))
            {
                base.ViewData["SurveyId"] = Id;
                base.ViewData["Message"] = "Dirección de correo inválida.";
                return base.View();
            }
            if (SurveyService.ExistToken(Id, Email))
            {
                return base.RedirectToAction("FormJoined", new { Id = Id });
            }
            HttpContext.Session.SetString("__verfyEmail", Email);
            return base.RedirectToAction("Form", new { Id = Id });
        }

        private Dictionary<int, List<SurveyQuestion>> GetCache(Guid Id)
        {
            string str = Id + "_postdata";
            Dictionary<int, List<SurveyQuestion>> dictionary = (Dictionary<int, List<SurveyQuestion>>)JsonConvert.DeserializeObject(HttpContext.Session.GetString(str));
            if (dictionary == null)
            {
                dictionary = new Dictionary<int, List<SurveyQuestion>>();

                HttpContext.Session.SetString(str, JsonConvert.SerializeObject(dictionary));
                //base.Session[str] = dictionary;
            }
            return dictionary;
        }

        private string GetValidToken(Survey_Form form)
        {
            ValidatorType type;
            if (Enum.TryParse<ValidatorType>(form.ValidatorType, true, out type))
            {
                if (type == ValidatorType.Cookie)
                {
                    string str = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    HttpContext.Session.SetString("__verfyCookie", str);
                    return str;
                }
                if (type == ValidatorType.Email)
                {
                    if (HttpContext.Session.GetString("__verfyEmail") != null)
                    {
                        return HttpContext.Session.GetString("__verfyEmail");
                    }
                }
                //else if (type == ValidatorType.IP)
                //{
                //    return RuntimeKit.GetClientIP(base.Request);
                //}
            }
            return null;
        }

        [AccessFilter("Survey")]
        public ActionResult Index()
        {
            base.Total = SurveyService.SurveyCount();
            List<Survey_Form> list = SurveyService.SurveyList(base.PagingParameters.Start, base.PagingParameters.Count);
            ListModel<Survey_Form> model = new ListModel<Survey_Form>
            {
                Items = list
            };
            return base.View(model);
        }

        [AccessFilter("Survey"), HttpPost]
        public ActionResult Index(Guid?[] Selected)
        {
            if (Selected != null)
            {
                foreach (Guid? nullable in Selected)
                {
                    if (nullable.HasValue && (nullable.Value != Guid.Empty))
                    {
                        if (base.Request.Form.Keys.Contains<string>("CommandStop"))
                        {
                            SurveyService.PausedSurvey(nullable.Value, true);
                            base.Info("La encuesta fue finalizada".Localize(""), 5);
                        }
                        else if (base.Request.Form.Keys.Contains<string>("CommandUnstop"))
                        {
                            SurveyService.PausedSurvey(nullable.Value, false);
                            base.Info("La encuesta fue iniciada".Localize(""), 5);
                        }
                        else if (base.Request.Form.Keys.Contains<string>("CommandPublish"))
                        {
                            SurveyService.PublishForm(nullable.Value, true);
                            base.Info("La encuesta fue publicada".Localize(""), 5);
                        }
                        else if (base.Request.Form.Keys.Contains<string>("CommandUnpublish"))
                        {
                            SurveyService.PublishForm(nullable.Value, false);
                            base.Info("La encuesta ha sido despublicada".Localize(""), 5);
                        }
                    }
                }
            }
            return base.RedirectToAction("Index");
        }

        [AccessFilter("Survey")]
        public ActionResult LoadHtml(Guid? id, bool? IsNew)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (id.HasValue && (id.Value != Guid.Empty))
            {
                Survey_Form survey = SurveyService.GetSurvey(id.Value);
                str = ((IsNew == true) ? "Copy of ".Localize("") : "") + survey.FormName;
                str2 = (survey.FormHtml ?? string.Empty).Trim();
            }
            return base.Json(new { name = str, html = str2 });
        }

        private SurveyContext PrepareSurveyContext(Survey_Form f, int pageIndex)
        {
            PostWriter writer = new PostWriter();
            if (f.RespondResult == true)
            {
                writer.ResultUrl = base.Url.Action("FormReport", new { Id = f.Id });
            }
            if (GetSourceType(base.Request) == SurveyPostSource.Nested)
            {
                writer.CloseButton = false;
            }
            SurveyContext context = SurveyService.ToSurveyForm(f).Context;
            context.PageIndex = pageIndex;
            context.Writer = writer;
            return context;
        }

        private SurveyContext PrepareSurveyContext(Survey_Preview p, int pageIndex)
        {
            PostWriter writer = new PostWriter();
            SurveyContext context = SurveyService.ToSurveyForm(p).Context;
            context.PageIndex = pageIndex;
            context.Writer = writer;
            return context;
        }

        public ActionResult Preview(Guid Id)
        {
            int result = 0;
            int.TryParse(base.Request.Form["__pageIndex"], out result);
            Survey_Preview p = SurveyService.GetPreview(Id);
            if (p == null)
            {
                base.RedirectToAction("FormMissing");
            }

            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

            foreach (var item in HttpContext.Request.Form)
            {
                nvc.Add(item.Key, item.Value);
            }

            int num2 = PostWriter.PageTurning(nvc);

            SurveyContext context = PrepareSurveyContext(p, result + num2);
            context.FormAction = base.Url.Action("Preview", new { Id = Id });
            List<SurveyQuestion> list = RuntimeKit.ProcessData(context.Handler, result);
            Dictionary<int, List<SurveyQuestion>> cache = GetCache(p.Id);
            cache[result] = list;
            RuntimeKit.GenerateRuntimeCss();
            return base.View("Form", ProcessSurvey(context, cache));
        }

        private SurveyFormViewModel ProcessSurvey(SurveyContext context, Dictionary<int, List<SurveyQuestion>> cacheData)
        {
            List<SurveyQuestion> datas = null;
            if (cacheData != null)
            {
                datas = new List<SurveyQuestion>();
                foreach (KeyValuePair<int, List<SurveyQuestion>> pair in cacheData)
                {
                    datas.AddRange(pair.Value.ToList<SurveyQuestion>());
                }
            }
            return ProcessSurvey(context, datas);
        }

        private SurveyFormViewModel ProcessSurvey(SurveyContext context, List<SurveyQuestion> datas = null)
        {
            if (datas != null)
            {
                context.Handler.RevertAnswer(datas);
            }
            return new SurveyFormViewModel { FormTitle = context.FormName, FormHtml = context.Handler.Render()};
        }

        [HttpPost, AccessFilter("Survey")]
        public ActionResult Publish(SurveyInfoViewModel model)
        {
            DateTime? nullable;
            DateTime? nullable2;
            if ((model.EndTime.HasValue && model.StartTime.HasValue) && (((nullable = model.EndTime).HasValue & (nullable2 = model.StartTime).HasValue) ? (nullable.GetValueOrDefault() <= nullable2.GetValueOrDefault()) : false))
            {
                base.ModelState.AddModelError("StartTime", "The start time must be earlier than the end time.".Localize(""));
            }
            if (base.ModelState.IsValid)
            {
                SurveyService.SaveSurvey(new Guid?(model.SurveyId), model.SurveyName, null, model.StartTime, model.EndTime, model.ValidatorType, model.Paused, model.JoinPassword, new bool?(model.RespondResult));
                if (base.Request.Form.Keys.Contains<string>("btnPrevious"))
                {
                    base.Info("La encuesta se ha guardado".Localize(""), 5);
                    return base.RedirectToAction("Builder", new { Id = model.SurveyId });
                }
                if (base.Request.Form.Keys.Contains<string>("btnSave"))
                {
                    base.Info("La encuesta se ha guardado".Localize(""), 5);
                }
                else if (base.Request.Form.Keys.Contains<string>("btnPublish"))
                {
                    SurveyService.PublishForm(model.SurveyId, true);
                    base.Info("La encuesta ha sido publicado.".Localize(""), 5);
                    return base.RedirectToAction("Distribute", new { Id = model.SurveyId });
                }
            }
            return base.RedirectToAction("Publish", new { Id = model.SurveyId });
        }

        [AccessFilter("Survey")]
        public ActionResult Publish(Guid Id)
        {
            var survey = SurveyService.GetSurvey(Id);
            var model = new SurveyInfoViewModel
            {
                SurveyId = survey.Id,
                ValidatorType = survey.ValidatorType,
                Published = survey.PublishTime.HasValue,
                Paused = survey.Paused,
                JoinPassword = survey.JoinPassword,
                RespondResult = survey.RespondResult == true,
                StartTime = survey.StartTime,
                EndTime = survey.EndTime,
                SurveyName = survey.FormName,
                IsNew = false
            };
            return base.View(model);
        }

        [AccessFilter("Survey")]
        public ActionResult ReportExport(Guid Id)
        {

            /*
            var viewData = new ViewDataDictionary<List<SurveyReportViewModel>>();
            StatisticsReport(SurveyService.GetSurvey(Id), viewData);
            var stream = new MemoryStream();
            var response = new HttpResponse(new StreamWriter(stream, Encoding.UTF8));
            var httpContext = new HttpContext(System.Web.HttpContext.Current.Request, response);
            var controllerContext = new ControllerContext(new HttpContextWrapper(httpContext), base.ControllerContext.RouteData, base.ControllerContext.Controller);
            var view = new WebFormView(base.ControllerContext, "~/Views/Survey/ReportExport.aspx");
            var viewContext = new ViewContext(controllerContext, view, viewData, base.TempData, httpContext.Response.Output);
            viewContext.View.Render(viewContext, viewContext.HttpContext.Response.Output);
            response.Flush();
            stream.Position = 0L;
            return File(stream, "text/html", "report.htm".Localize(""));

            */

            return null;
        }

        [AccessFilter("Survey")]
        public ActionResult ReportOverview(Guid Id)
        {
            StatisticsReport(SurveyService.GetSurvey(Id), base.ViewData);
            base.ViewData["ShowLink"] = "true";
            base.ViewData["ShowPrefix"] = "false";
            return base.View();
        }

        [AccessFilter("Survey")]
        public ActionResult ReportSingle(Guid Id, Guid? Qid)
        {
            Survey_Form survey = SurveyService.GetSurvey(Id);
            List<Survey_Question> list = SurveyService.QuestionList(Id);
            survey.Survey_Question = new List<Survey_Question>();
            Dictionary<string, List<SurveyAnswer>> answerOptions = SurveyService.ToSurveyForm(survey).GetAnswerOptions();
            foreach (Survey_Question question in list)
            {
                if (answerOptions.ContainsKey(question.QuestionHtmlId))
                {
                    survey.Survey_Question.Add(question);
                }
            }
            Survey_Question question2 = null;
            if (survey.Survey_Question.Count > 0)
            {
                question2 = (Qid.HasValue && (Qid.Value != Guid.Empty)) ? (from o in survey.Survey_Question
                                                                           where o.Id == Qid.Value
                                                                           select o).FirstOrDefault<Survey_Question>() : survey.Survey_Question.FirstOrDefault<Survey_Question>();
            }
            List<SurveyReportOption> list2 = null;
            if (question2 != null)
            {
                list2 = StatisticsQuestion(answerOptions, question2);
            }
            if (question2 != null)
            {
                base.ViewData["ChartSrc"] = base.Url.Action("ReportSingleChart", new { Id = survey.Id, Qid = question2.Id });
            }
            base.ViewData["ShowLink"] = "false";
            base.ViewData["ShowPrefix"] = "true";
            var model = new SurveyReportViewModel
            {
                Survey = survey,
                SelectedQuestion = question2,
                Items = list2
            };
            return base.View(model);
        }

        [AccessFilter("Survey")]
        public ActionResult ReportSingleChart(Guid Id, Guid Qid, string Type)
        {
            /*

            Survey_Form survey = SurveyService.GetSurvey(Id);
            Survey_Question question = SurveyService.GetQuestion(Qid);
            if ((question == null) || (survey == null))
            {
                return new EmptyResult();
            }
            List<SurveyReportOption> list = StatisticsQuestion(SurveyService.ToSurveyForm(survey).GetAnswerOptions(), question);
            if (list.Count == 0)
            {
                return new EmptyResult();
            }
            SeriesChartType chartType = (Type == "Column") ? SeriesChartType.Column : SeriesChartType.Pie;
            SurveyReportData data2 = new SurveyReportData
            {
                Options = list,
                TitleX = "Opciones",
                TitleY = "Subtotal",
                Title = SurveyInfoViewModel.FormatQuestionTitle(question)
            };
            SurveyReportData chartData = data2;
            byte[] fileContents = new SurveyReportChart(chartType, chartData).Output();
            return File(fileContents, "image/png", "chart.png");

            */

            return null;
        }

        [AccessFilter("Survey")]
        public ActionResult ReportSource(Guid Id)
        {
            /*
            Survey_Form survey = SurveyService.GetSurvey(Id);
            List<SurveyReportOption> list = StatisticsSource(survey.Id);
            base.ViewData["ChartSrc"] = base.Url.Action("ReportSourceChart", new { Id = survey.Id });
            base.ViewData["ShowPrefix"] = "true";
            SurveyReportViewModel model = new SurveyReportViewModel
            {
                Survey = survey,
                Items = list
            };
            return base.View(model);
            */
            return null;
        }

        [AccessFilter("Survey")]
        public ActionResult ReportSourceChart(Guid Id, string Type)
        {
            /*
            Survey_Form survey = SurveyService.GetSurvey(Id);
            List<SurveyReportOption> list = StatisticsSource(survey.Id);
            SeriesChartType chartType = (Type == "Column") ? SeriesChartType.Column : SeriesChartType.Pie;
            SurveyReportData data2 = new SurveyReportData
            {
                Options = list,
                TitleX = "Fuente",
                TitleY = "Subtotal",
                Title = survey.FormName
            };
            SurveyReportData chartData = data2;
            byte[] fileContents = new SurveyReportChart(chartType, chartData).Output();
            return File(fileContents, "image/png", "chart.png");
            */
            return null;
        }

        [AccessFilter("Survey"), HttpPost]
        public ActionResult SavePreview(string html, string name)
        {
            Survey_Preview preview = SurveyService.SavePreview(name ?? string.Empty, html);
            return base.RedirectToAction("Preview", new { Id = preview.Id });
        }

        private ActionResult StartValidate(Survey_Form form)
        {
            ValidatorType type;
            if (form == null)
            {
                return base.RedirectToAction("FormMissing");
            }
            string key = string.Empty;
            if (!ValidFormError(form, ref key))
            {
                return base.RedirectToAction("FormError", new { Id = form.Id, Key = key });
            }
            if (Enum.TryParse<ValidatorType>(form.ValidatorType, true, out type))
            {
                if (type == ValidatorType.Cookie)
                {
                    string str2 = string.Empty;
                    string cookie = HttpContext.Request.Cookies[form.Id.ToString()];
                        
                    if (cookie != null)
                    {
                        str2 = cookie;
                    }
                    if (!string.IsNullOrEmpty(str2) && SurveyService.ExistToken(form.Id, str2))
                    {
                        return base.RedirectToAction("FormJoined", new { Id = form.Id });
                    }
                }
                else if ((type == ValidatorType.Email) && (HttpContext.Session.GetString("__verfyEmail") == null))
                {
                    base.ViewData["SurveyId"] = form.Id;
                    return base.View("FormVerifyEmail");
                }
            }
            return null;
        }

        private List<SurveyReportOption> StatisticsQuestion(Dictionary<string, List<SurveyAnswer>> answersDict, Survey_Question question)
        {
            if (question == null)
            {
                return null;
            }
            if (!answersDict.ContainsKey(question.QuestionHtmlId))
            {
                return null;
            }
            List<SurveyReportOption> list = new List<SurveyReportOption>();
            List<Survey_Answer> source = SurveyService.AnswerListByQid(question.Id);
            List<SurveyAnswer> list3 = answersDict[question.QuestionHtmlId];
            if (list3 != null)
            {
                using (List<SurveyAnswer>.Enumerator enumerator = list3.GetEnumerator())
                {
                    Func<Survey_Answer, bool> predicate = null;
                    SurveyAnswer opt;
                    while (enumerator.MoveNext())
                    {
                        opt = enumerator.Current;
                        var option = new SurveyReportOption
                        {
                            OptionText = opt.AnswerText
                        };
                        if (predicate == null)
                        {
                            predicate = o => (o.AnswerTypeEnum == SurveyAnswerType.Option) && (o.AnswerHtmlId == opt.AnswerHtmlId);
                        }
                        option.Subtotal = source.Count<Survey_Answer>(predicate);
                        list.Add(option);
                    }
                }
                if (question.AllowComment == true)
                {
                    var option2 = new SurveyReportOption
                    {
                        OptionText = "(Comment Filled)".Localize(""),
                        Subtotal = source.Count<Survey_Answer>(o => o.AnswerTypeEnum == SurveyAnswerType.Comment)
                    };
                    list.Add(option2);
                }
                var option3 = new SurveyReportOption
                {
                    OptionText = "(Empty)".Localize(""),
                    Subtotal = source.Count<Survey_Answer>(o => o.AnswerTypeEnum == SurveyAnswerType.Empty)
                };
                list.Add(option3);
                return list;
            }
            SurveyReportOption item = new SurveyReportOption
            {
                OptionText = "(Filled)".Localize(""),
                Subtotal = source.Count<Survey_Answer>(o => o.AnswerTypeEnum == SurveyAnswerType.Input)
            };
            list.Add(item);
            SurveyReportOption option5 = new SurveyReportOption
            {
                OptionText = "(Empty)".Localize(""),
                Subtotal = source.Count<Survey_Answer>(o => o.AnswerTypeEnum == SurveyAnswerType.Empty)
            };
            list.Add(option5);
            return list;
        }

        private void StatisticsReport(Survey_Form survey, Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary viewData)
        {
            int num = SurveyService.PostDataCount(survey.Id);
            List<Survey_Question> list = SurveyService.QuestionList(survey.Id);
            SurveyFormClass class2 = SurveyService.ToSurveyForm(survey);
            List<SurveyReportViewModel> list2 = new List<SurveyReportViewModel>();
            foreach (Survey_Question question in list)
            {
                List<SurveyReportOption> list3 = StatisticsQuestion(class2.GetAnswerOptions(), question);
                if (list3 != null)
                {
                    SurveyReportViewModel item = new SurveyReportViewModel
                    {
                        Survey = survey,
                        SelectedQuestion = question,
                        Items = list3
                    };
                    list2.Add(item);
                }
            }
            viewData["SurveyTitle"] = class2.GetFormTitle();
            viewData["JoinCount"] = num;
            viewData["Survey"] = survey;
            viewData.Model = list2;
        }

        private List<SurveyReportOption> StatisticsSource(Guid formId)
        {
            List<Survey_PostData> source = SurveyService.PostDataReport(formId, false);
            List<SurveyPostSource> list2 = new List<SurveyPostSource> { SurveyPostSource.Form, SurveyPostSource.Nested, SurveyPostSource.Beyond };
            List<SurveyReportOption> list3 = new List<SurveyReportOption>();
            using (List<SurveyPostSource>.Enumerator enumerator = list2.GetEnumerator())
            {
                Func<Survey_PostData, bool> predicate = null;
                SurveyPostSource t;
                while (enumerator.MoveNext())
                {
                    t = enumerator.Current;
                    SurveyReportOption item = new SurveyReportOption
                    {
                        OptionText = SurveyInfoViewModel.FormatPostSourceType(t)
                    };
                    if (predicate == null)
                    {
                        predicate = o => o.SourceTypeEnum == t;
                    }
                    item.Subtotal = source.Count<Survey_PostData>(predicate);
                    list3.Add(item);
                }
            }
            return list3;
        }

        private bool ValidFormError(Survey_Form form, ref string Key)
        {
            DateTime? nullable;
            DateTime time2;
            DateTime now = DateTime.Now;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!form.PublishTime.HasValue)
            {
                dictionary.Add("Published", "La encuesta no se ha publicado aún".Localize(""));
            }
            if (form.StartTime.HasValue && ((nullable = form.StartTime).HasValue ? (nullable.GetValueOrDefault() > (time2 = now)) : false))
            {
                dictionary.Add("StartTime", "La encuesta no se ha iniciado aún".Localize(""));
            }
            if (form.EndTime.HasValue && ((nullable = form.EndTime).HasValue ? (nullable.GetValueOrDefault() < (time2 = now)) : false))
            {
                dictionary.Add("EndTime", "La encuesta expiro".Localize(""));
            }
            if (dictionary.Count > 0)
            {
                Key = Guid.NewGuid().ToString().Replace("-", string.Empty);
                //base.Session[Key] = dictionary;
                HttpContext.Session.SetString(Key, JsonConvert.SerializeObject(dictionary));
            }
            return (dictionary.Count == 0);
        }

        private DbSurveyService SurveyService
        {
            get
            {
                Func<Guid> getPortalId = null;
                if (_surveyService == null)
                {
                    if (getPortalId == null)
                    {
                        getPortalId = () => base.CommunicatorContext.PortalId;
                    }
                    _surveyService = new DbSurveyService(getPortalId);
                }
                return _surveyService;
            }
        }
    }
}

