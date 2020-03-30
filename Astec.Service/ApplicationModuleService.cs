using Astec.Data.Infastructure;
using Astec.Data.Repositories;
using Astec.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astec.Service
{
    public interface IApplicationModuleService
    {
        void Add(ApplicationModule module);
        void Update(ApplicationModule module);
        // void Delete(int id);
        ApplicationModule Delete(int id);
        IEnumerable<ApplicationModule> GetAll();
        ApplicationModule GetById(int id);
        void SaveChanges();
    }
    public class ApplicationModuleService : IApplicationModuleService
    {
        IApplicationModuleRepository _moduleRepository;
        IUnitOfWork _unitOfWork;
        public ApplicationModuleService(IApplicationModuleRepository moduleRepository, IUnitOfWork unitOfWork)
        {
            this._moduleRepository = moduleRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(ApplicationModule module)
        {
            _moduleRepository.Add(module);
        }

        public ApplicationModule Delete(int id)
        {
            return _moduleRepository.Delete(id);
        }

        public IEnumerable<ApplicationModule> GetAll()
        {
            return _moduleRepository.GetAll();
        }

        public ApplicationModule GetById(int id)
        {
            return _moduleRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
             _unitOfWork.Commit();
        }

        public void Update(ApplicationModule module)
        {
            _moduleRepository.Update(module);
        }
    }
}
