using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class MenuResponse :BaseResponse
    {
        public Menu[] menus = new Menu[] { };
    }   
}