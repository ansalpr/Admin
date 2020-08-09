using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures.Admin
{
    public class sp_manageCurrencyRate
    {


        private string _curCode;
        private string _curBaseCurrency;
        private int _curRateId;
        private string _curExchangeRate;
        private string _curEffectDate;
        private string _operation;
        private int _userID;
        private string _action;
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string curCode
        {
            get { return _curCode; }
            set { _curCode = value; }
        }
        public string curBaseCurrency
        {
            get { return _curBaseCurrency; }
            set { _curBaseCurrency = value; }
        }
        public int curRateId
        {
            get { return _curRateId; }
            set { _curRateId = value; }
        }
        public string curExchangeRate
        {
            get { return _curExchangeRate; }
            set { _curExchangeRate = value; }
        }
        public string curEffectDate
        {
            get { return _curEffectDate; }
            set { _curEffectDate = value; }
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
