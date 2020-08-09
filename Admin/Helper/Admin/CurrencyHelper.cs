using Admin.Base;
using Admin.Constants.Table;
using Admin.Models.Admin;
using API.Base;
using DataLayer;
using EntityLayer.StoredProcedures.Admin;
using EntityLayer.Tables;
using EntityLayer.Tables.Admin;
using GeneralLayer;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Admin.Helper.Admin
{
    public class CurrencyHelper : BaseHelper
    {
        public currency CreateCurrencyObject(string CurrencyCode)
        {
            currency retObj = new currency();
            try
            {
                retObj.CurrencyCode = CurrencyCode;
                retObj.BaseCurrency = "";
                retObj.CurrencyId = 0;
                retObj.CurrencyName = "";
                retObj.Precisions = "";
                retObj.Stats = "";
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
            return retObj;
        }
        public CurrencyResponse ValidateRequest(CurrencyRequest reqObjects)
        {
            CurrencyResponse response = new CurrencyResponse();
            response.Currencies = new Currency[reqObjects.Currencies.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.Currencies.Length; idx++)
            {
                if (reqObjects.Currencies == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.Currencies[idx].action.ToUpper() == "A" || reqObjects.Currencies[idx].action.ToUpper() == "E"))
                {
                    if((reqObjects.Currencies[idx].Code == null || reqObjects.Currencies[idx].Code == ""))
                    {
                        message = "Code " + ResponseConstants.Mandatory;
                    }
                    else if((reqObjects.Currencies[idx].Name == null || reqObjects.Currencies[idx].Name == "") )
                    {
                        message = "Name " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Currencies[idx].BaseCurrency == null || reqObjects.Currencies[idx].BaseCurrency == ""))
                    {
                        message = "Base Currency " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Currencies[idx].Precisions == null || reqObjects.Currencies[idx].Precisions == ""))
                    {
                        message = "Precisions " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.Currencies[idx].Stats == null || reqObjects.Currencies[idx].Stats == ""))
                    {
                        message = "Stats " + ResponseConstants.Mandatory;
                    }
                }                
                else if ((reqObjects.Currencies[idx].Id == null || reqObjects.Currencies[idx].Id == "") && (reqObjects.Currencies[idx].action.ToUpper() == "E" || reqObjects.Currencies[idx].action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                Currency proxyResponse = new Currency();
                proxyResponse = reqObjects.Currencies[idx];
                proxyResponse.message = message;
                response.Currencies[idx] = proxyResponse;
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
        public currency[] ProcessProxyToEntity(CurrencyRequest reqObjects, int UserId)
        {
            currency[] entityObects = new currency[reqObjects.Currencies.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.Currencies.Length; idx++)
                {
                    currency entityObect = new currency();
                    entityObect.CurrencyCode = reqObjects.Currencies[idx].Code == null ? "" : reqObjects.Currencies[idx].Code.Trim();
                    entityObect.CurrencyName = reqObjects.Currencies[idx].Name == null ? "" : reqObjects.Currencies[idx].Name.Trim();
                    entityObect.BaseCurrency = reqObjects.Currencies[idx].BaseCurrency == null ? "" : reqObjects.Currencies[idx].BaseCurrency.Trim();
                    entityObect.Precisions = reqObjects.Currencies[idx].Precisions == null ? "" : reqObjects.Currencies[idx].Precisions.Trim();
                    entityObect.Stats = reqObjects.Currencies[idx].Stats == null ? "" : reqObjects.Currencies[idx].Stats.Trim();
                    entityObect.CurrencyId = reqObjects.Currencies[idx].Id == null ? 0 : reqObjects.Currencies[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.Currencies[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.Currencies[idx].action == "D")
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
        public bool CheckTheDataExistance(currency entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrency spParams = new sp_manageCurrency();
                spParams.curName = entityObject.CurrencyName;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curPrecision = entityObject.Precisions;
                spParams.baseCur = entityObject.BaseCurrency;
                spParams.curStatus = entityObject.Stats;
                spParams.curId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrency");
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
        public DataSet GetTheData(currency entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrency spParams = new sp_manageCurrency();
                spParams.curName = entityObject.CurrencyName;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curPrecision = entityObject.Precisions;
                spParams.baseCur = entityObject.BaseCurrency;
                spParams.curStatus = entityObject.Stats;
                spParams.curId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrency");
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
        public int UpdateTheData(currency entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrency spParams = new sp_manageCurrency();
                spParams.curName = entityObject.CurrencyName;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curPrecision = entityObject.Precisions;
                spParams.baseCur = entityObject.BaseCurrency;
                spParams.curStatus = entityObject.Stats;
                spParams.curId = 0;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrency");
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
        public int DeleteTheData(currency entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrency spParams = new sp_manageCurrency();
                spParams.curName = entityObject.CurrencyName;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curPrecision = entityObject.Precisions;
                spParams.baseCur = entityObject.BaseCurrency;
                spParams.curStatus = entityObject.Stats;
                spParams.curId = 0;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrency");
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
        public int ProcessInsertEntity(currency entityObject)
        {
            int result = 0;
            string TableName = "currency";
            string skipAttributes = "CurrencyId,CreatedDate,ModifiedDate,";
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
        public CurrencyResponse processResponseToProxy(CurrencyResponse response, DataSet ds, string tui, string signature, string message, string action)
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
        private CurrencyResponse processResponseToProxy(CurrencyResponse response, string tui, string signature, string message, string action)
        {
            try
            {

                foreach (Currency dept in response.Currencies)
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
        private CurrencyResponse processResponseToProxy(CurrencyResponse response, DataSet ds, string tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.Currencies = new Currency[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Currency DD = new Currency();
                        DD.Name = dr[CnstCurrency.CurrencyName].ToString();
                        DD.Code = dr[CnstCurrency.CurrencyCode].ToString();
                        DD.BaseCurrency = dr[CnstCurrency.BaseCurrency].ToString();
                        DD.Precisions = dr[CnstCurrency.Precisions].ToString();
                        DD.Stats = dr[CnstCurrency.Stats].ToString();
                        DD.Id = getEncryptData(dr[CnstCurrency.CurrencyId].ToString(), DBConstants.PrimaryKey);
                        response.Currencies[idx] = DD;
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
                        response.message = "Getting Currency has " + ResponseConstants.Fail;
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