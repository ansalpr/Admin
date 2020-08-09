using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;

namespace Admin.Models.Admin
{ 
    public class CountryRequest : BaseRequest
    {

        public Country[] Countries = new Country[] { };

    }
}