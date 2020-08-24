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
    public class ClassHelper : BaseHelper
    {
        public ClassResponse ValidateRequest(ClassRequest reqObjects)
        {
            ClassResponse response = new ClassResponse();
            response.Classes = new Classe[reqObjects.Classes.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Classes.Length; idx++)
            {
                if (reqObjects.Classes == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.Classes[idx].Action.ToUpper() == "A" || reqObjects.Classes[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.Classes[idx].Code == null || reqObjects.Classes[idx].Code == ""))
                    {
                        message = CnstClass.ClassCode + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Classes[idx].Name == null || reqObjects.Classes[idx].Name == ""))
                    {
                        message = CnstClass.ClassName + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Classes[idx].Sort == null || reqObjects.Classes[idx].Sort == ""))
                    {
                        message = CnstClass.Sort + " " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Classes[idx].CurriculumCode == null || reqObjects.Classes[idx].CurriculumCode == ""))
                    {
                        message = CnstClass.CurriculumCode + " " + ResponseConstants.Mandatory;
                    }
                }
                else if ((reqObjects.Classes[idx].Id == null || reqObjects.Classes[idx].Id == "") && (reqObjects.Classes[idx].Action.ToUpper() == "E" || reqObjects.Classes[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Classe proxyResponse = new Classe();
                proxyResponse = reqObjects.Classes[idx];
                proxyResponse.Message = message;
                response.Classes[idx] = proxyResponse;
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
        public classe[] ProcessProxyToEntity(ClassRequest reqObjects, int UserId)
        {
            classe[] entityObects = new classe[reqObjects.Classes.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Classes.Length; idx++)
                {
                    classe entityObect = new classe();
                    entityObect.ClassCode = reqObjects.Classes[idx].Code == null ? "" : reqObjects.Classes[idx].Code.Trim();
                    entityObect.ClassName = reqObjects.Classes[idx].Name == null ? "" : reqObjects.Classes[idx].Name.Trim();
                    entityObect.Sort = reqObjects.Classes[idx].Sort == null ? "" : reqObjects.Classes[idx].Sort.Trim();
                    entityObect.CurriculumCode = reqObjects.Classes[idx].CurriculumCode == null ? "" : reqObjects.Classes[idx].CurriculumCode.Trim();
                    entityObect.ClassId = reqObjects.Classes[idx].Id == null ? 0 : reqObjects.Classes[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Classes[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Classes[idx].Action == "D")
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
        public bool CheckTheDataExistance(classe entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageClass spParams = new sp_manageClass();
                spParams.clsName = entityObject.ClassName;
                spParams.clsCode = entityObject.ClassCode;
                spParams.clsSort = entityObject.Sort;
                spParams.clsCurriculumCode = entityObject.CurriculumCode;
                spParams.clsId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageClass");
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
        public DataSet GetTheData(classe entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageClass spParams = new sp_manageClass();
                spParams.clsName = entityObject.ClassName;
                spParams.clsCode = entityObject.ClassCode;
                spParams.clsSort = entityObject.Sort;
                spParams.clsCurriculumCode = entityObject.CurriculumCode;
                spParams.clsId = entityObject.ClassId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageClass");
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
        public int UpdateTheData(classe entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageClass spParams = new sp_manageClass();
                spParams.clsName = entityObject.ClassName;
                spParams.clsCode = entityObject.ClassCode;
                spParams.clsSort = entityObject.Sort;
                spParams.clsCurriculumCode = entityObject.CurriculumCode;
                spParams.clsId = entityObject.ClassId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageClass");
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
        public int DeleteTheData(classe entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageClass spParams = new sp_manageClass();
                spParams.clsName = entityObject.ClassName;
                spParams.clsCode = entityObject.ClassCode;
                spParams.clsSort = entityObject.Sort;
                spParams.clsCurriculumCode = entityObject.CurriculumCode;
                spParams.clsId = entityObject.ClassId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageClass");
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
        public int ProcessInsertEntity(classe entityObject)
        {
            int result = 0;
            string TableName = "class";
            string skipAttributes = "ClassId,CreatedDate,ModifiedDate,";
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
        public ClassResponse processResponseToProxy(ClassResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private ClassResponse processResponseToProxy(ClassResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Classe dept in response.Classes)
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
        private ClassResponse processResponseToProxy(ClassResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Classes = new Classe[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Classe DD = new Classe();
                        DD.Name = dr[CnstClass.ClassName].ToString();
                        DD.Code = dr[CnstClass.ClassCode].ToString();
                        DD.Sort = dr[CnstClass.Sort].ToString();
                        DD.CurriculumCode = dr[CnstClass.CurriculumCode].ToString();
                        DD.Id = getEncryptData(dr[CnstClass.ClassId].ToString(), DBConstants.PrimaryKey);
                        response.Classes[idx] = DD;
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
                        response.Message = "Getting Class has " + ResponseConstants.Fail;
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