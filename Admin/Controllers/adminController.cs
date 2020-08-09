using Admin.Base;
using Admin.Helper.Admin;
using Admin.Helper.General;
using Admin.Models.Admin;
using AdminAPI;
using AdminAPI.Base;
using API.Base;
using EntityLayer.Tables;
using EntityLayer.Tables.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace Admin.Controllers
{
    public class adminController : BaseAdminController
    {

        [AuthentificationFilter]
        public HttpResponseMessage ManageDepartment([FromBody] DepartmentRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            //Helper Classes
            GeneralHelper GH = new GeneralHelper();
            DepartmentHelper helperObj = new DepartmentHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            department[] entityObjects = new department[] { };
            //Proxy Objects
            DepartmentResponse response = new DepartmentResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDepartment, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.departments.Length; idx++)
                    {
                        if (reqObj.departments[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.departments[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);                                   
                                }
                                else
                                {
                                    response.departments[idx].message =  entityObjects[idx].DepartmentName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.departments[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.departments[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.departments[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {                               
                            }
                            else
                            {
                                response.departments[idx].message =  entityObjects[idx].DepartmentName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.departments[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.departments[idx].message = entityObjects[idx].DepartmentName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response,  ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.departments[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDepartment, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui == null ? "" : reqObj.tui);
                }
                catch (Exception)
                {
                }

                response.code = ResponseConstants.Exception.ToString();
                response.message = ResponseConstants.SomeErrorOccoured;
            }
            msg = Request.CreateResponse(HttpStatusCode.OK, response);
            return msg;
        }
        [AuthentificationFilter]
        public HttpResponseMessage ManageCurriculum([FromBody] CurriculumRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            //Helper Classes
            GeneralHelper GH = new GeneralHelper();
            CurriculumHelper helperObj = new CurriculumHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            curriculum[] entityObjects = new curriculum[] { };
            //Proxy Objects
            CurriculumResponse response = new CurriculumResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurriculum, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.curriculums.Length; idx++)
                    {
                        if (reqObj.curriculums[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.curriculums[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.curriculums[idx].message = entityObjects[idx].CurriculumName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.curriculums[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.curriculums[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.curriculums[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.curriculums[idx].message = entityObjects[idx].CurriculumName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.curriculums[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.curriculums[idx].message = entityObjects[idx].CurriculumName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.curriculums[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurriculum, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui == null ? "" : reqObj.tui);
                }
                catch (Exception)
                {
                }

                response.code = ResponseConstants.Exception.ToString();
                response.message = ResponseConstants.SomeErrorOccoured;
            }
            msg = Request.CreateResponse(HttpStatusCode.OK, response);
            return msg;
        }
        [AuthentificationFilter]
        public HttpResponseMessage ManageCurrency([FromBody] CurrencyRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            //Helper Classes
            GeneralHelper GH = new GeneralHelper();
            CurrencyHelper helperObj = new CurrencyHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            currency[] entityObjects = new currency[] { };
            //Proxy Objects
            CurrencyResponse response = new CurrencyResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurrency, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Currencies.Length; idx++)
                    {
                        if (reqObj.Currencies[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Currencies[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Currencies[idx].message = entityObjects[idx].CurrencyName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Currencies[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Currencies[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Currencies[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Currencies[idx].message = entityObjects[idx].CurrencyName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Currencies[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Currencies[idx].message = entityObjects[idx].CurrencyName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.Currencies[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurrency, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui == null ? "" : reqObj.tui);
                }
                catch (Exception)
                {
                }

                response.code = ResponseConstants.Exception.ToString();
                response.message = ResponseConstants.SomeErrorOccoured;
            }
            msg = Request.CreateResponse(HttpStatusCode.OK, response);
            return msg;
        }
        [AuthentificationFilter]
        public HttpResponseMessage ManageCountry([FromBody] CountryRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            //Helper Classes
            GeneralHelper GH = new GeneralHelper();
            CountryHelper helperObj = new CountryHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            country[] entityObjects = new country[] { };
            //Proxy Objects
            CountryResponse response = new CountryResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCountry, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Countries.Length; idx++)
                    {
                        if (reqObj.Countries[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Countries[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Countries[idx].message = entityObjects[idx].CountryName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Countries[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Countries[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Countries[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Countries[idx].message = entityObjects[idx].CountryName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Countries[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Countries[idx].message = entityObjects[idx].CountryName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.Countries[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCountry, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui == null ? "" : reqObj.tui);
                }
                catch (Exception)
                {
                }

                response.code = ResponseConstants.Exception.ToString();
                response.message = ResponseConstants.SomeErrorOccoured;
            }
            msg = Request.CreateResponse(HttpStatusCode.OK, response);
            return msg;
        }
    }
}
