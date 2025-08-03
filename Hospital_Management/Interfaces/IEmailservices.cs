namespace Hospital_Management.Interfaces
{
    public interface IEmailservices
    {
        bool SendEmail(string toEmail, string subject, string body);
    }
}
