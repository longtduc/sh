using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Interfaces;
using System.Linq;
using System.Transactions;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class ShareHolderRepository_Test
    {
        private ShareHolderRepository _shareHolderRepo;
        
        [TestInitialize]
        public void Init()
        {
            _shareHolderRepo = new ShareHolderRepository();
        
        }
        [TestMethod]
        public void CreateShareHolder_ThenOneMoreShareHolderSaved()
        {
            var count = _shareHolderRepo.All.Count();
            var countAfter = 0;
            using (var scope = new TransactionScope())
            {
                var sh = new ShareHolder() { Name = "new name", NumberOfShares = 1000, ShareHolderCode = "xxx" };
                _shareHolderRepo.InsertOrUpdate(sh);
                _shareHolderRepo.Save();
                countAfter = _shareHolderRepo.All.Count();
            }
            Assert.AreEqual(count + 1, countAfter);
        }
    }
}
