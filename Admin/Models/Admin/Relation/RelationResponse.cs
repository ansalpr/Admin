using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class RelationResponse : BaseResponse
    {
        public Relation[] relations = new Relation[] { };
    }   
}