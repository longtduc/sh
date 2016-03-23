using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Interfaces
{
    public interface IUoWVotingByHand
    {
        IRepository<ShareHolder> ShareHolders { get; }
        IRepository<VotingByHand> VotingByHands { get; }
        IRepository<Statement> Statements { get; }
    }
}