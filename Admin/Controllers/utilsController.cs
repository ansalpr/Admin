using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminAPI.Base;
using API.Base;
using Admin.Helper.Utility.Authentication;
using AdminAPI.Models;
using System.Data;
using System.Diagnostics;
using Admin.Helper.General;
using EntityLayer.Tables;
using AdminAPI;
using Admin.Models.Other;
using Admin.Models.Utils;
using Admin.Helper.Utility;
using EntityLayer.Tables.Log;

namespace Admin.Controllers
{
    public class utilsController : BaseAdminController
    {
        public HttpResponseMessage AuthCheck([FromBody]authRequest reqObj)
        {
            #region variable
            string encCredentials = "";
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
            AuthHelper helperObj = new AuthHelper();
            GeneralHelper GH = new GeneralHelper();
            DataSet ds;
            authResponse response = new authResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.Login, reqObj.tui);
                //Validate Request
                response = helperObj.VaidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Check Login
                    ds = GH.getAuthData(reqObj);
                    if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        encCredentials = GH.getSignature(reqObj.name + "|" + reqObj.password + "|" + DateTime.Now.ToString() + "|" + ds.Tables[0].Rows[0]["UserId"].ToString());
                    }
                    //Response Processing
                    response = helperObj.processResponseToProxy(ds, reqObj.tui, encCredentials);
                }
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.Login, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui);
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
        public HttpResponseMessage LogEvent([FromBody] EventLogRequest reqObj)
        {
            #region variable
            int result = 0;
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            currentMethodName = sf.GetMethod().Name;
            currentControllerName = this.GetType().Name;
            #endregion

            #region objects
           
            //Entity Objects
            eventlogs entityObjects = new eventlogs { };
            EventLogHelper helperObj = new EventLogHelper();
            //DataOperation
            DataSet ds = new DataSet();
            EventLogResponse response = new EventLogResponse();
            #endregion

            try
            {
                //Log Request
                LogRequest(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reqObj), WorkFlowConstants.Login, reqObj.tui);
                //Validate Request
                response = helperObj.ValidateRequest(reqObj);
                if (response != null && response.code == ResponseConstants.OK.ToString())
                {
                    //Process Proxy to Entity
                    entityObjects = helperObj.ProcessProxyToEntity(reqObj, UserId,IP);
                    //Insert Entity Details
                    result = helperObj.ProcessInsertEntity(entityObjects);
                    if (result > 0)
                    {                       
                    }
                    else
                    {
                        response.eventlogs[0].message = entityObjects.ClassName + " Insertion " + ResponseConstants.Fail;
                    }
                    //Response Processing
                    response = helperObj.processResponseToProxy(response, ds, reqObj.tui, "", response.message, reqObj.eventlogs[0].action);
                }
                //Log Response
                LogResponse(currentControllerName, currentMethodName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response), WorkFlowConstants.Login, response.tui);
            }
            catch (Exception ex)
            {
                try
                {
                    currentMethodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                    currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
                    LogError(currentControllerName, currentMethodName, ex.Message, reqObj.tui);
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
        [HttpPost]
        public HttpResponseMessage GetEntityStructure([FromBody]DataProxy DP)
        {
            
            HttpResponseMessage msg = new HttpResponseMessage();
            GeneralHelper GH = new GeneralHelper();
            string resultString = "";
            try
            {
                
                    resultString = GH.getEntityStructure(DP.name, DP.conName, DP.type);
            }
            catch (Exception ex)
            {
                string methodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : "AuthCheck";
                string className = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;
            }
            msg = Request.CreateResponse(HttpStatusCode.OK, resultString);
            // msg = Request.CreateResponse(HttpStatusCode.OK, functionReturnValue);
            return msg;
        }
    }
}
