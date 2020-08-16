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
            response.eventlogs = new EventLogs[reqObjects.eventlogs.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.eventlogs.Length; idx++)
            {
                if (reqObjects.eventlogs == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.eventlogs[idx].action.ToUpper() == "A" || reqObjects.eventlogs[idx].action.ToUpper() == "E"))
                {
                    if ((reqObjects.eventlogs[idx].ClassName == null || reqObjects.eventlogs[idx].ClassName == ""))
                    {
                        message = "ClassName " + ResponseConstants.Mandatory;
                    }                   
                    else if ((reqObjects.eventlogs[idx].MethodName == null || reqObjects.eventlogs[idx].MethodName == ""))
                    {
                        message = "MethodName " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.eventlogs[idx].Remarks == null || reqObjects.eventlogs[idx].Remarks  == ""))
                    {
                        message = "Remarks " + ResponseConstants.Mandatory;
                    }
                }                
                EventLogs proxyResponse = new EventLogs();
                proxyResponse = reqObjects.eventlogs[idx];
                proxyResponse.message = message;
                response.eventlogs[idx] = proxyResponse;
                if (message != "")
                {
                    response.message = "Invalid Request";
                }
            }
            response.tui = reqObjects.tui;
            if (response.message == "" || response.message == null)
            {
                response.code = ResponseConstants.OK.ToString();
            }
            else
            {
                response.code = ResponseConstants.NotOK.ToString();
            }
            return response;
        }
        public eventlogs ProcessProxyToEntity(EventLogRequest reqObjects, int UserId,string IP)
        {
            eventlogs entityObects = new eventlogs();
            try            {
                for (int idx = 0; idx < reqObjects.eventlogs.Length; idx++)
                {
                    eventlogs entityObect = new eventlogs();
                    entityObect.ClassName = reqObjects.eventlogs[idx].ClassName == null ? "" : reqObjects.eventlogs[idx].ClassName.Trim();
                    entityObect.IP = IP;
                    entityObect.MethodName = reqObjects.eventlogs[idx].MethodName == null ? "" : reqObjects.eventlogs[idx].MethodName.Trim();
                    entityObect.Remarks = reqObjects.eventlogs[idx].Remarks == null ? "" : reqObjects.eventlogs[idx].Remarks.Trim();
                    entityObect.TUI = reqObjects.eventlogs[idx].TUI == null ? "" : reqObjects.eventlogs[idx].TUI.Trim();                                                            
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
        public EventLogResponse processResponseToProxy(EventLogResponse response, DataSet ds, string tui, string signature, string message, string action)
        {
            try
            {
                if (action != "S")
                {
                    response = processResponseToProxy(response, tui, signature, message, action);
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
        private EventLogResponse processResponseToProxy(EventLogResponse response, string tui, string signature, string message, string action)
        {
            try
            {

                foreach (EventLogs dept in response.eventlogs)
                {
                    if (dept.message != "")
                    {
                        response.code = ResponseConstants.NotOK.ToString();
                        response.message = ResponseConstants.Fail;
                        break;
                    }
                    else
                    {
                        response.code = ResponseConstants.OK.ToString();
                        response.message = ResponseConstants.Success;
                    }
                }
                response.signature = signature;
                response.tui = tui;

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