using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class classe
    {

        private int _ClassId;
        private string _ClassCode;
        private string _ClassName;
        private string _Sort;
        private string _CurriculumCode;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int ClassId
        {
            get { return _ClassId; }
            set { _ClassId = value; }
        }
        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public string CurriculumCode
        {
            get { return _CurriculumCode; }
            set { _CurriculumCode = value; }
        }
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public int CreatedUser
        {
            get { return _CreatedUser; }
            set { _CreatedUser = value; }
        }
        public int ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        public int ModifiedUser
        {
            get { return _ModifiedUser; }
            set { _ModifiedUser = value; }
        }
        public int RecordStatus
        {
            get { return _RecordStatus; }
            set { _RecordStatus = value; }
        }


    }
}
