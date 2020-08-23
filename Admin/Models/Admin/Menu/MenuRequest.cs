using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class MenuRequest :BaseRequest
    {

        public Menu[] menus = new Menu[] { };

    }
}