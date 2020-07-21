using System;
using System.Net.Http;
using System.Web.Http.Filters;
using GeneralLayer;
using EntityLayer.StoredProcedures;
using System.Data;
using DataLayer;
using System.Net.Http.Headers;
using API.Base;
using Admin.Base;

namespace AdminAPI
{
    public class AuthentificationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            BaseResponse resObj = new BaseResponse();
            resObj.code = "401";
            resObj.message = "Un-Authorized";
            resObj.tui = "";
            resObj.signature = "";
            try
            {
                if (actionContext.Request.Headers.Authorization != null)
                {
                    string path = @System.Configuration.ConfigurationManager.AppSettings["params"];
                    paramFile PF = new paramFile(path);
                    string authString = actionContext.Request.Headers.Authorization.Parameter;

                    ManagedAesSample MAS = new ManagedAesSample();
                    string dec = MAS.DecryptData(authString, PF.getKey(DBConstants.Token));
                    if (dec.Split('|').Length >= 3)
                    {
                        if ((DateTime.Now - DateTime.Parse(dec.Split('|')[2].ToString())).TotalMinutes <= 20)
                        {

                            string dbCon = PF.getDatabaseConnectionString(DBConstants.MainDB);
                            DataOperation DO = new DataOperation(dbCon);
                            sp_AuthCheck authCheck = new sp_AuthCheck();
                            authCheck.uName = dec.Split('|')[0];
                            authCheck.pwd = dec.Split('|')[1];
                            authCheck.action = "select";
                            DO.BeginTRansaction();
                            DataSet ds = DO.iteratePropertyObjectsSP(authCheck, "sp_AuthCheck");
                            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                            {
                                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, resObj);
                            }
                            else
                            {
                                string enc = MAS.EncryptData(authCheck.uName + "|" + authCheck.pwd + "|" + DateTime.Now.ToString() + "|" + ds.Tables[0].Rows[0]["UserId"].ToString(), PF.getKey(DBConstants.Token));
                                AuthenticationHeaderValue headerValues = new AuthenticationHeaderValue(actionContext.Request.Headers.Authorization.Scheme, enc);
                                actionContext.Request.Headers.Authorization = headerValues;
                            }
                            DO.EndTRansaction();
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, resObj);
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, resObj);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, resObj);
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, resObj);
            }
            // actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
        }

    }
}