using System.ComponentModel.DataAnnotations;

namespace ForeverPetsHome.Application.Command.AdoptionManagement.Request
{
    public class AdoptionRequest
    {
        [Required(ErrorMessage = "El campo PetId es obligatorio.")]
        public int PetId { get; set; }
        [Required(ErrorMessage = "El campo CustomerId es obligatorio.")]
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "El campo VolunteerId es obligatorio.")]
        public Guid VolunteerId { get; set; } 
    }
}