using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IRequestTypesService
    {
        IEnumerable<RequestType> GetAllRequesttypes();
        Task<RequestType> GetRequesttypeByIdAsync(int Id);
    }
}
