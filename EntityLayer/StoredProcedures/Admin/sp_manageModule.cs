using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageModule
    {

        private string _modCode;
        private string _modName;
        private int _modId;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string modCode
        {
            get { return _modCode; }
            set { _modCode = value; }
        }
        public string modName
        {
            get { return _modName; }
            set { _modName = value; }
        }
        public int modId
        {
            get { return _modId; }
            set { _modId = value; }
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
