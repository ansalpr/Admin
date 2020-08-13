using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Utils
{
    public class EventLogRequest : BaseRequest
    {

        public EventLogs[] eventlogs = new EventLogs[] { };

    }
}