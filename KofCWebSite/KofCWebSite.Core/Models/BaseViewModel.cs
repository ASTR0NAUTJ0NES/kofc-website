using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Title = "";
        }

        public string UserName { get; set; }
        public int UserId { get; set; }
        public string ThemeName { get; set; }
        public string Title { get; set; }
        public string AlertMessage { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
