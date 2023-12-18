using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class PasswordTokenEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
