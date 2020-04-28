using KofCWebSite.Core.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Dto.Results
{
    public class BaseAddResult<T> 
    {
        public T Entity { get; set; }
        public AddResultStatus Status { get; set; }
        public Exception Exception { get; set; }
    }
}
