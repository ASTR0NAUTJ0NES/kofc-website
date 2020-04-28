using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.ReCaptcha.enums
{
    public enum ResultStatus
    {
        NotFound = 0,
        Error = 1,
        BadRequest = 2,
        Ok = 3,
        Unauthorized = 4,
        Conflict = 5
    }
}
