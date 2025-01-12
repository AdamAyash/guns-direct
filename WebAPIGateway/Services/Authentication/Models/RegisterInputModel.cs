namespace WebAPIGateway.Services.Authentication.Models
{
    public class RegisterInputModel
    {
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Password { get; set; }
        public required string ConfirmedPassword { get; set; }
    }
}
