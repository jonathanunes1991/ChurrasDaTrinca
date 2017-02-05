using ChurrasDaTrinca.Model;
using ChurrasDaTrinca.Repository;
using System;
using System.Collections.Generic;

namespace ChurrasDaTrinca.Service
{
    public class EventService
    {
        private EventRepository Repository;

        public EventService()
        {
            this.Repository = new EventRepository();
        }

        public Event Get(long id)
        {
            return Repository.Get(id);
        }

        public List<Event> GetAll()
        {
            return Repository.GetAll();
        }

        public Event Post(Event entity)
        {
            return Repository.Post(entity);
        }

        public void Delete(long id)
        {
            Repository.Delete(id);
        }
    }
}