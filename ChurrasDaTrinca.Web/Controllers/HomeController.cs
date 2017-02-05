using ChurrasDaTrinca.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurrasDaTrinca.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Método responsável por exibir a Home da aplicação
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            SessionManager.Instance.CurrentMenu = MenuType.Home;

            return View();
        }
    }
}