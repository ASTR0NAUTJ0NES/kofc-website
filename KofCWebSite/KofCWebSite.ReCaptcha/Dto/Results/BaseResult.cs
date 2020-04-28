using KofCWebSite.ReCaptcha.enums;
using KofCWebSite.ReCaptcha.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.ReCaptcha.Dto.Results
{
    public class BaseResult
    {
        public BaseResult()
        {

        }

        public BaseResult(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public BaseResult(ResultStatus resultStatus, ResultErrorMessage resultErrorMessage)
        {
            ResultStatus = resultStatus;
            ResultStatusMessages = new ResultErrorMessage[]
            {
                resultErrorMessage
            };
        }

        public BaseResult(ResultStatus resultStatus, ResultErrorMessage[] resultErrorMessages)
        {
            ResultStatus = resultStatus;
            ResultStatusMessages = resultErrorMessages;
        }

        public string HttpResponseBody { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public ResultErrorMessage[] ResultStatusMessages { get; set; }
    }
}
