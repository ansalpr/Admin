using GeneralLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Admin.Base
{
    public class BaseHelper
    {
        public string ParamsPath = "";
        public BaseHelper()
        {
            ParamsPath = @System.Configuration.ConfigurationManager.AppSettings["params"];
        }
        public string getDecryptData(string encString,string decType)
        {
            ManagedAesSample MAS = new ManagedAesSample();
            paramFile PF = new paramFile(ParamsPath);
            string decData = "";
            try
            {               
                decData = MAS.DecryptData(encString, PF.getKey(decType));
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
        public string getEncryptData(string decString, string decType)
        {
            ManagedAesSample MAS = new ManagedAesSample();
            paramFile PF = new paramFile(ParamsPath);
            string encData = "";
            try
            {
                encData = MAS.EncryptData(decString, PF.getKey(decType));
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                string currentMethodName = sf.GetMethod().Name;
                Exception customex = new Exception(currentMethodName + " | " + this.GetType().Name + " | " + ex.Message + " : " + ex.StackTrace);
                throw customex;
            }

            return encData;
        }

    }
}