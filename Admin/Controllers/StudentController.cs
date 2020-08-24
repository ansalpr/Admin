using Admin.Base;
using Admin.Helper;
using Admin.Helper.Admin;
using Admin.Helper.General;
using Admin.Models.Admin;
using Admin.Models.Student;
using AdminAPI;
using AdminAPI.Base;
using API.Base;
using EntityLayer.Tables;
using EntityLayer.Tables.Admin;
using EntityLayer.Tables.Json;
using EntityLayer.Tables.Student;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Admin.Controllers
{
    public class StudentController : BaseAdminController
    {
        [AuthentificationFilter]
        public HttpResponseMessage ManageStudentEntry([FromBody] StudentRequest reqObj)
        {

            /*
             //  Loping is more faster than thread while insertion
            for (int x = 0; x < 10; x++)
            {
                CreateStudentJson(100);
            }

            int stdcount = 10;
           int TaskCount = 100;
           Task[] tasks = new Task[TaskCount];
           for (int x=0; x< TaskCount; x++)
           { 
               tasks[x] = Task.Factory.StartNew(() => CreateStudentJson(stdcount));
           }
           int ms = DateTime.Now.Second;
           Task.WaitAll(tasks);
           ms = DateTime.Now.Second - ms;
           Console.WriteLine("");           
           */
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            var json = "";
            #endregion

            #region objects
            //Helper Classes
            GeneralHelper GH = new GeneralHelper();
            StudentHelper helperObj = new StudentHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            JStudents[] entityObjects = new JStudents[] { };
            //Proxy Objects
            StudentResponse response = new StudentResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NStudent, reqObj.Tui);
                // Procees Reference Settings
                reqObj = helperObj.ProxyRequestReferenceSettings(reqObj);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);                    
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToJsonEntity(reqObj, UserId);
                    //Insert Entity Details
                    json = helperObj.ProcessObjectstoJson(entityObjects, reqObj.Tui);
                    //Process Proxy to JSON
                    ds = helperObj.ProcessInsertJsonEntity(json, UserId.ToString());
                }
                //Response Processing
                response = helperObj.processJsonResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Students[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NStudent, response.Tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.Tui == null ? "" : reqObj.Tui);
                }
                catch (Exception)
                {
                }

                response.Code = ResponseConstants.Exception.ToString();
                response.Message = ResponseConstants.SomeErrorOccoured;
            }

            msg = Request.CreateResponse(HttpStatusCode.OK, response);
            return msg;
        }

        private string CreateStudentJson(int numberOfStudent)
        {
            try
            {
                StudentHelper helperObj = new StudentHelper();
                JStudent jStudent = new JStudent();
                jStudent.Student = new JStudents[numberOfStudent];
                for (int stdCount = 0; stdCount < numberOfStudent; stdCount++)
                {

                    JParents[] centityObects = new JParents[2];
                    JStudents entityObect = new JStudents();
                    entityObect.Address1 = "Address" + stdCount.ToString();
                    entityObect.Address2 = "Address2" + stdCount.ToString();
                    entityObect.AdmissionNo = "";
                    entityObect.BloodGroupCode = "O+";
                    entityObect.CountryCode = "IN";
                    entityObect.DOB = "1980-02-10";
                    entityObect.FatherID = "";
                    entityObect.FirstName = "FirstName" + stdCount.ToString();
                    entityObect.Gender = "F";
                    entityObect.GuardianID = "";
                    entityObect.LastName = "LastName" + stdCount.ToString(); ;
                    entityObect.MiddleName = "";
                    entityObect.MotherID = "";
                    entityObect.MotherTongue = "MotherTongue";
                    entityObect.POB = "POB";
                    entityObect.StateCode = "Kerala";
                    entityObect.Stats = "";
                    entityObect.StudentId = "";
                    for (int cidx = 0; cidx < 2; cidx++)
                    {
                        JParents centityObect = new JParents();
                        centityObect.Address1 = "Address" + cidx.ToString();
                        centityObect.Address2 = "Address" + cidx.ToString();
                        centityObect.BloodGroupCode = "AB+";
                        centityObect.CountryCode = "IN";
                        centityObect.RelationCode = "MOT";
                        centityObect.StateCode = "Kerala";
                        centityObect.DOB = "1980-10-10";
                        centityObect.ParentName = "ParentName" + cidx.ToString();
                        centityObect.MotherTongue = "Malayalam";
                        centityObect.Stats = "";
                        centityObect.POB = "POB";
                        centityObect.ParentId = "";
                        centityObects[cidx] = centityObect;
                    }
                    entityObect.Parent = centityObects;
                    jStudent.Student[stdCount] = entityObect;
                }
                jStudent.TUI = "";
                var json = new JavaScriptSerializer().Serialize(jStudent);
                DataSet ds = helperObj.ProcessInsertJsonEntity(json, UserId.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }

            return "";
        }
    }
}
