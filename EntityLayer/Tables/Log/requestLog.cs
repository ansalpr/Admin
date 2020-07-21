namespace AdminAPI.Models.Log
{
    public class RequestLog
    {
        private string _request;
        private string _response;
        private string _workFlow;
        private string _methodName;
        private string _className;
        private string _tui;
        private string _ip;
        public string Request
        {
            get { return _request; }
            set { _request = value; }
        }
        public string Response
        {
            get { return _response; }
            set { _response = value; }
        }
        public string WorkFlow
        {
            get { return _workFlow; }
            set { _workFlow = value; }
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