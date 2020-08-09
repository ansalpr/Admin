using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;

namespace Admin.Models.Admin
{ 
    public class CurrencyRequest : BaseRequest
    {

        public Currency[] Currencies = new Currency[] { };

    }
}