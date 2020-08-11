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
    public class DivisionHelper : BaseHelper
    {
        public DivisionResponse ValidateRequest(DivisionRequest reqObjects)
        {
            DivisionResponse response = new DivisionResponse();
            response.divisions = new Division[reqObjects.divisions.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.divisions.Length; idx++)
            {
                if (reqObjects.divisions == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.divisions[idx].action.ToUpper() == "A" || reqObjects.divisions[idx].action.ToUpper() == "E"))
                {
                    if ((reqObjects.divisions[idx].Code == null || reqObjects.divisions[idx].Code == ""))
                    {
                        message = CnstDivision.DivisionCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.divisions[idx].Name == null || reqObjects.divisions[idx].Name == ""))
                    {
                        message = CnstDivision.DivisionName + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.divisions[idx].ClassCode == null || reqObjects.divisions[idx].ClassCode == ""))
                    {
                        message = CnstDivision.ClassCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.divisions[idx].Stats == null || reqObjects.divisions[idx].Stats == ""))
                    {
                        message = CnstDivision.Stats + " " + ResponseConstants.Mandatory;
                    }

                }
                else if ((reqObjects.divisions[idx].Id == null || reqObjects.divisions[idx].Id == "") && (reqObjects.divisions[idx].action.ToUpper() == "E" || reqObjects.divisions[idx].action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Division proxyResponse = new Division();
                proxyResponse = reqObjects.divisions[idx];
                proxyResponse.message = message;
                response.divisions[idx] = proxyResponse;
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
        public division[] ProcessProxyToEntity(DivisionRequest reqObjects, int UserId)
        {
            division[] entityObects = new division[reqObjects.divisions.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.divisions.Length; idx++)
                {
                    division entityObect = new division();
                    entityObect.DivisionCode = reqObjects.divisions[idx].Code == null ? "" : reqObjects.divisions[idx].Code.Trim();
                    entityObect.DivisionName = reqObjects.divisions[idx].Name == null ? "" : reqObjects.divisions[idx].Name.Trim();
                    entityObect.ClassCode = reqObjects.divisions[idx].ClassCode == null ? "" : reqObjects.divisions[idx].ClassCode.Trim();
                    entityObect.Stats = reqObjects.divisions[idx].Stats == null ? "" : reqObjects.divisions[idx].Stats.Trim();
                    entityObect.DivisionId = reqObjects.divisions[idx].Id == null ? 0 : reqObjects.divisions[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.divisions[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.divisions[idx].action == "D")
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
        public bool CheckTheDataExistance(division entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageDivision spParams = new sp_manageDivision();
                spParams.divCode = entityObject.DivisionCode;
                spParams.divClassCode = entityObject.ClassCode;
                spParams.divName = entityObject.DivisionName.ToString();
                spParams.divStatus = entityObject.Stats.ToString();
                spParams.divId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageDivision");
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
        public DataSet GetTheData(division entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageDivision spParams = new sp_manageDivision();
                spParams.divCode = entityObject.DivisionCode;
                spParams.divClassCode = entityObject.ClassCode;
                spParams.divName = entityObject.DivisionName.ToString();
                spParams.divStatus = entityObject.Stats.ToString();
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageDivision");
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
        public int UpdateTheData(division entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageDivision spParams = new sp_manageDivision();
                spParams.divCode = entityObject.DivisionCode;
                spParams.divClassCode = entityObject.ClassCode;
                spParams.divName = entityObject.DivisionName.ToString();
                spParams.divStatus = entityObject.Stats.ToString();
                spParams.divId = entityObject.DivisionId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageDivision");
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
        public int DeleteTheData(division entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageDivision spParams = new sp_manageDivision();
                spParams.divCode = entityObject.DivisionCode;
                spParams.divClassCode = entityObject.ClassCode;
                spParams.divName = entityObject.DivisionName.ToString();
                spParams.divStatus = entityObject.Stats.ToString();
                spParams.divId = entityObject.DivisionId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageDivision");
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
        public int ProcessInsertEntity(division entityObject)
        {
            int result = 0;
            string TableName = "division";
            string skipAttributes = "DivisionId,CreatedDate,ModifiedDate,";
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
        public DivisionResponse processResponseToProxy(DivisionResponse response, DataSet ds, string tui, string signature, string message, string action)
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
        private DivisionResponse processResponseToProxy(DivisionResponse response, string tui, string signature, string message, string action)
        {
            try
            {

                foreach (Division dept in response.divisions)
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
        private DivisionResponse processResponseToProxy(DivisionResponse response, DataSet ds, string tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.divisions = new Division[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Division DD = new Division();
                        DD.Code = dr[CnstDivision.DivisionCode].ToString();
                        DD.ClassCode = dr[CnstDivision.ClassCode].ToString();
                        DD.Name = dr[CnstDivision.DivisionName].ToString();
                        DD.Stats = dr[CnstDivision.Stats].ToString();
                        DD.Id = getEncryptData(dr[CnstDivision.DivisionId].ToString(), DBConstants.PrimaryKey);
                        response.divisions[idx] = DD;
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
                        response.message = "Getting Division has " + ResponseConstants.Fail;
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