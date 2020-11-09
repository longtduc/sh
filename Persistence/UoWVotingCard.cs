
using Domain.Entities;
using Application.Common.Interfaces;

namespace Persistence
{
    public class UoWvotingCard : IUoWVotingCard
    {
        private IShareHolderContext context;
        private IRepository<ShareHolder> shareHolders;
        private IRepository<VotingCard> votingCards;     
        public UoWvotingCard(IShareHolderContext context)
        {
            this.context = context;
        }
        public UoWvotingCard()
        {
            CreateDbContext();
        }
        public void Commit()
        {
            context.SaveChanges();
        }


        public IRepository<ShareHolder> ShareHolders
        {
            get
            {
                if (shareHolders == null)
                {
                    shareHolders = new ShareHolderRepo(context);
                }
                return shareHolders;
            }
        }       

        public IRepository<VotingCard> VotingCards
        {
            get
            {
                if ( votingCards == null)

                    votingCards = new VotingCardRepo(context);

                return votingCards;
            }
        }

        protected void CreateDbContext()
        {
            context = new ShareHolderContext();

            //Need reviewing: Do NOT enable proxied entities, else serialization fails
            //if false it will not get the associated certification and skills when we get the applicants
            context.Configuration.ProxyCreationEnabled = false;

            //Need reviewing: Load navigation properties explicitly (avoid serialization trouble)
            context.Configuration.LazyLoadingEnabled = false;

            //Need reviewing: Because Web API will perform validation, we don't need/want EF to do so
            //context.Configuration.ValidateOnSaveEnabled = false;

            //Need reviewing:  DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }
    }
}
