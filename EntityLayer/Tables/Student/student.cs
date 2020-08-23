using EntityLayer.Tables.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Student
{
    public class student
    {
        private int _StudentId;
        private string _AdmissionNo;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Gender;
        private int _MotherID;
        private int _FatherID;
        private int _GuardianID;
        private string _POB;
        private string _DOB;
        private string _Address1;
        private string _Address2;
        private string _StateCode;
        private string _CountryCode;
        private string _MotherTongue;
        private string _BloodGroupCode;
        private string _Stats;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;
        public parent[] Parent = new parent[] { };
        public int StudentId
        {
            get { return _StudentId; }
            set { _StudentId = value; }
        }
        public string AdmissionNo
        {
            get { return _AdmissionNo; }
            set { _AdmissionNo = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public int MotherID
        {
            get { return _MotherID; }
            set { _MotherID = value; }
        }
        public int FatherID
        {
            get { return _FatherID; }
            set { _FatherID = value; }
        }
        public int GuardianID
        {
            get { return _GuardianID; }
            set { _GuardianID = value; }
        }
        public string POB
        {
            get { return _POB; }
            set { _POB = value; }
        }
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
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
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string MotherTongue
        {
            get { return _MotherTongue; }
            set { _MotherTongue = value; }
        }
        public string BloodGroupCode
        {
            get { return _BloodGroupCode; }
            set { _BloodGroupCode = value; }
        }
        public string Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
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
