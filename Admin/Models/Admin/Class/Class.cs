using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Classe
    {     
        
        private string _ClassId;
        private string _ClassCode;
        private string _ClassName;
        private string _Sort;
        private string _CurriculumCode;       
        private string _action;
        private string _status;
        private string _message;


        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Id
        {
            get { return _ClassId; }
            set { _ClassId = value; }
        }
        public string Code
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }
        public string Name
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public string CurriculumCode
        {
            get { return _CurriculumCode; }
            set { _CurriculumCode = value; }
        }
       
    }
}