using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForeverPetsHome.Domain
{
    public class RefreshToken {
        [Key]
        public int Id {get; set;}
        [ForeignKey("UserId")]
        public AppUser AppUser {get; set;}
        public Guid UserId {get; set;}
        public string Token {get; set;}
        public DateTime Expires {get; set;} = DateTime.UtcNow.AddDays(30);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked {get; set;}
        public bool IsActive => Revoked == null && !IsExpired;
        public DateTime? CreatedOn {get; set;}
    }
}