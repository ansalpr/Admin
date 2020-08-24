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
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDepartment, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Departments.Length; idx++)
                    {
                        if (reqObj.Departments[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Departments[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Departments[idx].Message = entityObjects[idx].DepartmentName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Departments[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Departments[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Departments[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Departments[idx].Message = entityObjects[idx].DepartmentName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Departments[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Departments[idx].Message = entityObjects[idx].DepartmentName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Departments[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDepartment, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurriculum, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Curriculums.Length; idx++)
                    {
                        if (reqObj.Curriculums[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Curriculums[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Curriculums[idx].Message = entityObjects[idx].CurriculumName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Curriculums[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Curriculums[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Curriculums[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Curriculums[idx].Message = entityObjects[idx].CurriculumName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Curriculums[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Curriculums[idx].Message = entityObjects[idx].CurriculumName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Curriculums[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurriculum, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurrency, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Currencies.Length; idx++)
                    {
                        if (reqObj.Currencies[idx].Action.ToUpper() == "A")
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
                                    response.Currencies[idx].Message = entityObjects[idx].CurrencyName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Currencies[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Currencies[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Currencies[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Currencies[idx].Message = entityObjects[idx].CurrencyName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Currencies[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Currencies[idx].Message = entityObjects[idx].CurrencyName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Currencies[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurrency, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCountry, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Countries.Length; idx++)
                    {
                        if (reqObj.Countries[idx].Action.ToUpper() == "A")
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
                                    response.Countries[idx].Message = entityObjects[idx].CountryName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Countries[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Countries[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Countries[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Countries[idx].Message = entityObjects[idx].CountryName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Countries[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Countries[idx].Message = entityObjects[idx].CountryName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Countries[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCountry, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NCurrencyRate, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.CurrencyRate.Length; idx++)
                    {
                        if (reqObj.CurrencyRate[idx].Action.ToUpper() == "A")
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
                                        response.CurrencyRate[idx].Message = entityObjects[idx].CurrencyCode + " Rate Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.CurrencyRate[idx].Message = entityObjects[idx].CurrencyCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.CurrencyRate[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.CurrencyRate[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.CurrencyRate[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.CurrencyRate[idx].Message = entityObjects[idx].CurrencyCode + " Rate Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.CurrencyRate[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.CurrencyRate[idx].Message = entityObjects[idx].CurrencyCode + " Rate Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.CurrencyRate[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NCurrencyRate, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NState, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.States.Length; idx++)
                    {
                        if (reqObj.States[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCountryData(reqObj.States[idx].CountryCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.States[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.States[idx].Message = entityObjects[idx].StateName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.States[idx].Message = entityObjects[idx].CountryCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.States[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.States[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.States[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.States[idx].Message = entityObjects[idx].StateName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.States[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.States[idx].Message = entityObjects[idx].StateName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.States[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NState, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NClass, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Classes.Length; idx++)
                    {
                        if (reqObj.Classes[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCurriculumData(reqObj.Classes[idx].CurriculumCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.Classes[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.Classes[idx].Message = entityObjects[idx].ClassName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.Classes[idx].Message = entityObjects[idx].CurriculumCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.Classes[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Classes[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Classes[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Classes[idx].Message = entityObjects[idx].ClassName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Classes[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Classes[idx].Message = entityObjects[idx].ClassName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Classes[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NClass, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDivision, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Divisions.Length; idx++)
                    {
                        if (reqObj.Divisions[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Class Code Existance
                                if (ADMH.getTheCurriculumData(reqObj.Divisions[idx].ClassCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.Divisions[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.Divisions[idx].Message = entityObjects[idx].DivisionName + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.Divisions[idx].Message = entityObjects[idx].ClassCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.Divisions[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Divisions[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Divisions[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Divisions[idx].Message = entityObjects[idx].DivisionName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Divisions[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Divisions[idx].Message = entityObjects[idx].DivisionName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Divisions[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDivision, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NSection, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Sections.Length; idx++)
                    {
                        if (reqObj.Sections[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Sections[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Sections[idx].Message = entityObjects[idx].SectionName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Sections[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Sections[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Sections[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Sections[idx].Message = entityObjects[idx].SectionName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Sections[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Sections[idx].Message = entityObjects[idx].SectionName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Sections[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NSection, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NDesignation, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Designations.Length; idx++)
                    {
                        if (reqObj.Designations[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Designations[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Designations[idx].Message = entityObjects[idx].DesignationName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Designations[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Designations[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Designations[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Designations[idx].Message = entityObjects[idx].DesignationName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Designations[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Designations[idx].Message = entityObjects[idx].DesignationName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Designations[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NDesignation, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NModule, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Modules.Length; idx++)
                    {
                        if (reqObj.Modules[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Modules[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Modules[idx].Message = entityObjects[idx].ModuleName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Modules[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Modules[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Modules[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Modules[idx].Message = entityObjects[idx].ModuleName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Modules[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Modules[idx].Message = entityObjects[idx].ModuleName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Modules[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NModule, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NModuleControl, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.ModuleControls.Length; idx++)
                    {
                        if (reqObj.ModuleControls[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                if (ADMH.getTheModuleData(reqObj.ModuleControls[idx].ModuleCode, 0).Tables[0].Rows.Count > 0)
                                {
                                    //Insert Entity Details
                                    result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                    if (result > 0)
                                    {
                                        response.ModuleControls[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                    }
                                    else
                                    {
                                        response.ModuleControls[idx].Message = entityObjects[idx].ModuleCode + " Insertion " + ResponseConstants.Fail;
                                    }
                                }
                                else
                                {
                                    response.ModuleControls[idx].Message = entityObjects[idx].ModuleCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.ModuleControls[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.ModuleControls[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.ModuleControls[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.ModuleControls[idx].Message = entityObjects[idx].ModuleCode + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.ModuleControls[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.ModuleControls[idx].Message = entityObjects[idx].ModuleCode + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.ModuleControls[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NModuleControl, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NRelation, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Relations.Length; idx++)
                    {
                        if (reqObj.Relations[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Relations[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Relations[idx].Message = entityObjects[idx].RelationName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Relations[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Relations[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Relations[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Relations[idx].Message = entityObjects[idx].RelationName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Relations[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Relations[idx].Message = entityObjects[idx].RelationName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Relations[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NRelation, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NParent, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Parents.Length; idx++)
                    {
                        if (reqObj.Parents[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Check the Country Code Existance
                                if (ADMH.getTheCountryData(reqObj.Parents[idx].CountryCode, 0).Tables[0].Rows.Count > 0)
                                { 
                                    //Check the State Code Existance
                                    if (ADMH.getTheStateData(reqObj.Parents[idx].StateCode, 0).Tables[0].Rows.Count > 0)
                                    {
                                        //Check the BloodGroup Code Existance
                                        if (ADMH.getTheBloodGroupData(reqObj.Parents[idx].BloodGroupCode, 0).Tables[0].Rows.Count > 0)
                                        {
                                            //Insert Entity Details
                                            result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                            if (result > 0)
                                            {
                                                response.Parents[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                            }
                                            else
                                            {
                                                response.Parents[idx].Message = entityObjects[idx].ParentName + " Insertion " + ResponseConstants.Fail;
                                            }
                                        }
                                        else
                                        {
                                            response.Parents[idx].Message = entityObjects[idx].BloodGroupCode + " " + ResponseConstants.InValid;
                                        }
                                    }
                                    else
                                    {
                                        response.Parents[idx].Message = entityObjects[idx].StateCode + " " + ResponseConstants.InValid;
                                    }
                                }
                                else
                                {
                                    response.Parents[idx].Message = entityObjects[idx].CountryCode + " " + ResponseConstants.InValid;
                                }

                            }
                            else
                            {
                                response.Parents[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Parents[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Parents[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Parents[idx].Message = entityObjects[idx].ParentName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Parents[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Parents[idx].Message = entityObjects[idx].ParentName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Parents[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NParent, response.Tui);
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
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NBloodGroup, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.BloodGroups.Length; idx++)
                    {
                        if (reqObj.BloodGroups[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.BloodGroups[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.BloodGroups[idx].Message = entityObjects[idx].BloodGroupName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.BloodGroups[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.BloodGroups[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.BloodGroups[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.BloodGroups[idx].Message = entityObjects[idx].BloodGroupName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.BloodGroups[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.BloodGroups[idx].Message = entityObjects[idx].BloodGroupName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.BloodGroups[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NBloodGroup, response.Tui);
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
        [AuthentificationFilter]
        public HttpResponseMessage ManageMenu([FromBody] MenuRequest reqObj)
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
            MenuHelper helperObj = new MenuHelper();
            //DataOperation
            DataSet ds = new DataSet();
            //Entity Objects
            menu[] entityObjects = new menu[] { };
            //Proxy Objects
            MenuResponse response = new MenuResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.NMenu, reqObj.Tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.Code == ResponseConstants.OK.ToString())
                {
                    //Get Logined User Id
                    UserId = GH.GetUserId(Request.Headers.Authorization.Parameter);
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId);

                    for (int idx = 0; idx < reqObj.Menus.Length; idx++)
                    {
                        if (reqObj.Menus[idx].Action.ToUpper() == "A")
                        {
                            //Check The Existance Of New Request
                            if (!helperObj.CheckTheDataExistance(entityObjects[idx]))
                            {
                                //Insert Entity Details
                                result = helperObj.ProcessInsertEntity(entityObjects[idx]);
                                if (result > 0)
                                {
                                    response.Menus[idx].Id = getEncryptData(result.ToString(), DBConstants.PrimaryKey);
                                }
                                else
                                {
                                    response.Menus[idx].Message = entityObjects[idx].MenuName + " Insertion " + ResponseConstants.Fail;
                                }
                            }
                            else
                            {
                                response.Menus[idx].Message = ResponseConstants.Exist;
                            }
                        }
                        else if (reqObj.Menus[idx].Action.ToUpper() == "S")
                        {
                            //Get The Data
                            ds = helperObj.GetTheData(entityObjects[idx]);
                        }
                        else if (reqObj.Menus[idx].Action.ToUpper() == "E")
                        {
                            //Update The Data
                            result = helperObj.UpdateTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Menus[idx].Message = entityObjects[idx].MenuName + " Update " + ResponseConstants.Fail;
                            }
                        }
                        else if (reqObj.Menus[idx].Action.ToUpper() == "D")
                        {
                            //Delete The Data
                            result = helperObj.DeleteTheData(entityObjects[idx]);
                            if (result > 0)
                            {
                            }
                            else
                            {
                                response.Menus[idx].Message = entityObjects[idx].MenuName + " Deletion " + ResponseConstants.Fail;
                            }
                        }

                    }
                }
                //Response Processing
                response = helperObj.processResponseToProxy(response, ds, reqObj.Tui, Request.Headers.Authorization.Parameter, response.Message, reqObj.Menus[0].Action);
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.NMenu, response.Tui);
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
    }
}
