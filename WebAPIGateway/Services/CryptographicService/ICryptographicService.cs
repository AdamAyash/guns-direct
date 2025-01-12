namespace WebAPIGateway.Services.CryptographicService
{
    public interface ICryptographicService
    {
        public byte[] GenerateSalt();
        public string HashPassword(string password, byte[] salt);
    }
}
