using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Tables.Json
{
    public class BaseJson
    {
        private string _tui;
        public string TUI
        {
            get { return _tui; }
            set { _tui = value; }
        }
    }
}
