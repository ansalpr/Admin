using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Models.Admin;
namespace Admin.Models.Student
{
    public class Student
    {
        private string _StudentId;
        private string _AdmissionNo;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Gender;
        private string _MotherID;
        private string _FatherID;
        private string _GuardianID;
        private string _POB;
        private string _DOB;
        private string _Address1;
        private string _Address2;
        private string _StateCode;
        private string _CountryCode;
        private string _MotherTongue;
        private string _BloodGroupCode;
        private string _Stats;
        private string _action;
        private string _status;
        private string _message;
        private string _SiNo;
        public Parent[] parents = new Parent []{ };

        public string SiNo
        {
            get { return _SiNo; }
            set { _SiNo = value; }
        }
        public string Id
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
        public string MotherID
        {
            get { return _MotherID; }
            set { _MotherID = value; }
        }
        public string FatherID
        {
            get { return _FatherID; }
            set { _FatherID = value; }
        }
        public string GuardianID
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
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

    }
}