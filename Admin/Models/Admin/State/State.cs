using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class State
    {
               
        private string _StateId;
        private string _StateCode;
        private string _StateName;
        private string _CountryCode;
        private string _action;
        private string _status;
        private string _message;

        public string Id
        {
            get { return _StateId; }
            set { _StateId = value; }
        }
        public string Code
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string Name
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }        
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}