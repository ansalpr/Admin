using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class CurrencyRate
    {
       
        private string _action;
        private string _status;
        private string _message;
        private string _CurrencyRateId;
        private string _CurrencyCode;
        private string _ExchangeRate;
        private string _BaseCurrency;
        private string _EffectDate;

        public string Id
        {
            get { return _CurrencyRateId; }
            set { _CurrencyRateId = value; }
        }
        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        public string ExchangeRate
        {
            get { return _ExchangeRate; }
            set { _ExchangeRate = value; }
        }
        public string BaseCurrency
        {
            get { return _BaseCurrency; }
            set { _BaseCurrency = value; }
        }
        public string EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
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

    }
}