namespace ParkingSystem.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string UserId { get; set; }

        public UserNotFoundException(string userid)
        {
            UserId = userid;
        }

        public UserNotFoundException(string? message, string userid) : base(message)
        {
            UserId = userid;
        }

        public UserNotFoundException(string? message, Exception? innerException, string userid) : base(message, innerException)
        {
            UserId = userid;
        }
    }
}
