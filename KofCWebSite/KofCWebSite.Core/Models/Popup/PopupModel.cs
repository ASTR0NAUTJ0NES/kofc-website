using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models.Popup
{
    public class PopupModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public PopupButtonModel[] Buttons { get; set; }
    }
}
