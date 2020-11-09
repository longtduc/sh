using Application.Common.Interfaces;
using Domain.Entities;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.CqrsExceptionNotFound
{
    public enum CreateStatementStatus
    {
        Successful
    }
    public class CreateStatementCommandHander
    {
        private readonly IShareHolderContext _context;
        public CreateStatementCommandHander(IShareHolderContext context)
        {
            _context = context;
        }

        public void Handle(CreateStatementCommand command)
        {
            //Return(ValidateCommand(command));

            //var location = new Domain.Movie(Guid.NewGuid(), command.Title, command.ReleaseDate, command.RunningTimeMinutes);
            var location = new Statement(command.Description);

            //_repository.Save(location);
        }
        protected CreateStatementStatus ValidateCommand(CreateStatementCommand command)
        {
            return CreateStatementStatus.Successful;
        }
    }
    
}