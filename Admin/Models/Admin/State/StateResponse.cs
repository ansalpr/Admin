using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class StateResponse :BaseResponse
    {
        public State[] states = new State[] { };
    }   
}