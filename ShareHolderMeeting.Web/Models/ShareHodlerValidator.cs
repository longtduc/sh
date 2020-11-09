using Application.Common.Interfaces;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class ShareHodlerValidator : IValidator<ShareHolder>
    {
        private readonly IShareHolderContext _shareHolderRepo;
        public ShareHodlerValidator(IShareHolderContext repo)
        {
            _shareHolderRepo = repo; // new ShareHolderRepo(ShareHolderContext context);
        }
        public bool IsValid(ShareHolder entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(ShareHolder entity)
        {
            if (String.IsNullOrEmpty(entity.ShareHolderCode))
                yield return "ShareHolder Code must be input ";

            if (String.IsNullOrEmpty(entity.Name))
                yield return "ShareHolder Name is required";

            if (ShareHolderIsExisted(entity))
                yield return "ShareHolder Already existed ";

            if (entity.NumberOfShares <= 0)
                yield return "Number Of Shares must be greater than 0";

            yield break;
        }

        private bool ShareHolderIsExisted(ShareHolder shareHolder)
        {
            var result = _shareHolderRepo.
                ShareHolders.
                Where(s => s.ShareHolderCode == shareHolder.ShareHolderCode && s.ShareHolderId != shareHolder.ShareHolderId).
                FirstOrDefault();
            return (result != null);
        }
    }
}