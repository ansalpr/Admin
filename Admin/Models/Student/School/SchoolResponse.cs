using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;
using API.Base;

namespace Admin.Models.Student
{
    public class SchoolResponse : BaseResponse
    {

        public School[] School = new School[] { };
    }
}