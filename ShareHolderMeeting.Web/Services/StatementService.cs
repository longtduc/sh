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
            _repo = new StatementRepo(new ShareHolderContext());
        }
        public Result Create(StatementVM vm)
        {
            var entity = new Statement(vm.Description);
            try
            {
                _repo.InsertOrUpdate(entity);
                _repo.Save();
                return new Result(true, "", entity.Id);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Update(StatementVM vm)
        {

            var entity = _repo.Find(vm.Id);

            if (entity == null)
                return new Result(false, "Statement not found!", vm.Id);

            entity.Description = vm.Description;

            try
            {
                _repo.InsertOrUpdate(entity);
                _repo.Save();
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message, vm.Id);
            }

            return new Result(true, "", vm.Id);
        }
        public Result Delete(int id)
        {
            try
            {
                _repo.Delete(id);
                _repo.Save();
                return new Result(true, "", id);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message, id);
            }

        }
    }
}