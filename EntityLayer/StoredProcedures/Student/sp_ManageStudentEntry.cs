using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Student
{
    public class sp_ManageStudentEntry
    {
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        private string _students;       
        private string _operation;
        private int _userID;

        public string students
        {
            get { return _students; }
            set { _students = value; }
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
