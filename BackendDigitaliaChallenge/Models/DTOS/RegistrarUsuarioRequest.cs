namespace BackendDigitaliaChallenge.Models.DTOS
{
    public class RegistrarUsuarioRequest
    {
        public string nombres { get; set; }
        public string? apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
