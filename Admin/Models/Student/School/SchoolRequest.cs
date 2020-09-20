using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;

namespace Admin.Models.Student
{
    public class SchoolRequest : BaseRequest
    {

        public School[] School = new School[] { };
    }
}