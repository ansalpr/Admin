using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class curriculum
    {


        private int _curriculumId;
        private string _curriculumCode;
        private string _curriculumName;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int CurriculumId
        {
            get { return _curriculumId; }
            set { _curriculumId = value; }
        }
        public string CurriculumCode
        {
            get { return _curriculumCode; }
            set { _curriculumCode = value; }
        }
        public string CurriculumName
        {
            get { return _curriculumName; }
            set { _curriculumName = value; }
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
