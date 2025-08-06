namespace Hospital_Management.Interfaces
{
    public interface IEmailservice
    {
        bool SendEmail(string toEmail, string subject, string body);
    }
}
