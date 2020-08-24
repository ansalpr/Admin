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
    public class CountryHelper : BaseHelper
    {
        public CountryResponse ValidateRequest(CountryRequest reqObjects)
        {
            CountryResponse response = new CountryResponse();
            response.Countries = new Country[reqObjects.Countries.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Countries.Length; idx++)
            {
                if (reqObjects.Countries == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ( (reqObjects.Countries[idx].Action.ToUpper() == "A" || reqObjects.Countries[idx].Action.ToUpper() == "E"))
                {
                    if((reqObjects.Countries[idx].Code == null || reqObjects.Countries[idx].Code == "") )
                    {
                        message = "Code " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Countries[idx].Name == null || reqObjects.Countries[idx].Name == "") )
                    {
                        message = "Code " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Countries[idx].Nationality == null || reqObjects.Countries[idx].Nationality == ""))
                    {
                        message = "Nationality " + ResponseConstants.Mandatory;
                    }
                }
                else if ((reqObjects.Countries[idx].Id == null || reqObjects.Countries[idx].Id == "") && (reqObjects.Countries[idx].Action.ToUpper() == "E" || reqObjects.Countries[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Country proxyResponse = new Country();
                proxyResponse = reqObjects.Countries[idx];
                proxyResponse.Message = message;
                response.Countries[idx] = proxyResponse;
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
        public country[] ProcessProxyToEntity(CountryRequest reqObjects, int UserId)
        {
            country[] entityObects = new country[reqObjects.Countries.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Countries.Length; idx++)
                {
                    country entityObect = new country();
                    entityObect.CountryCode = reqObjects.Countries[idx].Code == null ? "" : reqObjects.Countries[idx].Code.Trim();
                    entityObect.CountryName = reqObjects.Countries[idx].Name == null ? "" : reqObjects.Countries[idx].Name.Trim();
                    entityObect.Nationality = reqObjects.Countries[idx].Nationality == null ? "" : reqObjects.Countries[idx].Nationality.Trim();
                    entityObect.CountryId = reqObjects.Countries[idx].Id == null ? 0 : reqObjects.Countries[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Countries[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Countries[idx].Action == "D")
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
        public bool CheckTheDataExistance(country entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCountry spParams = new sp_manageCountry();
                spParams.cntName = entityObject.CountryName;
                spParams.cntCode = entityObject.CountryCode;
                spParams.cntNationality = entityObject.Nationality;
                spParams.cntId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCountry");
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
        public DataSet GetTheData(country entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCountry spParams = new sp_manageCountry();
                spParams.cntName = entityObject.CountryName;
                spParams.cntCode = entityObject.CountryCode;
                spParams.cntNationality = entityObject.Nationality;
                spParams.cntId = entityObject.CountryId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCountry");
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
        public int UpdateTheData(country entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCountry spParams = new sp_manageCountry();
                spParams.cntName = entityObject.CountryName;
                spParams.cntCode = entityObject.CountryCode;
                spParams.cntNationality = entityObject.Nationality;
                spParams.cntId = entityObject.CountryId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCountry");
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
        public int DeleteTheData(country entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCountry spParams = new sp_manageCountry();
                spParams.cntName = entityObject.CountryName;
                spParams.cntCode = entityObject.CountryCode;
                spParams.cntNationality = entityObject.Nationality;
                spParams.cntId = entityObject.CountryId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCountry");
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
        public int ProcessInsertEntity(country entityObject)
        {
            int result = 0;
            string TableName = "country";
            string skipAttributes = "CountryId,CreatedDate,ModifiedDate,";
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
        public CountryResponse processResponseToProxy(CountryResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private CountryResponse processResponseToProxy(CountryResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (Country dept in response.Countries)
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
        private CountryResponse processResponseToProxy(CountryResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Countries = new Country[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Country DD = new Country();
                        DD.Name = dr[CnstCountry.CountryName].ToString();
                        DD.Code = dr[CnstCountry.CountryCode].ToString();
                        DD.Nationality = dr[CnstCountry.Nationality].ToString();
                        DD.Id = getEncryptData(dr[CnstCountry.CountryId].ToString(), DBConstants.PrimaryKey);
                        response.Countries[idx] = DD;
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
                        response.Message = "Getting Country has " + ResponseConstants.Fail;
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