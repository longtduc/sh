using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class CandidateRepo_Stub_Test
    {
        private CandidateRepo_Stub _stub;

        [TestInitialize]
        public void Init()
        {
            _stub = new CandidateRepo_Stub();
        
            _stub.InitializeData();
        }

        [TestMethod]
        public void All_NumberOfCandidatesIsTwo()
        {
            var sut = _stub.All.ToList();
            Assert.AreEqual(2, sut.Count());

        }

        [TestMethod]
        public void All_TheFirstIsAKhoi()
        {
            var sut = _stub.All.ToList();
            Assert.AreEqual("Obama", sut.FirstOrDefault().Name);

        }

        [TestMethod]
        public void Find_ReturnTheFirst()
        {
            var sut = _stub.Find(1);
            Assert.AreEqual("Obama", sut.Name);
        }

        [TestMethod]
        public void AddCandiate_NumberOfCandidatesIsThree()
        {
            _stub.InsertOrUpdate(new Candidate() { Name = "Hilary Cliton" });
            var sut = _stub.All.ToList();
            Assert.AreEqual(3, sut.Count());
        }

        [TestMethod]
        public void UpdateCandidate_NumberOfCandidatesIsTheSame()
        {
            var update = new Candidate() { Id = 1, Name = "Obama 2" };
            _stub.InsertOrUpdate(update);

            var sut = _stub.All.ToList();
            Assert.AreEqual(2, sut.Count);
            Assert.AreEqual("Obama 2", _stub.Find(1).Name);
        }

        [TestMethod]
        public void DeleteCandidate_NumberOfCandidatesDecreaseOne()
        {
            _stub.Delete(1);

            var sut = _stub.All.ToList();
            Assert.AreEqual(1, sut.Count);            
        }
    }
}
