using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageDesignation
    {
        private string _desCode;
        private string _desName;
        private int _desId;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string desCode
        {
            get { return _desCode; }
            set { _desCode = value; }
        }
        public string desName
        {
            get { return _desName; }
            set { _desName = value; }
        }
        public int desId
        {
            get { return _desId; }
            set { _desId = value; }
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
