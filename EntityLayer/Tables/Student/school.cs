using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Student
{
    public class school
    {
        private int _SchoolId;
        private string _SchoolCode;
        private string _SchoolName;
        private string _SchoolAddress1;
        private string _SchoolAddress2;
        private string _Phone;
        private string _Fax;
        private string _Email;
        private DateTime _CreatedDate;
        private int _CreatedUser;
        private DateTime _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int SchoolId
        {
            get { return _SchoolId; }
            set { _SchoolId = value; }
        }
        public string SchoolCode
        {
            get { return _SchoolCode; }
            set { _SchoolCode = value; }
        }
        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        public string SchoolAddress1
        {
            get { return _SchoolAddress1; }
            set { _SchoolAddress1 = value; }
        }
        public string SchoolAddress2
        {
            get { return _SchoolAddress2; }
            set { _SchoolAddress2 = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public int CreatedUser
        {
            get { return _CreatedUser; }
            set { _CreatedUser = value; }
        }
        public DateTime ModifiedDate
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
