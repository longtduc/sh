using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class VotingByHand
    {
        public int Id { get; set; }
        public bool IsVoted { get; set; }
        public ICollection<VotingByHandLine> VotingByHandLines { get; private set; }
        public string ShareHolderCode { get; set; }
        public string ShareHolderName { get; set; }
        public int NumberOfShares { get; set; }
        public Nullable<int> ShareHolderId { get; set; }

        public ShareHolder ShareHolder { get; set; }

        public VotingByHand()
        {
            VotingByHandLines = new List<VotingByHandLine>();
        }

        public VotingByHand(ShareHolder shareHolder, List<Statement> statements)
        {
            if (shareHolder.StatusAtMeeting == StatusAtMeeting.Absent)
                throw new ArgumentException("Could create VotingByHandCard for Absent ShareHolders");
            VotingByHandLines = new List<VotingByHandLine>();

            this.ShareHolderId = shareHolder.ShareHolderId;
            this.ShareHolder = shareHolder;
            this.ShareHolderCode = shareHolder.ShareHolderCode;
            this.ShareHolderName = shareHolder.Name;
            this.NumberOfShares = shareHolder.NumberOfShares;

            foreach (var item in statements)
            {
                var line = new VotingByHandLine()
                {
                    StatementId = item.Id,
                    StatementDesc = item.Description
                };
                this.VotingByHandLines.Add(line);
            }
        }

        public void Vote(ICollection<VotingByHandLine> votingByHandLines)
        {
            foreach (var item in votingByHandLines)
            {
                var entity = VotingByHandLines.FirstOrDefault(v => v.Id == item.Id);
                if (entity == null)
                    throw new InvalidOperationException();
                entity.VotingOption = item.VotingOption;
            }
        }

    }

}