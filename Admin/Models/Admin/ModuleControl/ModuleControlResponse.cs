using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class ModuleControlResponse :BaseResponse
    {
        public ModuleControl[] ModuleControls = new ModuleControl[] { };
    }   
}