using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageMenu
    {       
        private string _menCode;
        private string _menName;
        private int _menId;
        private string _menPath;
        private string _menModuleCode;
        private string _operation;
        private int _userID;

        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string menCode
        {
            get { return _menCode; }
            set { _menCode = value; }
        }
        public string menName
        {
            get { return _menName; }
            set { _menName = value; }
        }
        public int menId
        {
            get { return _menId; }
            set { _menId = value; }
        }
        public string menPath
        {
            get { return _menPath; }
            set { _menPath = value; }
        }
        public string menModuleCode
        {
            get { return _menModuleCode; }
            set { _menModuleCode = value; }
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
