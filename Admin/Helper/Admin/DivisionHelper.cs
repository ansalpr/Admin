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
            response.Divisions = new Division[reqObjects.Divisions.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Divisions.Length; idx++)
            {
                if (reqObjects.Divisions == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.Divisions[idx].Action.ToUpper() == "A" || reqObjects.Divisions[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.Divisions[idx].Code == null || reqObjects.Divisions[idx].Code == ""))
                    {
                        message = CnstDivision.DivisionCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Divisions[idx].Name == null || reqObjects.Divisions[idx].Name == ""))
                    {
                        message = CnstDivision.DivisionName + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Divisions[idx].ClassCode == null || reqObjects.Divisions[idx].ClassCode == ""))
                    {
                        message = CnstDivision.ClassCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Divisions[idx].Stats == null || reqObjects.Divisions[idx].Stats == ""))
                    {
                        message = CnstDivision.Stats + " " + ResponseConstants.Mandatory;
                    }

                }
                else if ((reqObjects.Divisions[idx].Id == null || reqObjects.Divisions[idx].Id == "") && (reqObjects.Divisions[idx].Action.ToUpper() == "E" || reqObjects.Divisions[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Division proxyResponse = new Division();
                proxyResponse = reqObjects.Divisions[idx];
                proxyResponse.Message = message;
                response.Divisions[idx] = proxyResponse;
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
        public division[] ProcessProxyToEntity(DivisionRequest reqObjects, int UserId)
        {
            division[] entityObects = new division[reqObjects.Divisions.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Divisions.Length; idx++)
                {
                    division entityObect = new division();
                    entityObect.DivisionCode = reqObjects.Divisions[idx].Code == null ? "" : reqObjects.Divisions[idx].Code.Trim();
                    entityObect.DivisionName = reqObjects.Divisions[idx].Name == null ? "" : reqObjects.Divisions[idx].Name.Trim();
                    entityObect.ClassCode = reqObjects.Divisions[idx].ClassCode == null ? "" : reqObjects.Divisions[idx].ClassCode.Trim();
                    entityObect.Stats = reqObjects.Divisions[idx].Stats == null ? "" : reqObjects.Divisions[idx].Stats.Trim();
                    entityObect.DivisionId = reqObjects.Divisions[idx].Id == null ? 0 : reqObjects.Divisions[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Divisions[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Divisions[idx].Action == "D")
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
        public DivisionResponse processResponseToProxy(DivisionResponse response, DataSet ds, string Tui, string signature, string message, string action)
        {
            try
            {
                if (action != "S")
                {
                    response = processResponseToProxy(response, Tui, signature, message, action);
                }
                else
                {
                    response = processResponseToProxy(response, ds, Tui, signature, message);
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
        private DivisionResponse processResponseToProxy(DivisionResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Division dept in response.Divisions)
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
        private DivisionResponse processResponseToProxy(DivisionResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Divisions = new Division[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Division DD = new Division();
                        DD.Code = dr[CnstDivision.DivisionCode].ToString();
                        DD.ClassCode = dr[CnstDivision.ClassCode].ToString();
                        DD.Name = dr[CnstDivision.DivisionName].ToString();
                        DD.Stats = dr[CnstDivision.Stats].ToString();
                        DD.Id = getEncryptData(dr[CnstDivision.DivisionId].ToString(), DBConstants.PrimaryKey);
                        response.Divisions[idx] = DD;
                        idx++;
                    }
                    response.Code = ResponseConstants.OK.ToString();
                    response.Message = ResponseConstants.Success;
                    response.Signature = signature;
                    response.Tui = Tui;
                }
                else
                {
                    response.Code = ResponseConstants.NotOK.ToString();
                    if (message == null || message == "")
                    {
                        response.Message = "Getting Division has " + ResponseConstants.Fail;
                    }
                    else
                    {
                        response.Message = message;
                    }
                    response.Signature = signature;
                    response.Tui = Tui;
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