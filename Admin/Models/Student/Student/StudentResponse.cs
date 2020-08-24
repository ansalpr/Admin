using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Student
{
    public class StudentResponse:BaseResponse
    {
       public Student[] Students = new Student[] { };
    }
}