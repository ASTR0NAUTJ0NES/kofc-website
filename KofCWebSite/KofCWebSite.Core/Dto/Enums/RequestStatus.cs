using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Dto.Enums
{
    public enum RequestStatus : int
    {
        Requested = 0,
        Accepted = 1,
        Active = 2,
        Deleted = 3
    }
}
