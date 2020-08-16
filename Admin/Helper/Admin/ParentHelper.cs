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
            response.parents = new Parent[reqObjects.parents.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.parents.Length; idx++)
            {
                if (reqObjects.parents == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.parents[idx].action.ToUpper() == "A" || reqObjects.parents[idx].action.ToUpper() == "E"))
                {
                    if ((reqObjects.parents[idx].Country == null || reqObjects.parents[idx].Country == ""))
                    {
                        message = CnstParent.Country + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.parents[idx].DOB == null || reqObjects.parents[idx].DOB == ""))
                    {
                        message = CnstParent.DOB + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.parents[idx].MotherTongue == null || reqObjects.parents[idx].MotherTongue == ""))
                    {
                        message = CnstParent.MotherTongue + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.parents[idx].Name == null || reqObjects.parents[idx].Name == ""))
                    {
                        message = CnstParent.ParentName + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.parents[idx].POB == null || reqObjects.parents[idx].POB == ""))
                    {
                        message = CnstParent.POB + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.parents[idx].Address1 == null || reqObjects.parents[idx].Address1 == ""))
                    {
                        message = CnstParent.Address1 + " " + ResponseConstants.Mandatory;
                    }
                    else
                    {
                        if (!validateDateFormat(reqObjects.parents[idx].DOB))
                        {
                            message = ResponseConstants.InValid + " " + CnstParent.DOB;
                        }
                    }
                }
                else if ((reqObjects.parents[idx].Id == null || reqObjects.parents[idx].Id == "") && (reqObjects.parents[idx].action.ToUpper() == "E" || reqObjects.parents[idx].action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Parent proxyResponse = new Parent();
                proxyResponse = reqObjects.parents[idx];
                proxyResponse.message = message;
                response.parents[idx] = proxyResponse;
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
        public parent[] ProcessProxyToEntity(ParentRequest reqObjects, int UserId)
        {
            parent[] entityObects = new parent[reqObjects.parents.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.parents.Length; idx++)
                {
                    parent entityObect = new parent();
                    entityObect.Address1 = reqObjects.parents[idx].Address1 == null ? "" : reqObjects.parents[idx].Address1.Trim();
                    entityObect.Address2 = reqObjects.parents[idx].Address2 == null ? "" : reqObjects.parents[idx].Address2.Trim();
                    entityObect.BloodGroup = reqObjects.parents[idx].BloodGroup == null ? "" : reqObjects.parents[idx].BloodGroup.Trim();
                    entityObect.Country = reqObjects.parents[idx].Country == null ? "" : reqObjects.parents[idx].Country.Trim();
                    entityObect.DOB = reqObjects.parents[idx].DOB == null ? DateTime.Now :  Convert.ToDateTime(reqObjects.parents[idx].DOB.Trim());
                    entityObect.ParentName = reqObjects.parents[idx].Name == null ? "" : reqObjects.parents[idx].Name.Trim();
                    entityObect.POB = reqObjects.parents[idx].POB == null ? "" : reqObjects.parents[idx].POB.Trim();                   
                    entityObect.ParentId = reqObjects.parents[idx].Id == null ? 0 : reqObjects.parents[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.parents[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.parents[idx].action == "D")
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
                spParams.parBloodGroup = entityObject.BloodGroup;
                spParams.parCountry = entityObject.Country;
                spParams.parDOB = entityObject.DOB;
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
                spParams.parBloodGroup = entityObject.BloodGroup;
                spParams.parCountry = entityObject.Country;
                spParams.parDOB = entityObject.DOB;
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
                spParams.parBloodGroup = entityObject.BloodGroup;
                spParams.parCountry = entityObject.Country;
                spParams.parDOB = entityObject.DOB;
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
                spParams.parBloodGroup = entityObject.BloodGroup;
                spParams.parCountry = entityObject.Country;
                spParams.parDOB = entityObject.DOB;
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
        public ParentResponse processResponseToProxy(ParentResponse response, DataSet ds, string tui, string signature, string message, string action)
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
        private ParentResponse processResponseToProxy(ParentResponse response, string tui, string signature, string message, string action)
        {
            try
            {

                foreach (Parent dept in response.parents)
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
        private ParentResponse processResponseToProxy(ParentResponse response, DataSet ds, string tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.parents = new Parent[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Parent DD = new Parent();
                        DD.Name = dr[CnstParent.ParentName].ToString();
                        DD.Address1 = dr[CnstParent.Address1].ToString();
                        DD.Address2 = dr[CnstParent.Address2].ToString();
                        DD.BloodGroup = dr[CnstParent.BloodGroup].ToString();
                        DD.Country = dr[CnstParent.Country].ToString();
                        DD.DOB = dr[CnstParent.DOB].ToString();
                        DD.MotherTongue = dr[CnstParent.MotherTongue].ToString();
                        DD.Name = dr[CnstParent.ParentName].ToString();
                        DD.POB = dr[CnstParent.POB].ToString();
                        DD.Id = getEncryptData(dr[CnstParent.ParentId].ToString(), DBConstants.PrimaryKey);
                        response.parents[idx] = DD;
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
                        response.message = "Getting Parent has " + ResponseConstants.Fail;
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