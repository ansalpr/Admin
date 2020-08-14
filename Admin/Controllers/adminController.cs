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
                                    response.departments[idx].message = entityObjects[idx].DepartmentName + " Insertion " + ResponseConstants.Fail;
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
                                response.departments[idx].message = entityObjects[idx].DepartmentName + " Update " + ResponseConstants.Fail;
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
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.departments[0].action);
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
        [AuthentificationFilter]
        public HttpResponseMessage ManageCurrencyRate([FromBody] CurrencyRateRequest reqObj)
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
            AdminHelper ADMH = new AdminHelper();
            CurrencyRateHelper helperObj = new CurrencyRateHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            currencyrate[] entityObjects = new currencyrate[] { };
            //Proxy Objects
            CurrencyRateResponse response = new CurrencyRateResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurrencyRate, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.CurrencyRate.Length; idx++)
                    {
                        if (reqObj.CurrencyRate[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the CurrencyCode Existance                               
                                if (ADMH.getTheCurrencyData(reqObj.CurrencyRate[idx].CurrencyCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.CurrencyRate[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.CurrencyRate[idx].message = entityObjects[idx].CurrencyCode + " Rate Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.CurrencyRate[idx].message = entityObjects[idx].CurrencyCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.CurrencyRate[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.CurrencyRate[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.CurrencyRate[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.CurrencyRate[idx].message = entityObjects[idx].CurrencyCode + " Rate Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.CurrencyRate[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.CurrencyRate[idx].message = entityObjects[idx].CurrencyCode + " Rate Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.CurrencyRate[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurrencyRate, response.tui);
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
        public HttpResponseMessage ManageState([FromBody] StateRequest reqObj)
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
            AdminHelper ADMH = new AdminHelper();
            StateHelper helperObj = new StateHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            state[] entityObjects = new state[] { };
            //Proxy Objects
            StateResponse response = new StateResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NState, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.states.Length; idx++)
                    {
                        if (reqObj.states[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCountryData(reqObj.states[idx].CountryCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.states[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.states[idx].message = entityObjects[idx].StateName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.states[idx].message = entityObjects[idx].CountryCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.states[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.states[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.states[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.states[idx].message = entityObjects[idx].StateName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.states[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.states[idx].message = entityObjects[idx].StateName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.states[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NState, response.tui);
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
        public HttpResponseMessage ManageClass([FromBody] ClassRequest reqObj)
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
            AdminHelper ADMH = new AdminHelper();
            ClassHelper helperObj = new ClassHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            classe[] entityObjects = new classe[] { };
            //Proxy Objects
            ClassResponse response = new ClassResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NClass, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.classes.Length; idx++)
                    {
                        if (reqObj.classes[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCurriculumData(reqObj.classes[idx].CurriculumCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.classes[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.classes[idx].message = entityObjects[idx].ClassName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.classes[idx].message = entityObjects[idx].CurriculumCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.classes[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.classes[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.classes[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.classes[idx].message = entityObjects[idx].ClassName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.classes[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.classes[idx].message = entityObjects[idx].ClassName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.classes[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NClass, response.tui);
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
        public HttpResponseMessage ManageDivision([FromBody] DivisionRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            //HelperDivisiones
            GeneralHelper GH = new GeneralHelper();
            AdminHelper ADMH = new AdminHelper();
            DivisionHelper helperObj = new DivisionHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            division[] entityObjects = new division[] { };
            //Proxy Objects
            DivisionResponse response = new DivisionResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDivision, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.divisions.Length; idx++)
                    {
                        if (reqObj.divisions[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Class Code Existance
                                if (ADMH.getTheCurriculumData(reqObj.divisions[idx].ClassCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.divisions[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.divisions[idx].message = entityObjects[idx].DivisionName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.divisions[idx].message = entityObjects[idx].ClassCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.divisions[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.divisions[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.divisions[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.divisions[idx].message = entityObjects[idx].DivisionName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.divisions[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.divisions[idx].message = entityObjects[idx].DivisionName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.divisions[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDivision, response.tui);
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
        public HttpResponseMessage ManageSection([FromBody] SectionRequest reqObj)
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
            SectionHelper helperObj = new SectionHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            section[] entityObjects = new section[] { };
            //Proxy Objects
            SectionResponse response = new SectionResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NSection, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.sections.Length; idx++)
                    {
                        if (reqObj.sections[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.sections[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.sections[idx].message = entityObjects[idx].SectionName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.sections[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.sections[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.sections[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.sections[idx].message = entityObjects[idx].SectionName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.sections[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.sections[idx].message = entityObjects[idx].SectionName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.sections[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NSection, response.tui);
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
        public HttpResponseMessage ManageDesignation([FromBody] DesignationRequest reqObj)
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
            DesignationHelper helperObj = new DesignationHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            designation[] entityObjects = new designation[] { };
            //Proxy Objects
            DesignationResponse response = new DesignationResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDesignation, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.designations.Length; idx++)
                    {
                        if (reqObj.designations[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.designations[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.designations[idx].message = entityObjects[idx].DesignationName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.designations[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.designations[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.designations[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.designations[idx].message = entityObjects[idx].DesignationName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.designations[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.designations[idx].message = entityObjects[idx].DesignationName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.designations[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDesignation, response.tui);
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
        public HttpResponseMessage ManageModule([FromBody] ModuleRequest reqObj)
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
            ModuleHelper helperObj = new ModuleHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            module[] entityObjects = new module[] { };
            //Proxy Objects
            ModuleResponse response = new ModuleResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NModule, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.modules.Length; idx++)
                    {
                        if (reqObj.modules[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.modules[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.modules[idx].message = entityObjects[idx].ModuleName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.modules[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.modules[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.modules[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.modules[idx].message = entityObjects[idx].ModuleName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.modules[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.modules[idx].message = entityObjects[idx].ModuleName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.modules[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NModule, response.tui);
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
        public HttpResponseMessage ManageModuleControlControl([FromBody] ModuleControlRequest reqObj)
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
            AdminHelper ADMH = new AdminHelper();
            ModuleControlHelper helperObj = new ModuleControlHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            modulecontrol[] entityObjects = new modulecontrol[] { };
            //Proxy Objects
            ModuleControlResponse response = new ModuleControlResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NModuleControl, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.modulecontrols.Length; idx++)
                    {
                        if (reqObj.modulecontrols[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                if (ADMH.getTheModuleData(reqObj.modulecontrols[idx].ModuleCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.modulecontrols[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.modulecontrols[idx].message = entityObjects[idx].ModuleCode + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.modulecontrols[idx].message = entityObjects[idx].ModuleCode + " " + ResponseConstants.InValid;
                                }
                               
                            }
                            else
                            {
                                response.modulecontrols[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.modulecontrols[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.modulecontrols[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.modulecontrols[idx].message = entityObjects[idx].ModuleCode + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.modulecontrols[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.modulecontrols[idx].message = entityObjects[idx].ModuleCode + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.modulecontrols[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NModuleControl, response.tui);
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
        public HttpResponseMessage ManageRelation([FromBody] RelationRequest reqObj)
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
            RelationHelper helperObj = new RelationHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            relation[] entityObjects = new relation[] { };
            //Proxy Objects
            RelationResponse response = new RelationResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NRelation, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.relations.Length; idx++)
                    {
                        if (reqObj.relations[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.relations[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.relations[idx].message = entityObjects[idx].RelationName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.relations[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.relations[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.relations[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.relations[idx].message = entityObjects[idx].RelationName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.relations[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.relations[idx].message = entityObjects[idx].RelationName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.relations[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NRelation, response.tui);
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
        public HttpResponseMessage ManageParent([FromBody] ParentRequest reqObj)
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
            AdminHelper ADMH = new AdminHelper();
            ParentHelper helperObj = new ParentHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            parent[] entityObjects = new parent[] { };
            //Proxy Objects
            ParentResponse response = new ParentResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NParent, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.parents.Length; idx++)
                    {
                        if (reqObj.parents[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCountryData(reqObj.parents[idx].Country, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Check the BloodGroup Code Existance
                                    if (ADMH.getTheBloodGroupData(reqObj.parents[idx].BloodGroup, 0).Tables[0].Rows.Count > 0)
                                    {
                                        //Insert Entity Details
                                        result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                        if (result > 0)
                                        {
                                            response.parents[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                        }
                                        else
                                        {
                                            response.parents[idx].message = entityObjects[idx].ParentName + " Insertion " + ResponseConstants.Fail;
                                        }
                                    }
                                    else
                                    {
                                        response.parents[idx].message = entityObjects[idx].Country + " " + ResponseConstants.InValid;
                                    }                                   
                                }
                                else
                                {
                                    response.parents[idx].message = entityObjects[idx].Country + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.parents[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.parents[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.parents[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.parents[idx].message = entityObjects[idx].ParentName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.parents[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.parents[idx].message = entityObjects[idx].ParentName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.parents[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NParent, response.tui);
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
        public HttpResponseMessage ManageBloodGroup([FromBody] BloodGroupRequest reqObj)
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
            BloodGroupHelper helperObj = new BloodGroupHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            bloodgroup[] entityObjects = new bloodgroup[] { };
            //Proxy Objects
            BloodGroupResponse response = new BloodGroupResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NBloodGroup, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.bloodgroups.Length; idx++)
                    {
                        if (reqObj.bloodgroups[idx].action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.bloodgroups[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.bloodgroups[idx].message = entityObjects[idx].BloodGroupName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.bloodgroups[idx].message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.bloodgroups[idx].action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.bloodgroups[idx].action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.bloodgroups[idx].message = entityObjects[idx].BloodGroupName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.bloodgroups[idx].action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.bloodgroups[idx].message = entityObjects[idx].BloodGroupName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.tui, Request.Headers.Authorization.Parameter, response.message, reqObj.bloodgroups[0].action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NBloodGroup, response.tui);
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
