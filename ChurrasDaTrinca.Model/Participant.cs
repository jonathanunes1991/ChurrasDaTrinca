using System.ComponentModel.DataAnnotations;
using ChurrasDaTrinca.Language;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasDaTrinca.Model
{
    public class Participant
    {
        [Key]
        [Column(Order = 1)]
        public long Id { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "Name")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "Contribution")]
        public double Contribution { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "Paid")]
        public bool Paid { get; set; }
        
        public bool WithDrink { get; set; }

        [Display(ResourceType = typeof(GlobalResource), Name = "ObservationToOrganizer")]
        public string ObservationToOrganizer { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MessageResource))]
        [Display(ResourceType = typeof(GlobalResource), Name = "Event")]
        public long EventId { get; set; }

        public Participant()
        {
        }

        /// <summary>
        /// Método responsável por verificar se o objeto possui um ID
        /// </summary>
        /// <returns></returns>
        public bool HasId()
        {
            return this.Id > 0 ? true : false;
        }
    }
}