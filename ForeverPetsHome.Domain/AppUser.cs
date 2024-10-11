using System.ComponentModel.DataAnnotations;

namespace ForeverPetsHome.Domain{
    public class AppUser
    {
        [Key]
        public Guid Id {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public EnumRoles Roles {get; set;} = EnumRoles.Volunteer;
        public string Names {get; set;} 
        public string LastName {get; set;}
        public string MotherLastName {get; set;}
        public EnumStatus Status {get; set;} = EnumStatus.Active;
        public List<Adoption> Adoptions {get; set;}
    }

    public enum EnumStatus {
        Active,
        Inactive
    }

    public enum EnumRoles {
        Volunteer,
        Admin,
        Customer
    }
}