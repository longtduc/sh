using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Services
{
    public class StatementService
    {
        private StatementRepo _repo;
        public StatementService()
        {
            _repo = new StatementRepo();
        }
        public int Create(StatementVM vm)
        {
            var entity = new Statement()
            {
                Id = vm.Id,
                Description = vm.Description
            };

            _repo.InsertOrUpdate(entity);
            _repo.Save();

            return entity.Id;
        }
        public int Update(StatementVM vm)
        {
            var entity = new Statement()
            {
                Id = vm.Id,
                Description = vm.Description
            };

            _repo.InsertOrUpdate(entity);
            _repo.Save();

            return entity.Id;
        }
        public void Delete(int id) 
        {
            _repo.Delete(id);
            _repo.Save();        
        }
    }
}