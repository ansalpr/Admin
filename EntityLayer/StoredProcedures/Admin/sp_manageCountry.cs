using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageCountry
    {


        private string _cntCode;
        private string _cntName;
        private int _cntId;
        private string _operation;
        private int _userID;
        private string _cntNationality;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string cntCode
        {
            get { return _cntCode; }
            set { _cntCode = value; }
        }
        public string cntName
        {
            get { return _cntName; }
            set { _cntName = value; }
        }
        public int cntId
        {
            get { return _cntId; }
            set { _cntId = value; }
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
        public string cntNationality
        {
            get { return _cntNationality; }
            set { _cntNationality = value; }
        }


    }
}
