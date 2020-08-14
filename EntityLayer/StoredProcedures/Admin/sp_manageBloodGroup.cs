using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageBloodGroup
    {
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        private string _bldName;
        private string _bldCode;
        private int _bldId;
        private string _operation;
        private int _userID;

        public string bldName
        {
            get { return _bldName; }
            set { _bldName = value; }
        }
        public string bldCode
        {
            get { return _bldCode; }
            set { _bldCode = value; }
        }
        public int bldId
        {
            get { return _bldId; }
            set { _bldId = value; }
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
