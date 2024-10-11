using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForeverPetsHome.Domain
{
    public class Adoption
    {
        [Key]
        public int Id { get; set; }
        public Pet? Pet { get; set; }
        public int PetId { get; set; }
        [ForeignKey("CustomerId")]
        public required AppUser Customer { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("VolunteerId")]
        public required AppUser Volunteer { get; set; }
        public Guid VolunteerId { get; set; }
        public DateTime AdoptionDate { get; set; }
    }
}