using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;

namespace KofCWebSite.Core.Services
{
    public class RequestTypesService : IRequestTypesService
    {
        private IKofCRepository<RequestType> _RequesttypeRepo;
        public RequestTypesService(IKofCRepository<RequestType> RequesttypeRepository)
        {
            _RequesttypeRepo = RequesttypeRepository;
        }

        public IEnumerable<RequestType> GetAllRequesttypes()
        {
            return _RequesttypeRepo.GetAll();
        }

        public async Task<RequestType> GetRequesttypeByIdAsync(int Id)
        {
            return await _RequesttypeRepo.GetEntityByIdAsync(Id);
        }

    }
}
