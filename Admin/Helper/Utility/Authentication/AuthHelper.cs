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
            else if (reqObject.Name == null || reqObject.Name == "")
            {
                message = "Name " + ResponseConstants.Mandatory;
            }
            else if (reqObject.Password == null || reqObject.Password == "")
            {
                message = "Password " + ResponseConstants.Mandatory;
            }
            response.Message = message;
            response.Tui = reqObject.Tui;
            if (message == "")
            {
                response.Code = ResponseConstants.OK.ToString();
            }
            else
            {
                response.Code = ResponseConstants.NotOK.ToString();
            }
            return response;
        }
        public authResponse processResponseToProxy(DataSet ds, string Tui, string signature)
        {

            authResponse response = new authResponse();
            try
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.Code = ResponseConstants.OK.ToString();
                    response.Message = ResponseConstants.Success;
                    if (Tui == null || Tui == "")
                    {
                        Tui = Guid.NewGuid().ToString();
                    }
                    response.Tui = Tui;
                    response.Signature = signature;
                }
                else
                {
                    response.Code = ResponseConstants.NotOK.ToString();
                    response.Message = "Login " + ResponseConstants.Fail;
                    response.Tui = Tui;
                }

            }
            catch (Exception)
            {
                response.Code = ResponseConstants.NotOK.ToString();
                response.Message = ResponseConstants.SomeErrorOccoured;
                response.Tui = Tui;
            }
            return response;
        }     

    }
}