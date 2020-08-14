using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageRelation
    {
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        private string _relName;
        private string _relCode;
        private int _relId;
        private string _operation;
        private int _userID;

        public string relName
        {
            get { return _relName; }
            set { _relName = value; }
        }
        public string relCode
        {
            get { return _relCode; }
            set { _relCode = value; }
        }
        public int relId
        {
            get { return _relId; }
            set { _relId = value; }
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
