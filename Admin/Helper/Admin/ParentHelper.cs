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
    public class ParentHelper : BaseHelper
    {
        public ParentResponse ValidateRequest(ParentRequest reqObjects)
        {
            ParentResponse response = new ParentResponse();
            response.Parents = new Parent[reqObjects.Parents.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Parents.Length; idx++)
            {
                if (reqObjects.Parents == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.Parents[idx].Action.ToUpper() == "A" || reqObjects.Parents[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.Parents[idx].CountryCode == null || reqObjects.Parents[idx].CountryCode == ""))
                    {
                        message = CnstParent.CountryCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].BloodGroupCode == null || reqObjects.Parents[idx].BloodGroupCode == ""))
                    {
                        message = CnstParent.BloodGroupCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].StateCode == null || reqObjects.Parents[idx].StateCode == ""))
                    {
                        message = CnstParent.StateCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].DOB == null || reqObjects.Parents[idx].DOB == ""))
                    {
                        message = CnstParent.DOB + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].MotherTongue == null || reqObjects.Parents[idx].MotherTongue == ""))
                    {
                        message = CnstParent.MotherTongue + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].Name == null || reqObjects.Parents[idx].Name == ""))
                    {
                        message = CnstParent.ParentName + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].POB == null || reqObjects.Parents[idx].POB == ""))
                    {
                        message = CnstParent.POB + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Parents[idx].Address1 == null || reqObjects.Parents[idx].Address1 == ""))
                    {
                        message = CnstParent.Address1 + " " + ResponseConstants.Mandatory;
                    }
                    else
                    {
                        if (!validateDateFormat(reqObjects.Parents[idx].DOB))
                        {
                            message = ResponseConstants.InValid + " " + CnstParent.DOB;
                        }
                    }
                }
                else if ((reqObjects.Parents[idx].Id == null || reqObjects.Parents[idx].Id == "") && (reqObjects.Parents[idx].Action.ToUpper() == "E" || reqObjects.Parents[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Parent proxyResponse = new Parent();
                proxyResponse = reqObjects.Parents[idx];
                proxyResponse.Message = message;
                response.Parents[idx] = proxyResponse;
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
        public parent[] ProcessProxyToEntity(ParentRequest reqObjects, int UserId)
        {
            parent[] entityObects = new parent[reqObjects.Parents.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Parents.Length; idx++)
                {
                    parent entityObect = new parent();
                    entityObect.Address1 = reqObjects.Parents[idx].Address1 == null ? "" : reqObjects.Parents[idx].Address1.Trim();
                    entityObect.Address2 = reqObjects.Parents[idx].Address2 == null ? "" : reqObjects.Parents[idx].Address2.Trim();
                    entityObect.BloodGroupCode = reqObjects.Parents[idx].BloodGroupCode == null ? "" : reqObjects.Parents[idx].BloodGroupCode.Trim();
                    entityObect.CountryCode = reqObjects.Parents[idx].CountryCode == null ? "" : reqObjects.Parents[idx].CountryCode.Trim();
                    entityObect.StateCode = reqObjects.Parents[idx].StateCode == null ? "" : reqObjects.Parents[idx].StateCode.Trim();
                    entityObect.DOB = reqObjects.Parents[idx].DOB == null ? "" : (reqObjects.Parents[idx].DOB.Trim());
                    entityObect.ParentName = reqObjects.Parents[idx].Name == null ? "" : reqObjects.Parents[idx].Name.Trim();
                    entityObect.POB = reqObjects.Parents[idx].POB == null ? "" : reqObjects.Parents[idx].POB.Trim();                   
                    entityObect.ParentId = reqObjects.Parents[idx].Id == null ? 0 : reqObjects.Parents[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Parents[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Parents[idx].Action == "D")
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
        public bool CheckTheDataExistance(parent entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageParent spParams = new sp_manageParent();
                spParams.parAddress1 = entityObject.Address1;
                spParams.parAddress2 = entityObject.Address2;
                spParams.parBloodGroup = entityObject.BloodGroupCode;
                spParams.parCountry = entityObject.CountryCode;
                spParams.parState = entityObject.StateCode;
                spParams.parDOB = Convert.ToDateTime(entityObject.DOB).Date;
                spParams.parMotherTongue = entityObject.MotherTongue;
                spParams.parName = entityObject.ParentName;
                spParams.parPOB = entityObject.POB;
                spParams.parId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageParent");
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
        public DataSet GetTheData(parent entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageParent spParams = new sp_manageParent();
                spParams.parAddress1 = entityObject.Address1;
                spParams.parAddress2 = entityObject.Address2;
                spParams.parBloodGroup = entityObject.BloodGroupCode;
                spParams.parCountry = entityObject.CountryCode;
                spParams.parState = entityObject.StateCode;
                spParams.parDOB = Convert.ToDateTime(entityObject.DOB).Date;
                spParams.parMotherTongue = entityObject.MotherTongue;
                spParams.parName = entityObject.ParentName;
                spParams.parPOB = entityObject.POB;
                spParams.parId = entityObject.ParentId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageParent");
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
        public int UpdateTheData(parent entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageParent spParams = new sp_manageParent();
                spParams.parAddress1 = entityObject.Address1;
                spParams.parAddress2 = entityObject.Address2;
                spParams.parBloodGroup = entityObject.BloodGroupCode;
                spParams.parCountry = entityObject.CountryCode;
                spParams.parState = entityObject.StateCode;
                spParams.parDOB = Convert.ToDateTime(entityObject.DOB).Date;
                spParams.parMotherTongue = entityObject.MotherTongue;
                spParams.parName = entityObject.ParentName;
                spParams.parPOB = entityObject.POB;
                spParams.parId = entityObject.ParentId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageParent");
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
        public int DeleteTheData(parent entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageParent spParams = new sp_manageParent();
                spParams.parAddress1 = entityObject.Address1;
                spParams.parAddress2 = entityObject.Address2;
                spParams.parBloodGroup = entityObject.BloodGroupCode;
                spParams.parCountry = entityObject.CountryCode;
                spParams.parState = entityObject.StateCode;
                spParams.parDOB = Convert.ToDateTime(entityObject.DOB).Date;
                spParams.parMotherTongue = entityObject.MotherTongue;
                spParams.parName = entityObject.ParentName;
                spParams.parPOB = entityObject.POB;
                spParams.parId = entityObject.ParentId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageParent");
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
        public int ProcessInsertEntity(parent entityObject)
        {
            int result = 0;
            string TableName = "class";
            string skipAttributes = "ParentId,CreatedDate,ModifiedDate,";
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
        public ParentResponse processResponseToProxy(ParentResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private ParentResponse processResponseToProxy(ParentResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Parent dept in response.Parents)
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
        private ParentResponse processResponseToProxy(ParentResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Parents = new Parent[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Parent DD = new Parent();
                        DD.Name = dr[CnstParent.ParentName].ToString();
                        DD.Address1 = dr[CnstParent.Address1].ToString();
                        DD.Address2 = dr[CnstParent.Address2].ToString();
                        DD.BloodGroupCode = dr[CnstParent.BloodGroupCode].ToString();
                        DD.CountryCode = dr[CnstParent.CountryCode].ToString();
                        DD.StateCode = dr[CnstParent.StateCode].ToString();
                        DD.DOB = dr[CnstParent.DOB].ToString();
                        DD.MotherTongue = dr[CnstParent.MotherTongue].ToString();
                        DD.Name = dr[CnstParent.ParentName].ToString();
                        DD.POB = dr[CnstParent.POB].ToString();
                        DD.Id = getEncryptData(dr[CnstParent.ParentId].ToString(), DBConstants.PrimaryKey);
                        response.Parents[idx] = DD;
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
                        response.Message = "Getting Parent has " + ResponseConstants.Fail;
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