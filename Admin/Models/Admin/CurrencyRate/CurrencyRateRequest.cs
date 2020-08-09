using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;

namespace Admin.Models.Admin
{ 
    public class CurrencyRateRequest : BaseRequest
    {

        public CurrencyRate[] CurrencyRate = new CurrencyRate[] { };

    }
}