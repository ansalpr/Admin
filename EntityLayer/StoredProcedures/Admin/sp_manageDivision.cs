using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageDivision
    {
        private string _divCode;
        private string _divName;
        private int _divId;
        private string _divClassCode;
        private string _divStatus;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string divCode
        {
            get { return _divCode; }
            set { _divCode = value; }
        }
        public string divName
        {
            get { return _divName; }
            set { _divName = value; }
        }
        public int divId
        {
            get { return _divId; }
            set { _divId = value; }
        }
        public string divClassCode
        {
            get { return _divClassCode; }
            set { _divClassCode = value; }
        }
        public string divStatus
        {
            get { return _divStatus; }
            set { _divStatus = value; }
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
