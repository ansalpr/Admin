using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class module
    {
        private int _ModuleId;
        private string _ModuleCode;
        private string _ModuleName;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
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
