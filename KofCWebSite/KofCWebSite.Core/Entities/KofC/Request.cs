using System;
using System.Collections.Generic;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class Request
    {
        public int Id { get; set; }
        public int? RequestTypeId { get; set; }
        public string Subject { get; set; }
        public string RequestBody { get; set; }
        public int? Status { get; set; }
        public DateTime? Createdon { get; set; }
        public DateTime? Receivedon { get; set; }
        public DateTime? Closedon { get; set; }
        public int? ContactId { get; set; }
        public virtual RequestType RequestType { get; set; }
        public virtual Contact RequestContact { get; set; }
    }
}
