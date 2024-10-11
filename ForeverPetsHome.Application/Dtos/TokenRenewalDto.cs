namespace ForeverPetsHome.Application.Dtos
{
    public class TokenRenewalDto
    {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}