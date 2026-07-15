namespace ColegioMaster.DtoModels.Auth
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public bool ChangedPassword { get; set; }
        public string Token { get; set; }
    }
}