using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageCurriculum
    {
        private string _CurCode;
        private string _CurName;
        private int _CurId;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string CurCode
        {
            get { return _CurCode; }
            set { _CurCode = value; }
        }
        public string CurName
        {
            get { return _CurName; }
            set { _CurName = value; }
        }
        public int CurId
        {
            get { return _CurId; }
            set { _CurId = value; }
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
