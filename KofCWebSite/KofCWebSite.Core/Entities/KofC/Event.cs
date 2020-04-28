using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KofCWebsite.Core.Entities.KofC
{
    [Serializable]
    public partial class Event
    {
        public int Id { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public int? Contactid { get; set; }
        public string Title { get; set; }
        public char? Status { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
