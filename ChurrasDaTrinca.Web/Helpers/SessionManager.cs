using System;
using System.Collections.Generic;

namespace ChurrasDaTrinca.Web.Helpers
{
    public sealed class SessionManager
    {
        public static SessionManager Instance { get; private set; }

        public MenuType CurrentMenu { get; set; }

        public long CurrentEventId { get; set; }

        private bool PastEvents { get; set; }

        #region Construtor

        static SessionManager()
        {
            Instance = new SessionManager();
        }

        #endregion

        #region Menu

        /// <summary>
        /// Método responsável por selecionar o Menu onde o Usuário se encontra
        /// </summary>
        /// <param name="menuType"></param>
        public void SetCurrentMenu(MenuType menuType)
        {
            this.CurrentMenu = menuType;
        }

        #endregion

        #region Current Event
        
        /// <summary>
        /// Método responsável por limpar a sessão com o Evento corrente no momento
        /// </summary>
        public void CleanCurrentEventId()
        {
            this.CurrentEventId = 0;
        }

        #endregion

        #region Past Events

        /// <summary>
        /// Método responsável por informar a configuração de visualização da lista de Eventos
        /// </summary>
        /// <returns></returns>
        public bool GetPastEventsValue()
        {
            return this.PastEvents;
        }

        /// <summary>
        /// Método responsável por alterar a configuração de visualização da lista de Eventos
        /// </summary>
        public void ChangePastEventsValue()
        {
            this.PastEvents = !this.PastEvents;
        }

        #endregion
    }
}