using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
namespace GeneralLayer
{
    public class paramFile
    {
        public string getDatabaseConnectionString(string dbType)
        {
            string dbString = "";
            try
            {
                JObject jsonObject = JObject.Parse(File.ReadAllText(@"D:\Project\Admin\GeneralLayer\Files\params.json"));
                foreach (JToken dbSource in jsonObject.SelectToken("DataBase"))
                {
                    string type = (string)dbSource["type"];
                    if (type == dbType)
                    {
                        dbString = "Persist Security Info=False;database=" + (string)dbSource["name"] + "; server=" + (string)dbSource["server"] +"; Connect Timeout=30;user id=" + (string)dbSource["id"] + "; pwd=" + (string)dbSource["pwd"];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbString;
        }
        public string getKey(string keyType)
        {
            string key = "";
            try
            {
                JObject jsonObject = JObject.Parse(File.ReadAllText(@"D:\Project\WebAPIDemo\GeneralLayer\Files\params.json"));
                foreach (JToken dbSource in jsonObject.SelectToken("key"))
                {
                    key = (string)dbSource[keyType];                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return key;
        }
    }
}
