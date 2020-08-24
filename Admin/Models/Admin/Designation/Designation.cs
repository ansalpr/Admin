
namespace Admin.Models.Admin
{
    public class Designation
    {
        private string _action;
        private string _status;
        private string _message;
        private string _DesignationId;
        private string _DesignationCode;
        private string _DesignationName;
        

        public string Id
        {
            get { return _DesignationId; }
            set { _DesignationId = value; }
        }
        public string Code
        {
            get { return _DesignationCode; }
            set { _DesignationCode = value; }
        }
        public string Name
        {
            get { return _DesignationName; }
            set { _DesignationName = value; }
        }        
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}