using ShareHolderMeeting.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class ShareHodlerValidator : IValidator<ShareHolder>
    {
        private ShareHolderRepository _shareHolderRepo;
        public ShareHodlerValidator()
        {
            _shareHolderRepo = new ShareHolderRepository();
        }
        public bool IsValid(ShareHolder entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(ShareHolder entity)
        {
            if (String.IsNullOrEmpty(entity.ShareHolderCode))
                yield return "ShareHolder Code must be input ";

            if (ShareHolderIsExisted(entity))
                yield return "ShareHolder Already existed ";

            if (entity.NumberOfShares <= 0)
                yield return "Number Of Shares must be greater than 0";

            yield break;
        }

        private bool ShareHolderIsExisted(ShareHolder shareHolder)
        {
            var result = _shareHolderRepo.
                All.
                Where(s => s.ShareHolderCode == shareHolder.ShareHolderCode && s.ShareHolderId != shareHolder.ShareHolderId).
                FirstOrDefault();
            return (result != null);
        }
    }
}