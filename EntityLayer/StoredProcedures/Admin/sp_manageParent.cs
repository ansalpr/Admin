using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageParent
    {
        private string _parName;
        private int _parId;
        private DateTime _parDOB;
        private string _parPOB;
        private string _parAddress1;
        private string _parAddress2;
        private string _parCountry;
        private string _parMotherTongue;
        private string _parBloodGroup;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string parName
        {
            get { return _parName; }
            set { _parName = value; }
        }
        public int parId
        {
            get { return _parId; }
            set { _parId = value; }
        }
        public DateTime parDOB
        {
            get { return _parDOB; }
            set { _parDOB = value; }
        }
        public string parPOB
        {
            get { return _parPOB; }
            set { _parPOB = value; }
        }
        public string parAddress1
        {
            get { return _parAddress1; }
            set { _parAddress1 = value; }
        }
        public string parAddress2
        {
            get { return _parAddress2; }
            set { _parAddress2 = value; }
        }
        public string parCountry
        {
            get { return _parCountry; }
            set { _parCountry = value; }
        }
        public string parMotherTongue
        {
            get { return _parMotherTongue; }
            set { _parMotherTongue = value; }
        }
        public string parBloodGroup
        {
            get { return _parBloodGroup; }
            set { _parBloodGroup = value; }
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
