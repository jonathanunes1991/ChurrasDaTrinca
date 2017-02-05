using ChurrasDaTrinca.Model;
using ChurrasDaTrinca.Repository;
using System;
using System.Collections.Generic;

namespace ChurrasDaTrinca.Service
{
    public class ParticipantService
    {
        private ParticipantRepository Repository;

        public ParticipantService()
        {
            this.Repository = new ParticipantRepository();
        }

        public Participant Get(long id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Participant> GetAll()
        {
            return Repository.GetAll();
        }

        public Participant Post(Participant entity)
        {
            return Repository.Post(entity);
        }

        public void Delete(long id)
        {
            Repository.Delete(id);
        }
    }
}