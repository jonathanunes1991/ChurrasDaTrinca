using System.ComponentModel.DataAnnotations;
using ChurrasDaTrinca.Language;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace ChurrasDaTrinca.Model
{
    public class Event
    {
        [Key]
        [Column(Order = 1)]
        public long Id { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "When")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "Why")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [StringLength(200, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "Observation")]
        [StringLength(200, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(MessageResource))]
        public string Observation { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "WithDrink")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [DataType(DataType.Currency)]
        public double ContributionWithDrink { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "WithoutDrink")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [DataType(DataType.Currency)]
        public double ContributionWithoutDrink { get; set; }

        public List<Participant> Participants { get; set; }

        public Event()
        {
            this.Date = DateTime.Today;
            this.Participants = new List<Participant>();
        }

        /// <summary>
        /// Método responsável por verificar se o objeto possui um ID
        /// </summary>
        /// <returns></returns>
        public bool HasId()
        {
            return this.Id > 0 ? true : false;
        }

        #region Participants Methods

        /// <summary>
        /// Método responsável por verificar se o objeto possui Participantes
        /// </summary>
        /// <returns></returns>
        public bool HasParticipants()
        {
            return this.Participants.Count() > 0;
        }

        /// <summary>
        /// Método responsável por contar a quantidade de Participantes
        /// </summary>
        /// <returns></returns>
        public long CountParticipants()
        {
            return this.Participants.Count();
        }

        /// <summary>
        /// Método responsável por somar a quantidade total a ser arrecadada dos Participantes
        /// </summary>
        /// <returns></returns>
        public double TotalToBeCollected()
        {
            double result = 0;

            if (HasParticipants())
            {
                result += (this.Participants.Count(x => x.WithDrink) * this.ContributionWithDrink);
                result += (this.Participants.Count(x => !x.WithDrink) * this.ContributionWithoutDrink);
            }

            return result;
        }
        /// <summary>
        /// Método responsável por somar a quantidade arrecadada até o momento
        /// </summary>
        /// <returns></returns>
        public double AmountAlreadyPaid()
        {
            double result = 0;

            if (HasParticipants())
                result = this.Participants.Where(x => x.Paid).Sum(x => x.Contribution);

            return result;
        }

        #endregion
    }
}