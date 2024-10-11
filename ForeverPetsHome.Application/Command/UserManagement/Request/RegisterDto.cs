using ForeverPetsHome.Domain;

namespace ForeverPetsHome.Application.Command.UserManagement.Request
{
    public class RegisterDto
    {
        public string Email {get; set;}
        public string Password {get; set;}
        public EnumRoles Roles {get; set;} = EnumRoles.Volunteer;
        public string Names {get; set;} 
        public string LastName {get; set;}
        public string MotherLastName {get; set;}    
    }
}