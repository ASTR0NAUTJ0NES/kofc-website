
using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models.GridModels
{
    public class ContactGridModel
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public Contact[] rows { get; set; }
        public int total { get; set; }
    }
}
