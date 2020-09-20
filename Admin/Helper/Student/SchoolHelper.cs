using Admin.Base;
using Admin.Constants.Table;
using Admin.Constants.Table.Student;
using Admin.Models.Student;
using API.Base;
using DataLayer;
using EntityLayer.StoredProcedures.Student;
using EntityLayer.Tables.Student;
using GeneralLayer;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Admin.Helper
{
    public class SchoolHelper : BaseHelper
    {
        public SchoolResponse ValidateRequest(SchoolRequest reqObjects)
        {
            SchoolResponse response = new SchoolResponse();
            response.School = new School[reqObjects.School.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.School.Length; idx++)
            {
                if (reqObjects.School == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.School[idx].Action.ToUpper() == "A" || reqObjects.School[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.School[idx].Code == null || reqObjects.School[idx].Code == ""))
                    {
                        message = CnstSchool.SchoolCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.School[idx].Name == null || reqObjects.School[idx].Name == ""))
                    {
                        message = CnstSchool.SchoolName + " " + ResponseConstants.Mandatory;
                    }                    
                }
                else if ((reqObjects.School[idx].Id == null || reqObjects.School[idx].Id == "") && (reqObjects.School[idx].Action.ToUpper() == "E" || reqObjects.School[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                School proxyResponse = new School();
                proxyResponse = reqObjects.School[idx];
                proxyResponse.Message = message;
                response.School[idx] = proxyResponse;
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
        public school[] ProcessProxyToEntity(SchoolRequest reqObjects, int UserId)
        {
            school[] entityObects = new school[reqObjects.School.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.School.Length; idx++)
                {
                    school entityObect = new school();
                    entityObect.SchoolCode = reqObjects.School[idx].Code == null ? "" : reqObjects.School[idx].Code.Trim();
                    entityObect.SchoolName = reqObjects.School[idx].Name == null ? "" : reqObjects.School[idx].Name.Trim();
                    entityObect.SchoolAddress1 = reqObjects.School[idx].Address1 == null ? "" : reqObjects.School[idx].Address1.Trim();
                    entityObect.SchoolAddress2 = reqObjects.School[idx].Address2 == null ? "" : reqObjects.School[idx].Address2.Trim();
                    entityObect.Phone = reqObjects.School[idx].Phone == null ? "" : reqObjects.School[idx].Phone.Trim();
                    entityObect.Fax = reqObjects.School[idx].Fax == null ? "" : reqObjects.School[idx].Fax.Trim();
                    entityObect.Email = reqObjects.School[idx].Email == null ? "" : reqObjects.School[idx].Email.Trim();
                    entityObect.SchoolId = reqObjects.School[idx].Id == null ? 0 : reqObjects.School[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.School[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.School[idx].Action == "D")
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
        public bool CheckTheDataExistance(school entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageSchool spParams = new sp_manageSchool();
                spParams.schName = entityObject.SchoolName;
                spParams.schCode = entityObject.SchoolCode;
                spParams.schAddress1 = entityObject.SchoolAddress1;
                spParams.schAddress2 = entityObject.SchoolAddress2;
                spParams.schPhone = entityObject.Phone;
                spParams.schEmail = entityObject.Email;
                spParams.schFax = entityObject.Fax;
                spParams.schId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageSchool");
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
        public DataSet GetTheData(school entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageSchool spParams = new sp_manageSchool();
                spParams.schName = entityObject.SchoolName;
                spParams.schCode = entityObject.SchoolCode;
                spParams.schAddress1 = entityObject.SchoolAddress1;
                spParams.schAddress2 = entityObject.SchoolAddress2;
                spParams.schPhone = entityObject.Phone;
                spParams.schEmail = entityObject.Email;
                spParams.schFax = entityObject.Fax;
                spParams.schId = entityObject.SchoolId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageSchool");
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
        public int UpdateTheData(school entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageSchool spParams = new sp_manageSchool();
                spParams.schName = entityObject.SchoolName;
                spParams.schCode = entityObject.SchoolCode;
                spParams.schAddress1 = entityObject.SchoolAddress1;
                spParams.schAddress2 = entityObject.SchoolAddress2;
                spParams.schPhone = entityObject.Phone;
                spParams.schEmail = entityObject.Email;
                spParams.schFax = entityObject.Fax;
                spParams.schId = entityObject.SchoolId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageSchool");
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
        public int DeleteTheData(school entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageSchool spParams = new sp_manageSchool();
                spParams.schName = entityObject.SchoolName;
                spParams.schCode = entityObject.SchoolCode;
                spParams.schAddress1 = entityObject.SchoolAddress1;
                spParams.schAddress2 = entityObject.SchoolAddress2;
                spParams.schPhone = entityObject.Phone;
                spParams.schEmail = entityObject.Email;
                spParams.schFax = entityObject.Fax;
                spParams.schId = entityObject.SchoolId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageSchool");
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
        public int ProcessInsertEntity(school entityObject)
        {
            int result = 0;
            string TableName = "school";
            string skipAttributes = "SchoolId,CreatedDate,ModifiedDate,";
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
        public SchoolResponse processResponseToProxy(SchoolResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private SchoolResponse processResponseToProxy(SchoolResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (School dept in response.School)
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
        private SchoolResponse processResponseToProxy(SchoolResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.School = new School[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        School DD = new School();
                        DD.Name = dr[CnstSchool.SchoolName].ToString();
                        DD.Code = dr[CnstSchool.SchoolCode].ToString();
                        DD.Address1 = dr[CnstSchool.SchoolAddress1].ToString();
                        DD.Address2 = dr[CnstSchool.SchoolAddress2].ToString();
                        DD.Phone = dr[CnstSchool.Phone].ToString();
                        DD.Email = dr[CnstSchool.Email].ToString();
                        DD.Fax = dr[CnstSchool.Fax].ToString();                      
                        DD.Id = getEncryptData(dr[CnstSchool.SchoolId].ToString(), DBConstants.PrimaryKey);
                        response.School[idx] = DD;
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
                        response.Message = "Getting School has " + ResponseConstants.Fail;
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