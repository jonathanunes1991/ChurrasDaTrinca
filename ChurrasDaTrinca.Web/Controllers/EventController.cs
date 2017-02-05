using ChurrasDaTrinca.Service;
using ChurrasDaTrinca.Web.Helpers;
using ChurrasDaTrinca.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ChurrasDaTrinca.Web.Controllers
{
    public class EventController : Controller
    {
        private EventService Service;

        public EventController()
        {
            this.Service = new EventService();
        }

        /// <summary>
        /// Método responsável por exibir a lista de Eventos
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Schedule);
            SessionManager.Instance.CleanCurrentEventId();

            var entities = this.Service.GetAll();

            if (!SessionManager.Instance.GetPastEventsValue())
                entities = entities.Where(x => x.Date >= DateTime.Today).ToList();

            return View(new EventViewModel() { Entities = entities });
        }

        /// <summary>
        /// Método responsável por exibir as informações do Evento
        /// </summary>
        /// <param name="id">Identificador do Evento a ser exibido</param>
        /// <returns></returns>
        public ActionResult Details(long id)
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Schedule);
            SessionManager.Instance.CleanCurrentEventId();

            return View(new EventViewModel() { Entity = this.Service.Get(id) });
        }

        /// <summary>
        /// Método responsável por exibir a tela para inclusão de um Evento
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Event);
            SessionManager.Instance.CleanCurrentEventId();

            return View("Manipulate", new EventViewModel() { Entity = new Model.Event() });
        }

        /// <summary>
        /// Método responsável por adicionar/editar um Evento
        /// </summary>
        /// <param name="viewModel">Valores digitados pelo usuário</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manipulate(EventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = Service.Post(viewModel.Entity);

                if (viewModel.SubmitType.Equals(SubmitType.AddParticipantToEvent))
                {
                    SessionManager.Instance.CurrentEventId = entity.Id;

                    return RedirectToAction("ManipulateWithInteration", "Participant");
                }
                else
                {
                    SessionManager.Instance.CleanCurrentEventId();

                    return RedirectToAction("List");
                }
            }

            return View("Manipulate", viewModel);
        }

        /// <summary>
        /// Método responsável por continuar com a inclusão/edição do Evento
        /// </summary>
        /// <returns></returns>
        public ActionResult ManipulateWithInteration()
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Event);

            var viewModel = new EventViewModel() { Entity = Service.Get(SessionManager.Instance.CurrentEventId) };
            SessionManager.Instance.CleanCurrentEventId();

            return View("Manipulate", viewModel);
        }

        /// <summary>
        /// Método responsável por exibir a tela de edição de um Evento
        /// </summary>
        /// <param name="id">Identificador do Evento a ser editado</param>
        /// <returns></returns>
        public ActionResult Edit(long id)
        {
            SessionManager.Instance.SetCurrentMenu(MenuType.Schedule);
            SessionManager.Instance.CleanCurrentEventId();

            return View("Manipulate", new EventViewModel() { Entity = this.Service.Get(id) });
        }

        /// <summary>
        /// Método responsável por excluir um Evento
        /// </summary>
        /// <param name="id">Identificador do Evento a ser excluído</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(long id)
        {
            SessionManager.Instance.CleanCurrentEventId();
            Service.Delete(id);

            return Json(new { redirectResult = Url.Action("List") });
        }

        /// <summary>
        /// Método responsável por alterar a configuração de visualização da lista de Eventos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePastEventsValue()
        {
            SessionManager.Instance.CleanCurrentEventId();
            SessionManager.Instance.ChangePastEventsValue();

            return Json(new { redirectResult = Url.Action("List") });
        }
    }
}