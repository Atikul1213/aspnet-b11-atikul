namespace Demo.Domain.Utilities
{
    public interface IEmailUtility
    {
        void SendEmail(string receiverEmail, string receiverName, string subject, string body);
    }
}
