using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class ModuleRequest :BaseRequest
    {

        public Module[] modules = new Module[] { };

    }
}