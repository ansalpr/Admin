using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Base;

namespace Admin.Models.Admin
{
    public class ClassResponse : BaseResponse
    {
        public Classe[] classes = new Classe[] { };
    }
}