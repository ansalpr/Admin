using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Base
{
    public class WorkFlowConstants
    {
        public const string Login = "LOGIN";
        public const string Registration = "REGISTRATION";

        #region ADMIN
        public const string NDepartment = "NEW-DEPARTMENT";
        public const string EDepartment = "EDIT-DEPARTMENT";
        public const string DDepartment = "DELETE-DEPARTMENT";
        public const string VDepartment = "VIEW-DEPARTMENT";

        public const string NCurriculum = "NEW-CURRICULUM";
        public const string ECurriculum = "EDIT-CURRICULUM";
        public const string DCurriculum = "DELETE-CURRICULUM";
        public const string VCurriculum = "VIEW-CURRICULUM";

        public const string NCurrency = "NEW-CURRENCY";
        public const string ECurrency = "EDIT-CURRENCY";
        public const string DCurrency = "DELETE-CURRENCY";
        public const string VCurrency = "VIEW-CURRENCY";

        public const string NDesignation = "NEW-DESIGNATION";
        public const string EDesignation = "EDIT-DESIGNATION";
        public const string DDesignation = "DELETE-DESIGNATION";
        public const string VDesignation = "VIEW-DESIGNATION";

        public const string NPermission = "NEW-PERMISSION";
        public const string EPermission = "EDIT-PERMISSION";
        public const string DPermission = "DELETE-PERMISSION";
        public const string VPermission = "VIEW-PERMISSION";
        #endregion

    }
}