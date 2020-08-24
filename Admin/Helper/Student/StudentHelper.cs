using Admin.Base;
using Admin.Constants.Table;
using Admin.Models.Admin;
using Admin.Models.Student;
using API.Base;
using DataLayer;
using EntityLayer.StoredProcedures.Student;
using EntityLayer.Tables.Admin;
using EntityLayer.Tables.Json;
using EntityLayer.Tables.Student;
using GeneralLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Admin.Helper
{
    public class StudentHelper : BaseHelper
    {
        public StudentResponse ValidateRequest(StudentRequest reqObjects)
        {
            StudentResponse response = new StudentResponse();
            response.Students = new Student[reqObjects.Students.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Students.Length; idx++)
            {
                reqObjects.Students[idx].SiNo = (idx + 1).ToString();
                message = "";
                if (reqObjects.Students == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else
                {
                    if ((reqObjects.Students[idx].Action.ToUpper() == "A" || reqObjects.Students[idx].Action.ToUpper() == "E"))
                    {
                        if ((reqObjects.Students[idx].Address1 == null || reqObjects.Students[idx].Address1 == ""))
                        {
                            message = CnstStudent.Address1 + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].BloodGroupCode == null || reqObjects.Students[idx].BloodGroupCode == ""))
                        {
                            message = CnstStudent.BloodGroupCode + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].CountryCode == null || reqObjects.Students[idx].CountryCode == ""))
                        {
                            message = CnstStudent.CountryCode + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].DOB == null || reqObjects.Students[idx].DOB == ""))
                        {
                            message = CnstStudent.DOB + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].FirstName == null || reqObjects.Students[idx].FirstName == ""))
                        {
                            message = CnstStudent.FirstName + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].Gender == null || reqObjects.Students[idx].Gender == ""))
                        {
                            message = CnstStudent.Gender + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].LastName == null || reqObjects.Students[idx].LastName == ""))
                        {
                            message = CnstStudent.LastName + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].MotherTongue == null || reqObjects.Students[idx].MotherTongue == ""))
                        {
                            message = CnstStudent.MotherTongue + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].POB == null || reqObjects.Students[idx].POB == ""))
                        {
                            message = CnstStudent.POB + " " + ResponseConstants.Mandatory;
                        }
                        else if ((reqObjects.Students[idx].StateCode == null || reqObjects.Students[idx].StateCode == ""))
                        {
                            message = CnstStudent.StateCode + " " + ResponseConstants.Mandatory;
                        }
                        else
                        {
                            if (!validateDateFormat(reqObjects.Students[idx].DOB))
                            {
                                message = ResponseConstants.InValid + " " + CnstStudent.DOB;
                            }
                        }
                        if (reqObjects.Students[idx].parents != null)
                        {
                            string childMsg = "";
                            for (int parCount = 0; parCount < reqObjects.Students[idx].parents.Length; parCount++)
                            {
                                reqObjects.Students[idx].parents[parCount].StudentRef = reqObjects.Students[idx].SiNo;
                                if ((reqObjects.Students[idx].parents[parCount].MotherTongue == null || reqObjects.Students[idx].parents[parCount].MotherTongue == ""))
                                {
                                    childMsg = CnstParent.MotherTongue + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].Name == null || reqObjects.Students[idx].parents[parCount].Name == ""))
                                {
                                    childMsg = CnstParent.ParentName + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].POB == null || reqObjects.Students[idx].parents[parCount].POB == ""))
                                {
                                    childMsg = CnstParent.POB + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].StateCode == null || reqObjects.Students[idx].parents[parCount].StateCode == ""))
                                {
                                    childMsg = CnstParent.StateCode + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].Address1 == null || reqObjects.Students[idx].parents[parCount].Address1 == ""))
                                {
                                    childMsg = CnstParent.Address1 + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].BloodGroupCode == null || reqObjects.Students[idx].parents[parCount].BloodGroupCode == ""))
                                {
                                    childMsg = CnstParent.BloodGroupCode + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].CountryCode == null || reqObjects.Students[idx].parents[parCount].CountryCode == ""))
                                {
                                    childMsg = CnstParent.CountryCode + " " + ResponseConstants.Mandatory;
                                }
                                else if ((reqObjects.Students[idx].parents[parCount].DOB == null || reqObjects.Students[idx].parents[parCount].DOB == ""))
                                {
                                    childMsg = CnstParent.DOB + " " + ResponseConstants.Mandatory;
                                }
                                else
                                {
                                    if (!validateDateFormat(reqObjects.Students[idx].parents[parCount].DOB))
                                    {
                                        childMsg = ResponseConstants.InValid + " " + CnstParent.DOB;
                                    }
                                }

                                if (childMsg != "")
                                {
                                    if (message == "")
                                    {
                                        reqObjects.Students[idx].Message = "Invalid Request";
                                    }
                                    reqObjects.Students[idx].Status = "F";
                                    reqObjects.Students[idx].parents[parCount].Status = "F";
                                    reqObjects.Students[idx].parents[parCount].Message = childMsg;
                                    childMsg = "";
                                }
                            }
                        }
                        if (message != "")
                        {
                            reqObjects.Students[idx].Status = "F";
                            reqObjects.Students[idx].Message = message;
                            message = "";
                        }

                    }

                    if ((reqObjects.Students[idx].Id == null || reqObjects.Students[idx].Id == "") && (reqObjects.Students[idx].Action.ToUpper() == "E" || reqObjects.Students[idx].Action.ToUpper() == "D"))
                    {
                        message = "Id " + ResponseConstants.Mandatory;
                    }
                }

                Student proxyResponse = new Student();
                proxyResponse = reqObjects.Students[idx];
                if (message != "")
                {
                    proxyResponse.Message = message;
                }
                response.Students[idx] = proxyResponse;
            }
            response.Tui = reqObjects.Tui;
            if (response.Students.Where(x => x.Status != "F").ToArray().Length > 0)
            {
                response.Code = ResponseConstants.OK.ToString();
            }
            else
            {
                response.Code = ResponseConstants.NotOK.ToString();
            }
            return response;
        }
        public StudentRequest ProxyRequestReferenceSettings(StudentRequest reqObjects)
        {

            for (int idx = 0; idx < reqObjects.Students.Length; idx++)
            {
                reqObjects.Students[idx].SiNo = (idx + 1).ToString();
                foreach(Parent pr in reqObjects.Students[idx].parents)
                {
                    pr.StudentRef = reqObjects.Students[idx].SiNo;
                }
            }
            return reqObjects;
        }      
        public JStudents[] ProcessProxyToJsonEntity(StudentRequest reqObjects, int UserId)
        {
            JStudents[] entityObects = new JStudents[reqObjects.Students.Where(x => x.Status != "F").ToArray().Length];
            Student[] objStudent = reqObjects.Students.Where(x => x.Status != "F").ToArray();
            try
            {
                for (int idx = 0; idx < objStudent.Length; idx++)
                {
                    JStudents entityObect = new JStudents();
                    entityObect.action = objStudent[idx].Action;
                    entityObect.SiNo = Convert.ToInt32(objStudent[idx].SiNo);
                    entityObect.Address1 = objStudent[idx].Address1 == null ? "" : objStudent[idx].Address1.Trim();
                    entityObect.Address2 = objStudent[idx].Address2 == null ? "" : objStudent[idx].Address2.Trim();
                    entityObect.AdmissionNo = objStudent[idx].AdmissionNo == null ? "" : objStudent[idx].AdmissionNo.Trim();
                    entityObect.BloodGroupCode = objStudent[idx].BloodGroupCode == null ? "" : objStudent[idx].BloodGroupCode.Trim();
                    entityObect.CountryCode = objStudent[idx].CountryCode == null ? "" : objStudent[idx].CountryCode.Trim();
                    entityObect.DOB = objStudent[idx].DOB.Trim();
                    entityObect.FatherID = (objStudent[idx].FatherID == null || objStudent[idx].FatherID == "") ? "0" : (objStudent[idx].FatherID.Trim());
                    entityObect.FirstName = objStudent[idx].FirstName == null ? "" : objStudent[idx].FirstName.Trim();
                    entityObect.Gender = objStudent[idx].Gender == null ? "" : objStudent[idx].Gender.Trim();
                    entityObect.GuardianID = (objStudent[idx].GuardianID == null || objStudent[idx].GuardianID == "") ? "0" : (objStudent[idx].GuardianID.Trim());
                    entityObect.LastName = objStudent[idx].LastName == null ? "" : objStudent[idx].LastName.Trim();
                    entityObect.MiddleName = objStudent[idx].MiddleName == null ? "" : objStudent[idx].MiddleName.Trim();
                    entityObect.MotherID = (objStudent[idx].MotherID == null || objStudent[idx].MotherID == "") ? "0" : (objStudent[idx].MotherID.Trim());
                    entityObect.MotherTongue = objStudent[idx].MotherTongue == null ? "" : objStudent[idx].MotherTongue.Trim();
                    entityObect.POB = objStudent[idx].POB == null ? "" : objStudent[idx].POB.Trim();
                    entityObect.StateCode = objStudent[idx].StateCode == null ? "" : objStudent[idx].StateCode.Trim();
                    entityObect.Stats = objStudent[idx].Stats == null ? "" : objStudent[idx].Stats.Trim();
                    entityObect.StudentId = objStudent[idx].Id == null ? "0" : objStudent[idx].Id == "" ? "0" : (getDecryptData(objStudent[idx].Id, DBConstants.PrimaryKey));
                    JParents[] centityObects = new JParents[objStudent[idx].parents.Where(x => x.Status != "F").ToArray().Length];
                    Parent[] ObjParents = objStudent[idx].parents.Where(x => x.Status != "F").ToArray();
                    for (int cidx = 0; cidx < (ObjParents == null ? 0 : ObjParents.Length); cidx++)
                    {
                        JParents centityObect = new JParents();
                        centityObect.action = ObjParents[cidx].Action;
                        centityObect.StudentRef = entityObect.SiNo;
                        centityObect.Address1 = ObjParents[cidx].Address1 == null ? "" : ObjParents[cidx].Address1.Trim();
                        centityObect.Address2 = ObjParents[cidx].Address2 == null ? "" : ObjParents[cidx].Address2.Trim();
                        centityObect.BloodGroupCode = ObjParents[cidx].BloodGroupCode == null ? "" : ObjParents[cidx].BloodGroupCode.Trim();
                        centityObect.CountryCode = ObjParents[cidx].CountryCode == null ? "" : ObjParents[cidx].CountryCode.Trim();
                        centityObect.RelationCode = ObjParents[cidx].RelationCode == null ? "" : ObjParents[cidx].RelationCode.Trim();
                        centityObect.StateCode = ObjParents[cidx].StateCode == null ? "" : ObjParents[cidx].StateCode.Trim();
                        centityObect.DOB = ObjParents[cidx].DOB == null ? "" : (ObjParents[cidx].DOB.Trim());
                        centityObect.ParentName = ObjParents[cidx].Name == null ? "" : ObjParents[cidx].Name.Trim();
                        centityObect.MotherTongue = ObjParents[cidx].MotherTongue == null ? "" : ObjParents[cidx].MotherTongue.Trim();
                        centityObect.Stats = ObjParents[cidx].Status == null ? "" : ObjParents[cidx].Status.Trim();
                        centityObect.POB = ObjParents[cidx].POB == null ? "" : ObjParents[cidx].POB.Trim();
                        centityObect.ParentId = ObjParents[cidx].Id == null ? "0" : ObjParents[cidx].Id == "" ? "0" : (getDecryptData(ObjParents[cidx].Id, DBConstants.PrimaryKey));
                        centityObects[cidx] = centityObect;
                    }
                    entityObect.Parent = centityObects;
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
        public string ProcessObjectstoJson(JStudents[] entityObjects, string TUI)
        {
            string json = "";
            try
            {
                JStudent jStudent = new JStudent();
                jStudent.TUI = "";
                jStudent.Student = entityObjects;
                json = new JavaScriptSerializer().Serialize(jStudent);
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
            return json;
        }
        public bool CheckTheDataExistance(student entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                //string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                //DataOperation DO = new DataOperation(dbCon);
                //sp_manageStudent spParams = new sp_manageStudent();
                //spParams.bldName = entityObject.StudentName;
                //spParams.bldCode = entityObject.StudentCode;
                //spParams.bldId = 0;
                //spParams.action = "select";
                //spParams.operation = "S";
                //DO.BeginTRansaction();
                //ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageStudent");
                //if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                //{
                //    result = true;
                //}

                //DO.EndTRansaction();
                result = true;

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
        public DataSet GetTheData(student entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                //string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                //DataOperation DO = new DataOperation(dbCon);
                //sp_manageStudent spParams = new sp_manageStudent();
                //spParams.bldName = entityObject.StudentName;
                //spParams.bldCode = entityObject.StudentCode;
                //spParams.bldId = entityObject.StudentId;
                //spParams.action = "select";
                //spParams.operation = "S";
                //DO.BeginTRansaction();
                //ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageStudent");
                //DO.EndTRansaction();
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
        public int UpdateTheData(student entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                //string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                //DataOperation DO = new DataOperation(dbCon);
                //sp_manageStudent spParams = new sp_manageStudent();
                //spParams.bldName = entityObject.StudentName;
                //spParams.bldCode = entityObject.StudentCode;
                //spParams.bldId = entityObject.StudentId;
                //spParams.userID = entityObject.CreatedUser;
                //spParams.action = "Edit";
                //spParams.operation = "E";
                //DO.BeginTRansaction();
                //ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageStudent");
                //DO.EndTRansaction();
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
        public int DeleteTheData(student entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                //string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                //DataOperation DO = new DataOperation(dbCon);
                //sp_manageStudent spParams = new sp_manageStudent();
                //spParams.bldName = entityObject.StudentName;
                //spParams.bldCode = entityObject.StudentCode;
                //spParams.bldId = entityObject.StudentId;
                //spParams.userID = entityObject.CreatedUser;
                //spParams.action = "Delete";
                //spParams.operation = "D";
                //DO.BeginTRansaction();
                //ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageStudent");
                //DO.EndTRansaction();
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
        public int ProcessInsertEntity(student entityObject)
        {
            int result = 0;
            string TableName = "student";
            string skipAttributes = "StudentId,CreatedDate,ModifiedDate,";
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
        public DataSet ProcessInsertJsonEntity(string jsonEntity, string userID)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_ManageStudentEntry spObject = new sp_ManageStudentEntry();
                spObject.students = jsonEntity;
                spObject.userID = Convert.ToInt32(userID);
                spObject.action = "Add";
                spObject.operation = "A";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spObject, "sp_manageStudent");
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
        public StudentResponse processResponseToProxy(StudentResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        public StudentResponse processJsonResponseToProxy(StudentResponse response, DataSet ds, string Tui, string signature, string message, string action)
        {
            try
            {
                if (action != "S")
                {
                    response = processJsonResponseToProxy(response, ds, Tui, signature, message);
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
        private StudentResponse processResponseToProxy(StudentResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Student bldt in response.Students)
                {
                    if (bldt.Message != "")
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
        private StudentResponse processResponseToProxy(StudentResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Students = new Student[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Student DD = new Student();
                        //DD.Name = dr[CnstStudent.StudentName].ToString();
                        //DD.Code = dr[CnstStudent.StudentCode].ToString();
                        DD.Id = getEncryptData(dr[CnstStudent.StudentId].ToString(), DBConstants.PrimaryKey);
                        response.Students[idx] = DD;
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
                        response.Message = "Getting Student has " + ResponseConstants.Fail;
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
        private StudentResponse processJsonResponseToProxy(StudentResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                int studentCount = 0;
                int parentCount = 0;
                if (response.Students.Where(x => x.Status != "F").ToArray().Length > 0 && ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    foreach (Student std in response.Students.Where(x => x.Status != "F"))
                    {
                       
                        DataRow[] drStudent = ds.Tables[0].Select(" SiNo = " + std.SiNo);
                        if(drStudent != null && drStudent.Length > 0)
                        {
                            if (ds.Tables.Count > 1)
                            {
                                parentCount = 0;
                                foreach (DataRow drParent in ds.Tables[1].Select(" StudentRef = " + drStudent[0]["SiNo"].ToString()))
                                {
                                    std.parents[parentCount].Status = drParent["State"].ToString();
                                    std.parents[parentCount].Message = drParent["Remarks"].ToString();
                                    std.parents[parentCount].Id = (getEncryptData(drParent["ParentId"].ToString(), DBConstants.PrimaryKey)); 
                                    parentCount += 1;
                                }
                            }
                            std.Id = (getEncryptData(drStudent[0]["StudentId"].ToString(), DBConstants.PrimaryKey));
                            std.AdmissionNo = drStudent[0]["AdmissionNo"].ToString();
                            std.Status = drStudent[0]["State"].ToString();
                            std.Message = drStudent[0]["Remarks"].ToString();
                        }                       

                        studentCount += 1;
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