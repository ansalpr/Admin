using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Relation
    {
        private string _RelationName;
        private string _RelationCode;
        private string _RelationId;
        private string _action;
        private string _status;
        private string _message;
        public string Name
        {
            get { return _RelationName; }
            set { _RelationName = value; }
        }
        public string Code
        {
            get { return _RelationCode; }
            set { _RelationCode = value; }
        }
        public string Id
        {
            get { return _RelationId; }
            set { _RelationId = value; }
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