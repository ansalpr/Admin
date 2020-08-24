using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Division
    {

        private string _DivisionId;
        private string _DivisionCode;
        private string _DivisionName;
        private string _ClassCode;
        private string _Stats;       
        private string _action;
        private string _status;
        private string _message;

        public string Id
        {
            get { return _DivisionId; }
            set { _DivisionId = value; }
        }
        public string Code
        {
            get { return _DivisionCode; }
            set { _DivisionCode = value; }
        }
        public string Name
        {
            get { return _DivisionName; }
            set { _DivisionName = value; }
        }
        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }
        public string Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
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