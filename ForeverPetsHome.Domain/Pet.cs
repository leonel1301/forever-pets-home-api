using System.ComponentModel.DataAnnotations;

namespace ForeverPetsHome.Domain
{
    public class Pet
    {
        [Key]
        public int Id {get; set;}
        public string Names {get; set;}
        public int Age {get; set;}
        public string Breed {get; set;}
        public PetType Type {get; set;}
        public PetStatus? Status {get; set;} = PetStatus.AVAILABLE;
        
    }

    public enum PetStatus {
        ADOPTED,
        AVAILABLE,
        NOT_AVAILABLE,
        PENDING
    }

    public enum PetType {
        DOG,
        CAT
    }
}