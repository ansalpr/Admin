using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageClass
    {
        private string _clsCode;
        private string _clsName;
        private int _clsId;
        private string _clsSort;
        private string _clsCurriculumCode;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string clsCode
        {
            get { return _clsCode; }
            set { _clsCode = value; }
        }
        public string clsName
        {
            get { return _clsName; }
            set { _clsName = value; }
        }
        public int clsId
        {
            get { return _clsId; }
            set { _clsId = value; }
        }
        public string clsSort
        {
            get { return _clsSort; }
            set { _clsSort = value; }
        }
        public string clsCurriculumCode
        {
            get { return _clsCurriculumCode; }
            set { _clsCurriculumCode = value; }
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
