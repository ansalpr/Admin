using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Currency
    {
        private string _CurrencyId;
        private string _CurrencyCode;
        private string _CurrencyName;
        private string _BaseCurrency;
        private string _Precisions;
        private string _Stats;
        private string _action;
        private string _status;
        private string _message;

        public string Id
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }
        public string Code
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        public string Name
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }
        public string BaseCurrency
        {
            get { return _BaseCurrency; }
            set { _BaseCurrency = value; }
        }
        public string Precisions
        {
            get { return _Precisions; }
            set { _Precisions = value; }
        }
        public string Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
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