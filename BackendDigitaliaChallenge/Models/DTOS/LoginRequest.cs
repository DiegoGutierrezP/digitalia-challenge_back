using System.ComponentModel.DataAnnotations;

namespace BackendDigitaliaChallenge.Models.DTOS
{
    public class LoginRequest
    {
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }
    }
}
