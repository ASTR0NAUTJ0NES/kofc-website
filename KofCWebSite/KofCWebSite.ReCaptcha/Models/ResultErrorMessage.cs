using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.ReCaptcha.Models
{
    public class ResultErrorMessage
    {
        public ResultErrorMessage()
        {

        }

        public ResultErrorMessage(string id, string msg)
        {
            Id = id;
            Message = msg;
        }

        public string Id { get; set; }
        public string Message { get; set; }
    }
}
