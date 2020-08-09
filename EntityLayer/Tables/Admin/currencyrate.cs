using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Admin
{
   public class currencyrate
    {
        private int _CurrencyRateId;
        private string _CurrencyCode;
        private decimal _ExchangeRate;
        private string _BaseCurrency;
        private DateTime _EffectDate;
        private string _CreatedDate;
        private int _CreatedUser;
        private int _ModifiedDate;
        private int _ModifiedUser;
        private int _RecordStatus;

        public int CurrencyRateId
        {
            get { return _CurrencyRateId; }
            set { _CurrencyRateId = value; }
        }
        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        public decimal ExchangeRate
        {
            get { return _ExchangeRate; }
            set { _ExchangeRate = value; }
        }
        public string BaseCurrency
        {
            get { return _BaseCurrency; }
            set { _BaseCurrency = value; }
        }
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
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
