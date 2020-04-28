using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Dto.Results
{
    public class BaseResult 

    {
        public BaseResult()
        {

        }

        public BaseResult(string resultMessage)
        {
            ResultMessage = resultMessage;
        }

        public string ResultMessage { get; set; }
    }
}
