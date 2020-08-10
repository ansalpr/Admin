using Admin.Base;
using AdminAPI.Models;
using DataLayer;
using EntityLayer.StoredProcedures;
using GeneralLayer;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Admin.Helper.General
{
    public class GeneralHelper: BaseHelper
    {
        public DataSet getAuthData(authRequest authObj)
        {
            DataSet ds = new DataSet();
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                DataOperation DO = new DataOperation(dbCon);
                sp_AuthCheck authCheck = new sp_AuthCheck();
                authCheck.uName = authObj.name;
                authCheck.pwd = authObj.password;
                authCheck.action = "select";
                DO.BeginTRansaction();
                ds = DO.iteratePropertyObjectsSP(authCheck, "sp_AuthCheck");
                DO.EndTRansaction();
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return ds;
        }
        public string getSignature(string userCredentials)
        {
            string encCredentials = "";
            try
            {
                paramFile PF = new paramFile(ParamsPath);
                string key = PF.getKey(DBConstants.Token);
                ManagedAesSample aes = new ManagedAesSample();
                encCredentials = aes.EncryptData(userCredentials, key);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return encCredentials;
        }

        public int GetUserId(string encData)
        {
            int userId = 0;
            string decData = "";
            try
            {
                decData = DecryptAuthData(encData);
                if (decData.Split('|').Length > 3)
                {
                    userId = Convert.ToInt32(decData.Split('|')[3].ToString());
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                currentMethodName = ex.Message.ToString().Split('|').Count() > 0 ? ex.Message.ToString().Split('|')[0] : "NewGroup";
                string currentControllerName = ex.Message.ToString().Split('|').Count() > 1 ? ex.Message.ToString().Split('|')[1] : this.GetType().Name;                
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return userId;
        }
        private string DecryptAuthData(string encData)
        {
            string decData = "";
            paramFile PF = new paramFile(ParamsPath);
            ManagedAesSample MAS = new ManagedAesSample();
            try
            {

                decData = MAS.DecryptData(encData, PF.getKey(DBConstants.Token));
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }
            return decData;
        }
        public string getEntityStructure(string Name, string ConnectionName, string type)
        {
            string resultStructure = "";
            paramFile PF = new paramFile(ParamsPath);
            try
            {
                string dbCon = PF.getDatabaseConnectionString(ConnectionName);
                DataOperation DO = new DataOperation(dbCon);
                DO.BeginTRansaction();
                if (type == "SP")
                {
                    resultStructure = resultStructure = DO.getSPStructure(Name);
                }
                else if (type == "TBL")
                {
                    resultStructure = resultStructure = DO.getEntityStructure(Name);
                }
                DO.EndTRansaction();
            }
            catch (Exception ex)
            {
                Exception exec = new Exception("getEntityStructure | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw exec;
            }
            return resultStructure;
        }
    }
}