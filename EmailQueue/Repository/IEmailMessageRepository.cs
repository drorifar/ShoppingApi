using EmailQueue.Entities;

namespace EmailQueue.Repository
{
    public interface IEmailMessageRepository
    {
        Task<EmailMessage> CreateEmail(EmailMessage message);
        Task<IEnumerable<EmailMessage>> GetPendingMessages(int? amount = null);
        Task MarkAsSent(int id);

        Task<EmailMessage> GetEmail(int id);
    }
}
