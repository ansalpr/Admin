using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class bloodgroup
    {
        private int _BloodGroupId;
        private string _BloodGroupCode;
        private string _BloodGroupName;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int BloodGroupId
        {
            get { return _BloodGroupId; }
            set { _BloodGroupId = value; }
        }
        public string BloodGroupCode
        {
            get { return _BloodGroupCode; }
            set { _BloodGroupCode = value; }
        }
        public string BloodGroupName
        {
            get { return _BloodGroupName; }
            set { _BloodGroupName = value; }
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
