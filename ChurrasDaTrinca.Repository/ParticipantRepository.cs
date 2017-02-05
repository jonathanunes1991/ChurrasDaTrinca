using ChurrasDaTrinca.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace ChurrasDaTrinca.Repository
{
    public class ParticipantRepository
    {
        private string ParticipantPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "..\\ChurrasDaTrinca.Repository\\Database\\Participant.txt");

        public Participant Get(long id)
        {
            var result = GetAll().FirstOrDefault(x => x.Id == id);

            return result != null ? result : new Participant();
        }

        public List<Participant> GetByEventId(long eventId)
        {
            return GetAll().Where(x => x.EventId == eventId).ToList();
        }

        public List<Participant> GetAll()
        {
            var result = new List<Participant>();

            if (!result.Any() && File.Exists(ParticipantPath))
                result = new JavaScriptSerializer().Deserialize<List<Participant>>(File.ReadAllText(ParticipantPath));

            return result != null ? result.OrderBy(x => x.Name).ToList() : new List<Participant>();
        }

        public Participant Post(Participant entity)
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

            if (File.Exists(ParticipantPath))
                File.WriteAllText(ParticipantPath, new JavaScriptSerializer().Serialize(results));

            return entity;
        }

        public void Delete(long id)
        {
            var entities = GetAll();

            if(entities != null && entities.Any())
            {
                var result = entities.Where(x => x.Id != id).ToList();

                if (File.Exists(ParticipantPath))
                    File.WriteAllText(ParticipantPath, new JavaScriptSerializer().Serialize(result));
            }
        }

        public void DeleteByEventId(long eventId)
        {
            var entities = GetAll();

            if (entities != null && entities.Any())
            {
                var result = entities.Where(x => x.EventId != eventId).ToList();

                if (File.Exists(ParticipantPath))
                    File.WriteAllText(ParticipantPath, new JavaScriptSerializer().Serialize(result));
            }
        }
    }
}