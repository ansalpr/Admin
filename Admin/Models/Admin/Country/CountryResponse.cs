using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Base;

namespace Admin.Models.Admin
{
    public class CountryResponse : BaseResponse
    {
        public Country[] Countries = new Country[] { };
    }
}