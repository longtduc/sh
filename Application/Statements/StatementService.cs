using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Statements
{
    public class StatementService
    {
        private IShareHolderContext _context;
        public StatementService(IShareHolderContext context)
        {
            _context = context;
        }
        public Result<int> Create(StatementDto vm)
        {
            var entity = new Statement(vm.Description);
            try
            {
                _context.Statements.Add(entity);
                _context.SaveChanges();
                return Result.OK<int>(entity.Id);
            }
            catch (Exception ex)
            {
                return Result.Fail<int>(ex.Message);
            }
        }
        public Result<int> Update(StatementDto vm)
        {
            int id = vm.Id;

            var entity = _context.Statements.Find(id);

            if (entity == null)
                return Result.Fail<int>($"Statement {id} not found!");

            entity.Description = vm.Description; 
            try
            {                
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return  Result.Fail<int>(ex.Message);
            }

            return Result.OK<int>(vm.Id);
        }
        public Result<int> Delete(int id)
        {
            var entry = _context.Statements.Find(id);
            if (entry == null)
                throw new InvalidOperationException($"{id} not found!");
            try
            {
                _context.Statements.Remove(entry);
                _context.SaveChanges();
                return Result.OK<int>(id);
            }
            catch (Exception ex)
            {
                return Result.Fail<int>(ex.Message);
            }

        }
    }
}