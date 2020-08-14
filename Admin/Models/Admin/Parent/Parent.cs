using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Parent
    {
        private string _ParentId;
        private string _ParentName;
        private string _DOB;
        private string _POB;
        private string _Address1;
        private string _Address2;
        private string _Country;
        private string _MotherTongue;
        private string _BloodGroup;
        private string _action;
        private string _status;
        private string _message;
        public string Id
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        public string Name
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string POB
        {
            get { return _POB; }
            set { _POB = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string MotherTongue
        {
            get { return _MotherTongue; }
            set { _MotherTongue = value; }
        }
        public string BloodGroup
        {
            get { return _BloodGroup; }
            set { _BloodGroup = value; }
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