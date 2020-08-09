using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Base;

namespace Admin.Models.Admin
{
    public class CurrencyRateResponse : BaseResponse
    {
        public CurrencyRate[] CurrencyRate = new CurrencyRate[] { };
    }
}