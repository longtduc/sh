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
        public Result<int> Create(StatementVM vm)
        {
            var entity = new Statement(vm.Description);
            try
            {
                _repo.InsertOrUpdate(entity);
                _repo.Save();
                return Result.OK<int>(entity.Id);
            }
            catch (Exception ex)
            {
                return Result.Fail<int>(ex.Message);
            }
        }
        public Result<int> Update(StatementVM vm)
        {

            var entity = _repo.Find(vm.Id);

            if (entity == null)
                return Result.Fail<int>("Statement not found!");

            entity.Description = vm.Description;

            try
            {
                _repo.InsertOrUpdate(entity);
                _repo.Save();
            }
            catch (Exception ex)
            {
                return  Result.Fail<int>(ex.Message);
            }

            return Result.OK<int>(vm.Id);
        }
        public Result<int> Delete(int id)
        {
            try
            {
                _repo.Delete(id);
                _repo.Save();
                return Result.OK<int>(id);
            }
            catch (Exception ex)
            {
                return Result.Fail<int>(ex.Message);
            }

        }
    }
}