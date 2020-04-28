using KofCWebSite.ReCaptcha.enums;
using KofCWebSite.ReCaptcha.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.ReCaptcha.Dto.Results
{
    public class SiteVerifyResult : BaseResult
    {
        public SiteVerifyResult()
        {
            ResultStatus = ResultStatus.Ok;
        }

        public SiteVerifyResult(ResultStatus resultStatus)
            : base(resultStatus)
        {
        }

        public SiteVerifyResult(ResultStatus resultStatus, ResultErrorMessage resultErrorMessage)
            : base(resultStatus, resultErrorMessage)
        {
        }

        public SiteVerifyResult(ResultStatus resultStatus, ResultErrorMessage[] resultErrorMessages)
            : base(resultStatus, resultErrorMessages)
        {
        }

        public bool IsVerified { get; set; } = false;
    }
}
