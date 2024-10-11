using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Command.UserManagement.Request
{
    public class PetRequest
    {
        public string Names {get; set;}
        public int? Age {get; set;}
        public string Breed {get; set;}
        public PetType? Type {get; set;}
        public PetStatus? Status {get; set;}
    }
}