using ChurrasDaTrinca.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace ChurrasDaTrinca.Repository
{
    public class EventRepository
    {
        private string EventPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "..\\ChurrasDaTrinca.Repository\\Database\\Event.txt");

        private ParticipantRepository ParticipantRepository;

        public EventRepository()
        {
            this.ParticipantRepository = new ParticipantRepository();
        }

        public Event Get(long id)
        {
            var result = GetAll().FirstOrDefault(x => x.Id == id);

            return result != null ? result : new Event();
        }

        public List<Event> GetAll()
        {
            var result = new List<Event>();

            if (!result.Any() && File.Exists(EventPath))
                result = new JavaScriptSerializer().Deserialize<List<Event>>(File.ReadAllText(EventPath));

            if (result == null)
                return new List<Event>();

            result.ForEach(x => x.Participants = ParticipantRepository.GetByEventId(x.Id));

            return result.OrderBy(x => x.Date).ToList();
        }

        public Event Post(Event entity)
        {
            var results = GetAll().ToList();

            if (entity.HasId())
            {
                var deleteItens = results.Where(x => x.Id == entity.Id).ToList();
                deleteItens.ForEach(x => results.Remove(x));

                results.Add(entity);
            }
            else
            {
                long id = 0;

                if (results.Any())
                    id = results.Max(x => x.Id);

                entity.Id = id + 1;
                results.Add(entity);
            }

            if (File.Exists(EventPath))
                File.WriteAllText(EventPath, new JavaScriptSerializer().Serialize(results));

            return entity;
        }

        public void Delete(long id)
        {
            var entities = GetAll();

            if (entities != null && entities.Any())
            {
                ParticipantRepository.DeleteByEventId(id);

                var result = entities.Where(x => x.Id != id).ToList();

                if (File.Exists(EventPath))
                    File.WriteAllText(EventPath, new JavaScriptSerializer().Serialize(result));
            }
        }
    }
}