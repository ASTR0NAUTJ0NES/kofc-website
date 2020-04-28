using System;
using System.Collections.Generic;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class RequestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}

