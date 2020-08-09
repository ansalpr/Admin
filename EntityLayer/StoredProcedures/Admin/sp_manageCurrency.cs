using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageCurrency
    {
        private string _curCode;
        private string _curName;
        private int _curId;
        private string _operation;
        private int _userID;
        private string _baseCur;
        private string _curPrecision;
        private string _curStatus;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string curCode
        {
            get { return _curCode; }
            set { _curCode = value; }
        }
        public string curName
        {
            get { return _curName; }
            set { _curName = value; }
        }
        public int curId
        {
            get { return _curId; }
            set { _curId = value; }
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
        public string baseCur
        {
            get { return _baseCur; }
            set { _baseCur = value; }
        }
        public string curPrecision
        {
            get { return _curPrecision; }
            set { _curPrecision = value; }
        }
        public string curStatus
        {
            get { return _curStatus; }
            set { _curStatus = value; }
        }

    }
}
