using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
    public class division
    {
        private int _DivisionId;
        private string _DivisionCode;
        private string _DivisionName;
        private string _ClassCode;
        private string _Stats;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int DivisionId
        {
            get { return _DivisionId; }
            set { _DivisionId = value; }
        }
        public string DivisionCode
        {
            get { return _DivisionCode; }
            set { _DivisionCode = value; }
        }
        public string DivisionName
        {
            get { return _DivisionName; }
            set { _DivisionName = value; }
        }
        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
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
