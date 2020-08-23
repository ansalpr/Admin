using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Student
{
    public class StudentRequest :  BaseRequest
    {
        public Student[] students = new Student[] { };
    }
}