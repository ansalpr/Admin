using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class ModuleControl
    {

        private string _action;
        private string _status;
        private string _message;
        private string _ModuleControlId;
        private string _ModuleCode;
        private string _From;
        private string _TO;
        private string _BackDate;
        private string _FutureDate;
        private string _Stats;

        public string Id
        {
            get { return _ModuleControlId; }
            set { _ModuleControlId = value; }
        }
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        public string TO
        {
            get { return _TO; }
            set { _TO = value; }
        }
        public string BackDate
        {
            get { return _BackDate; }
            set { _BackDate = value; }
        }
        public string FutureDate
        {
            get { return _FutureDate; }
            set { _FutureDate = value; }
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