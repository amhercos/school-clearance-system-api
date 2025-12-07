    public class LoginResult
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool Succeeded { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; }
}

