namespace ForeverPetsHome.Application.Command.AdoptionManagement.Response
{
    public class AdoptionResponse
    {
        public int PetId {get; set;}
        public Guid CustomerId {get; set;}
        public Guid VolunteerId {get; set;}
        public DateTime AdoptionDate {get; set;}
    }
}