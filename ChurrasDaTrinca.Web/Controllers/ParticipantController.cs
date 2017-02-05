using ChurrasDaTrinca.Service;
using ChurrasDaTrinca.Web.Helpers;
using ChurrasDaTrinca.Web.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChurrasDaTrinca.Web.Controllers
{
    public class ParticipantController : Controller
    {
        private ParticipantService Service;
        private EventService EventService;

        public ParticipantController()
        {
            this.Service = new ParticipantService();
            this.EventService = new EventService();
        }

        /// <summary>
        ///  Método responsável por exibir as informações do Participante
        /// </summary>
        /// <param name="id">Identificador do Participante a ser exibido</param>
        /// <returns></returns>
        public ActionResult Details(long id)
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Schedule);
            SessionManager.Instance.CleanCurrentEventId();

            var viewModel = new ParticipantViewModel() { SubmitType = SubmitType.AddParticipant, Entity = this.Service.Get(id), };
            viewModel.Event = this.EventService.Get(viewModel.Entity.EventId);

            return View(viewModel);
        }

        /// <summary>
        /// Método responsável por exibir a tela para inclusão de um Participante
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Participant);
            SessionManager.Instance.CleanCurrentEventId();

            return View("Manipulate", new ParticipantViewModel()
            {
                SubmitType = SubmitType.AddParticipant,
                Entity = new Model.Participant(),
                Events = this.EventService.GetAll().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Description }).ToList()
            });
        }

        /// <summary>
        /// Método responsável por adicionar/editar um Participante
        /// </summary>
        /// <param name="viewModel">Valores digitados pelo usuário</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manipulate(ParticipantViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.SubmitType.Equals(SubmitType.AddParticipantToEvent))
                {
                    var entity = Service.Post(viewModel.Entity);

                    return RedirectToAction("ManipulateWithInteration", "Event");
                }
                else
                {
                    SessionManager.Instance.CleanCurrentEventId();
                    Service.Post(viewModel.Entity);

                    return RedirectToAction("Edit", "Event", new { id = viewModel.Entity.EventId });
                }
            }

            return View("Manipulate", viewModel);
        }

        /// <summary>
        /// Método responsável por continuar com a inclusão/edição do Participante
        /// </summary>
        /// <returns></returns>
        public ActionResult ManipulateWithInteration()
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Participant);

            var viewModel = new ParticipantViewModel();
            viewModel.Entity.EventId = SessionManager.Instance.CurrentEventId;
            viewModel.SubmitType = SubmitType.AddParticipantToEvent;

            return View("Manipulate", viewModel);
        }

        /// <summary>
        /// Método responsável por exibir a tela de edição de um Participante
        /// </summary>
        /// <param name="id">Identificador do Participante a ser editado</param>
        /// <returns></returns>
        public ActionResult Edit(long id)
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Schedule);
            SessionManager.Instance.CleanCurrentEventId();

            return View("Manipulate", new ParticipantViewModel()
            {
                SubmitType = SubmitType.AddParticipant,
                Entity = this.Service.Get(id),
                Events = this.EventService.GetAll().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Description }).ToList()
            });
        }

        /// <summary>
        /// Método responsável por excluir um Evento
        /// </summary>
        /// <param name="id">Identificador do Participante a ser excluído</param>
        /// <param name="eventId">Identificador do Evento para qual a tela será retornada</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(long id, long eventId)
        {
            SessionManager.Instance.CleanCurrentEventId();
            Service.Delete(id);

            return Json(new { redirectResult = Url.Action("Edit", "Event", new { id = eventId }) });
        }

    }
}