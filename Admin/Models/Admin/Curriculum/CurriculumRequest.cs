using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Admin.Base;
namespace Admin.Models.Admin.Curriculum
{
    public class CurriculumRequest : BaseRequest
    {

        public Curriculum[] curriculums = new Curriculum[] { };

    }
}