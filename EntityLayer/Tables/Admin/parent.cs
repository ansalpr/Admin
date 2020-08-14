using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class parent
    {
        private int _ParentId;
        private string _ParentName;
        private DateTime _DOB;
        private string _POB;
        private string _Address1;
        private string _Address2;
        private string _Country;
        private string _MotherTongue;
        private string _BloodGroup;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string POB
        {
            get { return _POB; }
            set { _POB = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string MotherTongue
        {
            get { return _MotherTongue; }
            set { _MotherTongue = value; }
        }
        public string BloodGroup
        {
            get { return _BloodGroup; }
            set { _BloodGroup = value; }
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
