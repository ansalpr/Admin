using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Base;

namespace Admin.Models.Admin.Curriculum
{
    public class CurriculumResponse : BaseResponse
    {
        public Curriculum[] curriculums = new Curriculum[] { };
    }
}