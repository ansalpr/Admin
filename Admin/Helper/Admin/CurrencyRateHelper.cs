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
using System.Globalization;
using System.Linq;

namespace Admin.Helper.Admin
{
    public class CurrencyRateHelper : BaseHelper
    {
        public CurrencyRateResponse ValidateRequest(CurrencyRateRequest reqObjects)
        {
            CurrencyRateResponse response = new CurrencyRateResponse();
            response.CurrencyRate = new CurrencyRate[reqObjects.CurrencyRate.Length];
            string message = "";
            for (int idx = 0; idx < reqObjects.CurrencyRate.Length; idx++)
            {
                if (reqObjects.CurrencyRate == null)
                {
                    message = ResponseConstants.InvalidRequest;
                }
                else if ((reqObjects.CurrencyRate[idx].Action.ToUpper() == "A" || reqObjects.CurrencyRate[idx].Action.ToUpper() == "E"))
                {
                    if ((reqObjects.CurrencyRate[idx].CurrencyCode == null || reqObjects.CurrencyRate[idx].CurrencyCode == ""))
                    {
                        message = "CurrencyCode " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.CurrencyRate[idx].ExchangeRate == null || reqObjects.CurrencyRate[idx].ExchangeRate == "" ))
                    {
                        message = "ExchangeRate " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.CurrencyRate[idx].BaseCurrency == null || reqObjects.CurrencyRate[idx].BaseCurrency == ""))
                    {
                        message = "BaseCurrency " + ResponseConstants.Mandatory;
                    }
                    else if ((reqObjects.CurrencyRate[idx].EffectDate == null || reqObjects.CurrencyRate[idx].EffectDate == ""))
                    {
                        message = "EffectDate " + ResponseConstants.Mandatory;
                    }
                    else
                    {
                        if (!validateDateFormat(reqObjects.CurrencyRate[idx].EffectDate))
                        {
                            message = ResponseConstants.InValid + " " + CnstCurrencyRate.EffectDate;
                        }
                        else if(reqObjects.CurrencyRate[idx].ExchangeRate == "0")
                        {
                            message = CnstCurrencyRate.ExchangeRate + " Must be greater than Zero";
                        }
                    }
                }
                else if ((reqObjects.CurrencyRate[idx].Id == null || reqObjects.CurrencyRate[idx].Id == "") && (reqObjects.CurrencyRate[idx].Action.ToUpper() == "E" || reqObjects.CurrencyRate[idx].Action.ToUpper() == "D"))
                {
                    message = "Id " + ResponseConstants.Mandatory;
                }
                CurrencyRate proxyResponse = new CurrencyRate();
                proxyResponse = reqObjects.CurrencyRate[idx];
                proxyResponse.Message = message;
                response.CurrencyRate[idx] = proxyResponse;
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
        public currencyrate[] ProcessProxyToEntity(CurrencyRateRequest reqObjects, int UserId)
        {
            currencyrate[] entityObects = new currencyrate[reqObjects.CurrencyRate.Length];
            try
            {
                for (int idx = 0; idx < reqObjects.CurrencyRate.Length; idx++)
                {
                    currencyrate entityObect = new currencyrate();
                    entityObect.CurrencyCode = reqObjects.CurrencyRate[idx].CurrencyCode == null ? "" : reqObjects.CurrencyRate[idx].CurrencyCode.Trim();
                    entityObect.BaseCurrency = reqObjects.CurrencyRate[idx].BaseCurrency == null ? "" : reqObjects.CurrencyRate[idx].BaseCurrency.Trim();
                    entityObect.EffectDate =  Convert.ToDateTime(reqObjects.CurrencyRate[idx].EffectDate.Trim());
                    entityObect.ExchangeRate = Convert.ToDecimal(reqObjects.CurrencyRate[idx].ExchangeRate.Trim());
                    entityObect.CurrencyRateId = reqObjects.CurrencyRate[idx].Id == null ? 0 : reqObjects.CurrencyRate[idx].Id == "" ? 0 : Convert.ToInt32(getDecryptData(reqObjects.CurrencyRate[idx].Id, DBConstants.PrimaryKey));
                    entityObect.CreatedUser = UserId;
                    entityObect.ModifiedUser = 0;
                    entityObect.RecordStatus = 0;
                    if (reqObjects.CurrencyRate[idx].Action == "D")
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
        public bool CheckTheDataExistance(currencyrate entityObject)
        {
            bool result = false;
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrencyRate spParams = new sp_manageCurrencyRate();
                spParams.curBaseCurrency = entityObject.BaseCurrency;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curEffectDate = entityObject.EffectDate.ToString();
                spParams.curExchangeRate = entityObject.ExchangeRate.ToString();
                spParams.curRateId = 0;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrencyRate");
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
        public DataSet GetTheData(currencyrate entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrencyRate spParams = new sp_manageCurrencyRate();
                spParams.curBaseCurrency = entityObject.BaseCurrency;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curEffectDate = entityObject.EffectDate.ToString();
                spParams.curExchangeRate = entityObject.ExchangeRate.ToString();
                spParams.curRateId = entityObject.CurrencyRateId;
                spParams.action = "select";
                spParams.operation = "S";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrencyRate");
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
        public int UpdateTheData(currencyrate entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrencyRate spParams = new sp_manageCurrencyRate();
                spParams.curBaseCurrency = entityObject.BaseCurrency;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curEffectDate = entityObject.EffectDate.ToString();
                spParams.curExchangeRate = entityObject.ExchangeRate.ToString();
                spParams.curRateId = entityObject.CurrencyRateId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Edit";
                spParams.operation = "E";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrencyRate");
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
        public int DeleteTheData(currencyrate entityObject)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_manageCurrencyRate spParams = new sp_manageCurrencyRate();
                spParams.curBaseCurrency = entityObject.BaseCurrency;
                spParams.curCode = entityObject.CurrencyCode;
                spParams.curEffectDate = entityObject.EffectDate.ToString();
                spParams.curExchangeRate = entityObject.ExchangeRate.ToString();
                spParams.curRateId = entityObject.CurrencyRateId;
                spParams.userID = entityObject.CreatedUser;
                spParams.action = "Delete";
                spParams.operation = "D";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(spParams, "sp_manageCurrencyRate");
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
        public int ProcessInsertEntity(currencyrate entityObject)
        {
            int result = 0;
            string TableName = "currencyrate";
            string skipAttributes = "CurrencyRateId,CreatedDate,ModifiedDate,";
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
        public CurrencyRateResponse processResponseToProxy(CurrencyRateResponse response, DataSet ds, string Tui, string signature, string message, string action)
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
        private CurrencyRateResponse processResponseToProxy(CurrencyRateResponse response, string Tui, string signature, string message, string action)
        {
            try
            {

                foreach (CurrencyRate dept in response.CurrencyRate)
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
        private CurrencyRateResponse processResponseToProxy(CurrencyRateResponse response, DataSet ds, string Tui, string signature, string message)
        {
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    int idx = 0;
                    response.CurrencyRate = new CurrencyRate[ds.Tables[0].Rows.Count];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CurrencyRate DD = new CurrencyRate();
                        DD.BaseCurrency = dr[CnstCurrencyRate.BaseCurrency].ToString();
                        DD.CurrencyCode = dr[CnstCurrencyRate.CurrencyCode].ToString();
                        DD.EffectDate = dr[CnstCurrencyRate.EffectDate].ToString();
                        DD.ExchangeRate = dr[CnstCurrencyRate.ExchangeRate].ToString();
                        DD.Id = getEncryptData(dr[CnstCurrencyRate.CurrencyRateId].ToString(), DBConstants.PrimaryKey);
                        response.CurrencyRate[idx] = DD;
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
                        response.Message = "Getting CurrencyRate has " + ResponseConstants.Fail;
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