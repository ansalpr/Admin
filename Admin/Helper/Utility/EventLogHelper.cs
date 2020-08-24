using Admin.Base;
using Admin.Constants.Table;
using Admin.Models.Admin;
using Admin.Models.Utils;
using API.Base;
using DataLayer;
using EntityLayer.StoredProcedures.Admin;
using EntityLayer.Tables.Admin;
using EntityLayer.Tables.Log;
using GeneralLayer;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;


namespace Admin.Helper.Utility
{
    public class EventLogHelper : BaseHelper
    {
        public EventLogResponse ValidateRequest(EventLogRequest reqObjects)
        {
            EventLogResponse response = new EventLogResponse();
            response.EventLogs = new EventLogs[reqObjects.EventLogs.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.EventLogs.Length; idx++)
            {
                if (reqObjects.EventLogs == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.EventLogs[idx].Action.ToUpper() == "A" || reqObjects.EventLogs[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.EventLogs[idx].ClassName == null || reqObjects.EventLogs[idx].ClassName == ""))
                    {
                        message = "ClassName " + ResponseConstants.Mandatory;
                    }                   
                    else if ((reqObjects.EventLogs[idx].MethodName == null || reqObjects.EventLogs[idx].MethodName == ""))
                    {
                        message = "MethodName " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.EventLogs[idx].Remarks == null || reqObjects.EventLogs[idx].Remarks  == ""))
                    {
                        message = "Remarks " + ResponseConstants.Mandatory;
                    }
                }                
                EventLogs proxyResponse = new EventLogs();
                proxyResponse = reqObjects.EventLogs[idx];
                proxyResponse.Message = message;
                response.EventLogs[idx] = proxyResponse;
                if (message != "")
                {
                    response.Message = "Invalid Request";
                }
            }
            response.Tui = reqObjects.Tui;
            if (response.Message == "" || response.Message == null)
            {
                response.Code = ResponseConstants.OK.ToString();
            }
            else
            {
                response.Code = ResponseConstants.NotOK.ToString();
            }
            return response;
        }
        public eventlogs ProcessProxyToEntity(EventLogRequest reqObjects, int UserId,string IP)
        {
            eventlogs entityObects = new eventlogs();
            try            {
                for (int idx = 0; idx < reqObjects.EventLogs.Length; idx++)
                {
                    eventlogs entityObect = new eventlogs();
                    entityObect.ClassName = reqObjects.EventLogs[idx].ClassName == null ? "" : reqObjects.EventLogs[idx].ClassName.Trim();
                    entityObect.IP = IP;
                    entityObect.MethodName = reqObjects.EventLogs[idx].MethodName == null ? "" : reqObjects.EventLogs[idx].MethodName.Trim();
                    entityObect.Remarks = reqObjects.EventLogs[idx].Remarks == null ? "" : reqObjects.EventLogs[idx].Remarks.Trim();
                    entityObect.TUI = reqObjects.EventLogs[idx].Tui == null ? "" : reqObjects.EventLogs[idx].Tui.Trim();                                                            
                    entityObects = entityObect;
                }

            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;

                currentMethodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                string currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;

                Exception customex = new Exception(currentMethodName + " | " + currentControllerName + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return entityObects;
        }       
        public int ProcessInsertEntity(eventlogs entityObject)
        {
            int result = 0;
            string TableName = "eventlog";
            string skipAttributes = "EventLogId,CreatedDate,ModifiedDate,";
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDBLog);
                DataOperation DO = new DataOperation(dbCon);
                DO.BeginTRansaction();
                result = DO.iteratePropertyObjectsAndInsert(entityObject, TableName, true, skipAttributes);
                DO.EndTRansaction();
                result = 1;
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;

                currentMethodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : currentMethodName;
                string currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;

                Exception customex = new Exception(currentMethodName + " | " + currentControllerName + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return result;
        }
        public EventLogResponse processResponseToProxy(EventLogResponse response, DataSet ds, string Tui, string signature, string message, string action)
        {
            try
            {
                if (action != "S")
                {
                    response = processResponseToProxy(response, Tui, signature, message, action);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return response;
        }
        private EventLogResponse processResponseToProxy(EventLogResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (EventLogs dept in response.EventLogs)
                {
                    if (dept.Message != "")
                    {
                        response.Code = ResponseConstants.NotOK.ToString();
                        response.Message = ResponseConstants.Fail;
                        break;
                    }
                    else
                    {
                        response.Code = ResponseConstants.OK.ToString();
                        response.Message = ResponseConstants.Success;
                    }
                }
                response.Signature = signature;
                response.Tui = Tui;

            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return response;
        }
      
    }
}