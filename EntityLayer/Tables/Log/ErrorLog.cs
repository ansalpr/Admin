
namespace AdminAPI.Models.Log
{
    public class ErrorLog
    {
        private string _remarks;
        private string _methodName;
        private string _className;
        private string _tui;
        private string _ip;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }      
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string TUI
        {
            get { return _tui; }
            set { _tui = value; }
        }
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }
}