using ChurrasDaTrinca.Web.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChurrasDaTrinca.Web.Models
{
    public class ParticipantViewModel
    {
        public Model.Participant Entity { get; set; }

        public List<Model.Participant> Entities { get; set; }

        public Model.Event Event { get; set; }

        public List<SelectListItem> Events { get; set; }

        public SubmitType SubmitType { get; set; }

        /// <summary>
        /// Propriedade responsável por informar se é possível realizar a visualização/edição/exclusão dos Participantes
        /// </summary>
        public bool CanEdit { get; set; }

        public ParticipantViewModel()
        {
            this.Entity = new Model.Participant();
            this.Entities = new List<Model.Participant>();
            this.Event = new Model.Event();
            this.Events = new List<SelectListItem>();
            this.CanEdit = true;
        }
    }
}