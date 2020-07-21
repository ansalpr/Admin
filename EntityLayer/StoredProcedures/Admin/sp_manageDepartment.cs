using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageDepartment
    {
        
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        private string _depName;
        private string _depCode;
        private int _depId;
        private string _operation;
        private int _userID;

        public string depName
        {
            get { return _depName; }
            set { _depName = value; }
        }
        public string depCode
        {
            get { return _depCode; }
            set { _depCode = value; }
        }
        public int depId
        {
            get { return _depId; }
            set { _depId = value; }
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
