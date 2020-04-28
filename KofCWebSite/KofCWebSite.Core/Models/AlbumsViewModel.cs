
using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class AlbumsViewModel : BaseViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
    }
}
