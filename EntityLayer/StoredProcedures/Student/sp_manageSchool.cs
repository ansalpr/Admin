using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Student
{
    public class sp_manageSchool
    {       
        private string _schCode;
        private string _schName;
        private int _schId;
        private string _schAddress1;
        private string _schAddress2;
        private string _schPhone;
        private string _schFax;
        private string _schEmail;
        private string _operation;
        private int _userID;

        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string schCode
        {
            get { return _schCode; }
            set { _schCode = value; }
        }
        public string schName
        {
            get { return _schName; }
            set { _schName = value; }
        }
        public int schId
        {
            get { return _schId; }
            set { _schId = value; }
        }
        public string schAddress1
        {
            get { return _schAddress1; }
            set { _schAddress1 = value; }
        }
        public string schAddress2
        {
            get { return _schAddress2; }
            set { _schAddress2 = value; }
        }
        public string schPhone
        {
            get { return _schPhone; }
            set { _schPhone = value; }
        }
        public string schFax
        {
            get { return _schFax; }
            set { _schFax = value; }
        }
        public string schEmail
        {
            get { return _schEmail; }
            set { _schEmail = value; }
        }
        public string operation
        {
            get { return _operation; }
            set { _operation = value; }
        }
        public int userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
    }
}
