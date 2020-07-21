using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Base
{
    public class ResponseConstants
    {
        public const int OK = 200;
        public const int NotOK = 100;
        public const int Exception = 101;
        public const int Validation = 150;

        // Error Strings
        public const string SomeErrorOccoured = "Some Error Occoured";
        public const string Success = "Success";
        public const string Fail = "Failed";
        public const string Mandatory = "Cannot Be Empty";
        public const string InvalidRequest = "Invalid Request";
        public const string Exist = "Data Already There";
    }
}