using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class modulecontrol
    {
        private int _ModuleControlId;
        private string _ModuleCode;
        private DateTime _From;
        private DateTime _TO;
        private int _BackDate;
        private int _FutureDate;
        private int _Stats;
        private int _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int ModuleControlId
        {
            get { return _ModuleControlId; }
            set { _ModuleControlId = value; }
        }
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }
        public DateTime From
        {
            get { return _From; }
            set { _From = value; }
        }
        public DateTime TO
        {
            get { return _TO; }
            set { _TO = value; }
        }
        public int BackDate
        {
            get { return _BackDate; }
            set { _BackDate = value; }
        }
        public int FutureDate
        {
            get { return _FutureDate; }
            set { _FutureDate = value; }
        }
        public int Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
        }
        public int CreatedDate
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
