using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderMeeting.Test
{
    class CandidateRepo_Stub : ICandidateRepo
    {
        int nextId = 0;
        Dictionary<int, Candidate> candidates = new Dictionary<int, Candidate>();


        public IQueryable<Candidate> All
        {
            get { return candidates.Values.OrderBy(c => c.Id).AsQueryable(); }
        }

        public IQueryable<Candidate> AllIncluding(params System.Linq.Expressions.Expression<Func<Candidate, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            candidates.Remove(id);
        }

        public Candidate Find(int id)
        {
            var candidate = new Candidate();
            var found = candidates.TryGetValue(id, out candidate);
            if (!found)
                throw new InvalidOperationException("Can't find the candidate with id of " + id.ToString());
            return candidate;
        }

        public void InsertOrUpdate(Candidate candidate)
        {
            if (candidate.Id == 0)
            {
                candidate.Id = ++nextId;
                candidates[nextId] = candidate;
            }
            else
            {
                candidates[candidate.Id] = candidate;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void InitializeData()
        {

            candidates.Add(++nextId, new Candidate() { Id = nextId, Name = "Obama" });

            candidates.Add(++nextId, new Candidate() { Id = nextId, Name = "Bush" });
        }
    }



}
