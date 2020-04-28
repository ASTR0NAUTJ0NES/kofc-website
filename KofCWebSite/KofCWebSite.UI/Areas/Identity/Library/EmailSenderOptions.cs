using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.UI.Areas.Identity.Library
{
    public class EmailSenderOptions : SendGridClientOptions 
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
