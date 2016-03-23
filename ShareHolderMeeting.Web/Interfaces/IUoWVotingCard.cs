using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Interfaces;

namespace ShareHolderMeeting.Web.Interfaces
{
    public interface IUoWVotingCard
    {
        // Repositories
        IRepository<ShareHolder> ShareHolders { get; }
        IRepository<VotingCard> VotingCards { get; }    

        // Save pending changes to the data store.
        void Commit();
    }
}
