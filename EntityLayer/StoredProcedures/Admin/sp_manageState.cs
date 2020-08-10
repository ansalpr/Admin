using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageState
    {
        private string _statCode;
        private string _statName;
        private int _statId;
        private string _cntCode;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string statCode
        {
            get { return _statCode; }
            set { _statCode = value; }
        }
        public string statName
        {
            get { return _statName; }
            set { _statName = value; }
        }
        public int statId
        {
            get { return _statId; }
            set { _statId = value; }
        }
        public string cntCode
        {
            get { return _cntCode; }
            set { _cntCode = value; }
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
