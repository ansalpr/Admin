using Admin.Base;
using AdminAPI.Models;
using API.Base;
using DataLayer;
using EntityLayer.Tables;
using GeneralLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Admin.Helper.Utility.Authentication
{
    public class AuthHelper : BaseHelper
    {
        public authResponse VaidateRequest(authRequest reqObject)
        {
            authResponse response = new authResponse();
            string message = "";
            if (reqObject == null)
            {
                message = ResponseConstants.InvalidRequest;
            }
            else if (reqObject.name == null || reqObject.name == "")
            {
                message = "Name " + ResponseConstants.Mandatory;
            }
            else if (reqObject.password == null || reqObject.password == "")
            {
                message = "Password " + ResponseConstants.Mandatory;
            }
            response.message = message;
            response.tui = reqObject.tui;
            if (message == "")
            {
                response.code = ResponseConstants.OK.ToString();
            }
            else
            {
                response.code = ResponseConstants.NotOK.ToString();
            }
            return response;
        }
        public authResponse processResponseToProxy(DataSet ds, string tui, string signature)
        {

            authResponse response = new authResponse();
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.code = ResponseConstants.OK.ToString();
                    response.message = ResponseConstants.Success;
                    if (tui == null || tui == "")
                    {
                        tui = Guid.NewGuid().ToString();
                    }
                    response.tui = tui;
                    response.signature = signature;
                }
                else
                {
                    response.code = ResponseConstants.NotOK.ToString();
                    response.message = "Login " + ResponseConstants.Fail;
                    response.tui = tui;
                }

            }
            catch (Exception)
            {
                response.code = ResponseConstants.NotOK.ToString();
                response.message = ResponseConstants.SomeErrorOccoured;
                response.tui = tui;
            }
            return response;
        }     

    }
}