using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageSection
    {
        private string _secCode;
        private string _secName;
        private int _secId;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string secCode
        {
            get { return _secCode; }
            set { _secCode = value; }
        }
        public string secName
        {
            get { return _secName; }
            set { _secName = value; }
        }
        public int secId
        {
            get { return _secId; }
            set { _secId = value; }
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
