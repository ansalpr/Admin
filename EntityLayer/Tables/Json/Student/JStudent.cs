using EntityLayer.Tables.Student;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace EntityLayer.Tables.Json
{
    public class JStudent : BaseJson
    {
        public JStudents[] Student = new JStudents[] { };
        private string _CreatedUser;
        public string CreatedUser
        {
            get { return _CreatedUser; }
            set { _CreatedUser = value; }
        }
    }

    public class JStudents
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
        private int _SiNo;
        public JParents[] Parent = new JParents[] { };
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public int SiNo
        {
            get { return _SiNo; }
            set { _SiNo = value; }
        }
        public string StudentId
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

    }
    public class JParents
    {
        private string _ParentId;
        private string _ParentName;
        private string _Stats;
        private string _DOB;
        private string _POB;
        private string _Address1;
        private string _Address2;
        private string _CountryCode;
        private string _MotherTongue;
        private string _BloodGroupCode;
        private string _StateCode;
        private string _RelationCode;
        private int _StudentRef;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public int StudentRef
        {
            get { return _StudentRef; }
            set { _StudentRef = value; }
        }
        public string ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        public string Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
        }
        public string DOB
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
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        public string RelationCode
        {
            get { return _RelationCode; }
            set { _RelationCode = value; }
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
    }
}

