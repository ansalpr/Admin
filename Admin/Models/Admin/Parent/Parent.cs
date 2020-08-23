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
        private string _CountryCode;
        private string _StateCode;
        private string _RelationCode;
        private string _MotherTongue;
        private string _BloodGroupCode;
        private string _action;
        private string _status;
        private string _message;
        private string _StudentRef;
        public string StudentRef
        {
            get { return _StudentRef; }
            set { _StudentRef = value; }
        }
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
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string RelationCode
        {
            get { return _RelationCode; }
            set { _RelationCode = value; }
        }
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string MotherTongue
        {
            get { return _MotherTongue; }
            set { _MotherTongue = value; }
        }
        public string BloodGroupCode
        {
            get { return _BloodGroupCode; }
            set { _BloodGroupCode = value; }
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