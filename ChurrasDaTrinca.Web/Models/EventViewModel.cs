using ChurrasDaTrinca.Web.Helpers;
using System.Collections.Generic;

namespace ChurrasDaTrinca.Web.Models
{
    public class EventViewModel
    {
        public Model.Event Entity { get; set; }

        public List<Model.Event> Entities { get; set; }

        /// <summary>
        /// Propriedade responsável por informar a o tipo de acesso à controller
        /// </summary>
        public SubmitType SubmitType { get; set; }

        /// <summary>
        /// Propriedade responsável por informar a configuração de visualização da lista de Eventos
        /// </summary>
        public bool PastEvents
        {
            get
            {
                return SessionManager.Instance.GetPastEventsValue();
            }
        }

        public EventViewModel()
        {
            this.Entity = new Model.Event();
            this.Entities = new List<Model.Event>();
        }
    }
}