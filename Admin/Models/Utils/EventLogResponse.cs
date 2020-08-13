using Admin.Base;
using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Utils
{
    public class EventLogResponse : BaseResponse
    {

        public EventLogs[] eventlogs = new EventLogs[] { };

    }
}