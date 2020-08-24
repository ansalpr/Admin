using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Country
    {

        private string _CountryId;
        private string _CountryCode;
        private string _CountryName;
        private string _Nationality;
        private string _action;
        private string _status;
        private string _message;
        public string Id
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        public string Code
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string Name
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }       
       
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}