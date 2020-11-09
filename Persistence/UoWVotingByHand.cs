using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persistence
{
    public class UoWVotingByHand : IUoWVotingByHand
    {
        private ShareHolderContext context;
        private IRepository<ShareHolder> shareHolders;
        private IRepository<VotingByHand> votingByHands;
        private IRepository<Statement> statements;
        public UoWVotingByHand(ShareHolderContext context)
        {
            this.context = context;
        }

        public UoWVotingByHand()
        {
            CreateDbContext();
        }
        public IRepository<ShareHolder> ShareHolders
        {
            get
            {
                if (shareHolders == null)
                    shareHolders = new ShareHolderRepo(context);
                return shareHolders;
            }
        }

        public IRepository<VotingByHand> VotingByHands
        {
            get
            {
                if (votingByHands == null)
                    votingByHands = new VotingByHandRepo(context);
                return votingByHands;
            }
        }

        public IRepository<Statement> Statements
        {
            get 
            {
                if (statements == null)
                    statements = new StatementRepo(context);
                return statements;
            }
        }

        protected void CreateDbContext()
        {
            context = new ShareHolderContext();
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
        }
    }
}