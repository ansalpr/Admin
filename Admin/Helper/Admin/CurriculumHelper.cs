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
    public class CurriculumHelper : BaseHelper
    {
        public CurriculumResponse ValidateRequest(CurriculumRequest reqObjects)
        {
            CurriculumResponse response = new CurriculumResponse();
            response.Curriculums = new Curriculum[reqObjects.Curriculums.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Curriculums.Length; idx++)
            {
                if (reqObjects.Curriculums == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.Curriculums[idx].Code == null || reqObjects.Curriculums[idx].Code == "") && (reqObjects.Curriculums[idx].Action.ToUpper() == "A" || reqObjects.Curriculums[idx].Action.ToUpper() == "E"))
                {
                    message = "Code " + ResponseConstants.Mandatory;
                }
                else if ((reqObjects.Curriculums[idx].Name == null || reqObjects.Curriculums[idx].Name == "") && (reqObjects.Curriculums[idx].Action.ToUpper() == "A" || reqObjects.Curriculums[idx].Action.ToUpper() == "E"))
                {
                    message = "Name " + ResponseConstants.Mandatory;
                }
                else if ((reqObjects.Curriculums[idx].Id == null || reqObjects.Curriculums[idx].Id == "") && (reqObjects.Curriculums[idx].Action.ToUpper() == "E" || reqObjects.Curriculums[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Curriculum proxyResponse = new Curriculum();
                proxyResponse = reqObjects.Curriculums[idx];
                proxyResponse.Message = message;
                response.Curriculums[idx] = proxyResponse;
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
        public curriculum[] ProcessProxyToEntity(CurriculumRequest reqObjects, int UserId)
        {
            curriculum[] entityObects = new curriculum[reqObjects.Curriculums.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Curriculums.Length; idx++)
                {
                    curriculum entityObect = new curriculum();
                    entityObect.CurriculumCode = reqObjects.Curriculums[idx].Code == null ? "" : reqObjects.Curriculums[idx].Code.Trim();
                    entityObect.CurriculumName = reqObjects.Curriculums[idx].Name == null ? "" : reqObjects.Curriculums[idx].Name.Trim();
                    entityObect.CurriculumId = reqObjects.Curriculums[idx].Id == null ? 0 : reqObjects.Curriculums[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Curriculums[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Curriculums[idx].Action == "D")
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
        public bool CheckTheDataExistance(curriculum entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurriculum spParams = new sp_manageCurriculum();
                spParams.CurName = entityObject.CurriculumName;
                spParams.CurCode = entityObject.CurriculumCode;
                spParams.CurId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurriculum");
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
        public DataSet GetTheData(curriculum entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurriculum spParams = new sp_manageCurriculum();
                spParams.CurName = entityObject.CurriculumName;
                spParams.CurCode = entityObject.CurriculumCode;
                spParams.CurId = entityObject.CurriculumId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurriculum");
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
        public int UpdateTheData(curriculum entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurriculum spParams = new sp_manageCurriculum();
                spParams.CurName = entityObject.CurriculumName;
                spParams.CurCode = entityObject.CurriculumCode;
                spParams.CurId = entityObject.CurriculumId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurriculum");
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
        public int DeleteTheData(curriculum entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurriculum spParams = new sp_manageCurriculum();
                spParams.CurName = entityObject.CurriculumName;
                spParams.CurCode = entityObject.CurriculumCode;
                spParams.CurId = entityObject.CurriculumId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurriculum");
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
        public int ProcessInsertEntity(curriculum entityObject)
        {
            int result = 0;
            string TableName = "curriculum";
            string skipAttributes = "CurriculumId,CreatedDate,ModifiedDate,";
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
        public CurriculumResponse processResponseToProxy(CurriculumResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private CurriculumResponse processResponseToProxy(CurriculumResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Curriculum dept in response.Curriculums)
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
        private CurriculumResponse processResponseToProxy(CurriculumResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Curriculums = new Curriculum[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Curriculum DD = new Curriculum();
                        DD.Name = dr[CnstCurriculum.CurriculumName].ToString();
                        DD.Code = dr[CnstCurriculum.CurriculumCode].ToString();
                        DD.Id = getEncryptData(dr[CnstCurriculum.CurriculumId].ToString(), DBConstants.PrimaryKey);
                        response.Curriculums[idx] = DD;
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
                        response.Message = "Getting Curriculum has " + ResponseConstants.Fail;
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