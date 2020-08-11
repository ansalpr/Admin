using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class designation
    {
        private int _DesignationId;
        private string _DesignationCode;
        private string _DesignationName;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int DesignationId
        {
            get { return _DesignationId; }
            set { _DesignationId = value; }
        }
        public string DesignationCode
        {
            get { return _DesignationCode; }
            set { _DesignationCode = value; }
        }
        public string DesignationName
        {
            get { return _DesignationName; }
            set { _DesignationName = value; }
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
