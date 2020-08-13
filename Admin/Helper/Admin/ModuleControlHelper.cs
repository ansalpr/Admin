using Admin.Base;
using Admin.Constants.Table;
using Admin.Models.Admin;
using API.Base;
using DataLayer;
using EntityLayer.StoredProcedures.Admin;
using EntityLayer.Tables.Admin;
using GeneralLayer;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Admin.Helper.Admin
{
    public class ModuleControlHelper : BaseHelper
    {
        public ModuleControlResponse ValidateRequest(ModuleControlRequest reqObjects)
        {
            ModuleControlResponse response = new ModuleControlResponse();
            response.modulecontrols = new ModuleControl[reqObjects.modulecontrols.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.modulecontrols.Length; idx++)
            {
                if (reqObjects.modulecontrols == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.modulecontrols[idx].action.ToUpper() == "A" || reqObjects.modulecontrols[idx].action.ToUpper() == "E"))
                {
                    if ((reqObjects.modulecontrols[idx].ModuleCode == null || reqObjects.modulecontrols[idx].ModuleCode == ""))
                    {
                        message = CnstModuleControl.ModuleCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.modulecontrols[idx].BackDate == null || reqObjects.modulecontrols[idx].BackDate == ""))
                    {
                        message = CnstModuleControl.BackDate + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.modulecontrols[idx].From == null || reqObjects.modulecontrols[idx].From == ""))
                    {
                        message = CnstModuleControl.From + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.modulecontrols[idx].FutureDate == null || reqObjects.modulecontrols[idx].FutureDate == ""))
                    {
                        message = CnstModuleControl.FutureDate + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.modulecontrols[idx].Stats == null || reqObjects.modulecontrols[idx].Stats == ""))
                    {
                        message = CnstModuleControl.Stats + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.modulecontrols[idx].TO == null || reqObjects.modulecontrols[idx].TO == ""))
                    {
                        message = CnstModuleControl.Stats + " " + ResponseConstants.Mandatory;
                    }
                    else
                    {
                        if (!validateDateFormat(reqObjects.modulecontrols[idx].From))
                        {
                            message = ResponseConstants.InValid + " " + CnstModuleControl.From;
                        }
                        else if (!validateDateFormat(reqObjects.modulecontrols[idx].TO))
                        {
                            message = ResponseConstants.InValid + " " + CnstModuleControl.TO;
                        }
                    }
                }
                else if ((reqObjects.modulecontrols[idx].Id == null || reqObjects.modulecontrols[idx].Id == "") && (reqObjects.modulecontrols[idx].action.ToUpper() == "E" || reqObjects.modulecontrols[idx].action.ToUpper() == "D"))
                {
                    message = CnstModuleControl.ModuleControlId + " " + ResponseConstants.Mandatory;
                }
                ModuleControl proxyResponse = new ModuleControl();
                proxyResponse = reqObjects.modulecontrols[idx];
                proxyResponse.message = message;
                response.modulecontrols[idx] = proxyResponse;
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
        public modulecontrol[] ProcessProxyToEntity(ModuleControlRequest reqObjects, int UserId)
        {
            modulecontrol[] entityObects = new modulecontrol[reqObjects.modulecontrols.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.modulecontrols.Length; idx++)
                {
                    modulecontrol entityObect = new modulecontrol();
                    entityObect.ModuleCode = reqObjects.modulecontrols[idx].ModuleCode == null ? "" : reqObjects.modulecontrols[idx].ModuleCode.Trim();
                    entityObect.BackDate = reqObjects.modulecontrols[idx].BackDate == null ? 0 : Convert.ToInt32(reqObjects.modulecontrols[idx].BackDate.Trim());
                    entityObect.From = reqObjects.modulecontrols[idx].From == null ? DateTime.Now : Convert.ToDateTime(reqObjects.modulecontrols[idx].From.Trim());
                    entityObect.FutureDate = reqObjects.modulecontrols[idx].FutureDate == null ? 0 : Convert.ToInt32(reqObjects.modulecontrols[idx].FutureDate.Trim());
                    entityObect.Stats = reqObjects.modulecontrols[idx].Stats == null ? 0 : Convert.ToInt32(reqObjects.modulecontrols[idx].Stats.Trim());
                    entityObect.TO = reqObjects.modulecontrols[idx].TO == null ? DateTime.Now : Convert.ToDateTime(reqObjects.modulecontrols[idx].TO.Trim());
                    entityObect.ModuleControlId = reqObjects.modulecontrols[idx].Id == null ? 0 : reqObjects.modulecontrols[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.modulecontrols[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.modulecontrols[idx].action == "D")
                    {
                        entityObect.RecordStatus = 1;
                    }
                    entityObects[idx] = entityObect;
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
        public bool CheckTheDataExistance(modulecontrol entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageModuleControl spParams = new sp_manageModuleControl();
                spParams.modBackDate = entityObject.BackDate;
                spParams.modCode = entityObject.ModuleCode;
                spParams.modFromDate = entityObject.From;
                spParams.modFutureDate = entityObject.FutureDate;
                spParams.modStatus = entityObject.Stats;
                spParams.modToDate = entityObject.TO;
                spParams.modId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageModuleControl");
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    result = true;
                }

                DO.EndTRansaction();


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
        public DataSet GetTheData(modulecontrol entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageModuleControl spParams = new sp_manageModuleControl();
                spParams.modBackDate = entityObject.BackDate;
                spParams.modCode = entityObject.ModuleCode;
                spParams.modFromDate = entityObject.From;
                spParams.modFutureDate = entityObject.FutureDate;
                spParams.modStatus = entityObject.Stats;
                spParams.modToDate = entityObject.TO;
                spParams.modId = entityObject.ModuleControlId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageModuleControl");
                DO.EndTRansaction();
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
            return ds;
        }
        public int UpdateTheData(modulecontrol entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageModuleControl spParams = new sp_manageModuleControl();
                spParams.modBackDate = entityObject.BackDate;
                spParams.modCode = entityObject.ModuleCode;
                spParams.modFromDate = entityObject.From;
                spParams.modFutureDate = entityObject.FutureDate;
                spParams.modStatus = entityObject.Stats;
                spParams.modToDate = entityObject.TO;
                spParams.modId = entityObject.ModuleControlId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageModuleControl");
                DO.EndTRansaction();
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
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
        public int DeleteTheData(modulecontrol entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageModuleControl spParams = new sp_manageModuleControl();
                spParams.modBackDate = entityObject.BackDate;
                spParams.modCode = entityObject.ModuleCode;
                spParams.modFromDate = entityObject.From;
                spParams.modFutureDate = entityObject.FutureDate;
                spParams.modStatus = entityObject.Stats;
                spParams.modToDate = entityObject.TO;
                spParams.modId = entityObject.ModuleControlId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageModuleControl");
                DO.EndTRansaction();
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
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
        public int ProcessInsertEntity(modulecontrol entityObject)
        {
            int result = 0;
            string TableName = "modulecontrol";
            string skipAttributes = "ModuleControlId,CreatedDate,ModifiedDate,";
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                DO.BeginTRansaction();
                entityObject.RecordStatus = 0;
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
        public ModuleControlResponse processResponseToProxy(ModuleControlResponse response, DataSet ds, string tui, string signature, string message, string action)
        {
            try
            {
                if (action != "S")
                {
                    response = processResponseToProxy(response, tui, signature, message, action);
                }
                else
                {
                    response = processResponseToProxy(response, ds, tui, signature, message);
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
        private ModuleControlResponse processResponseToProxy(ModuleControlResponse response, string tui, string signature, string message, string action)
        {
            try
            {

                foreach (ModuleControl modt in response.modulecontrols)
                {
                    if (modt.message != "")
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
        private ModuleControlResponse processResponseToProxy(ModuleControlResponse response, DataSet ds, string tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.modulecontrols = new ModuleControl[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ModuleControl DD = new ModuleControl();
                        DD.BackDate = dr[CnstModuleControl.BackDate].ToString();
                        DD.From = dr[CnstModuleControl.From].ToString();
                        DD.FutureDate = dr[CnstModuleControl.FutureDate].ToString();
                        DD.ModuleCode = dr[CnstModuleControl.ModuleCode].ToString();
                        DD.Stats = dr[CnstModuleControl.Stats].ToString();
                        DD.TO = dr[CnstModuleControl.TO].ToString();
                        DD.Id = getEncryptData(dr[CnstModuleControl.ModuleControlId].ToString(), DBConstants.PrimaryKey);
                        response.modulecontrols[idx] = DD;
                        idx++;
                    }
                    response.code = ResponseConstants.OK.ToString();
                    response.message = ResponseConstants.Success;
                    response.signature = signature;
                    response.tui = tui;
                }
                else
                {
                    response.code = ResponseConstants.NotOK.ToString();
                    if (message == null || message == "")
                    {
                        response.message = "Getting ModuleControl has " + ResponseConstants.Fail;
                    }
                    else
                    {
                        response.message = message;
                    }
                    response.signature = signature;
                    response.tui = tui;
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
    }
}