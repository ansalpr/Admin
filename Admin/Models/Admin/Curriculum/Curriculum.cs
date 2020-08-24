using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Curriculum
    {
        private string _curriculumId;
        private string _curriculumCode;
        private string _curriculumName;
        private string _action;
        private string _status;
        private string _message;

        public string Id
        {
            get { return _curriculumId; }
            set { _curriculumId = value; }
        }
        public string Code
        {
            get { return _curriculumCode; }
            set { _curriculumCode = value; }
        }
        public string Name
        {
            get { return _curriculumName; }
            set { _curriculumName = value; }
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