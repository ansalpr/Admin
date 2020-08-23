using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class menu
    {
        private int _MenuId;
        private string _MenuCode;
        private string _MenuName;
        private string _Path;
        private string _ModuleCode;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        public string MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
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
