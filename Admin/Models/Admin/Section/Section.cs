
namespace Admin.Models.Admin
{
    public class Section
    {
        private string _action;
        private string _status;
        private string _message;
        private string _SectionId;
        private string _SectionCode;
        private string _SectionName;       

        public string Id
        {
            get { return _SectionId; }
            set { _SectionId = value; }
        }
        public string Code
        {
            get { return _SectionCode; }
            set { _SectionCode = value; }
        }
        public string Name
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }        
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}