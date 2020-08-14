using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class BloodGroupRequest :BaseRequest
    {

        public BloodGroup[] bloodgroups = new BloodGroup[] { };

    }
}