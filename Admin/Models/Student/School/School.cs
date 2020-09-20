using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Student
{
    public class School
    {
        private string _SchoolId;
        private string _SchoolCode;
        private string _SchoolName;
        private string _SchoolAddress1;
        private string _SchoolAddress2;
        private string _Phone;
        private string _Fax;
        private string _Email;
        private string _action;
        private string _status;
        private string _message;


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
        public string Id
        {
            get { return _SchoolId; }
            set { _SchoolId = value; }
        }
        public string Code
        {
            get { return _SchoolCode; }
            set { _SchoolCode = value; }
        }
        public string Name
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        public string Address1
        {
            get { return _SchoolAddress1; }
            set { _SchoolAddress1 = value; }
        }
        public string Address2
        {
            get { return _SchoolAddress2; }
            set { _SchoolAddress2 = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
       
    }
}