using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageModuleControl
    {
        private string _modCode;
        private int _modId;
        private DateTime _modFromDate;
        private DateTime _modToDate;
        private int _modBackDate;
        private int _modFutureDate;
        private int _modStatus;
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
        public int modId
        {
            get { return _modId; }
            set { _modId = value; }
        }
        public DateTime modFromDate
        {
            get { return _modFromDate; }
            set { _modFromDate = value; }
        }
        public DateTime modToDate
        {
            get { return _modToDate; }
            set { _modToDate = value; }
        }
        public int modBackDate
        {
            get { return _modBackDate; }
            set { _modBackDate = value; }
        }
        public int modFutureDate
        {
            get { return _modFutureDate; }
            set { _modFutureDate = value; }
        }
        public int modStatus
        {
            get { return _modStatus; }
            set { _modStatus = value; }
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
