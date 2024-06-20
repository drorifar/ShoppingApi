using EmailQueue.Context;
using EmailQueue.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmailQueue.Repository
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        private MainContext _db;

        public EmailMessageRepository(MainContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<EmailMessage> CreateEmail(EmailMessage message)
        {
            message.Created = DateTime.UtcNow;
            message.Sent = false;

            await _db.EmailMessages.AddAsync(message);
            await _db.SaveChangesAsync();

            return message;
        }

        public async Task<EmailMessage> GetEmail(int id)
        {
            EmailMessage? messege = await _db.EmailMessages.FirstOrDefaultAsync(e => e.ID == id);

            return messege;
        }

        public async Task<IEnumerable<EmailMessage>> GetPendingMessages(int? amount = null)
        {
            var messages = _db.EmailMessages.Where(m => !m.Sent);                

            if (amount != null)
            {
                messages = messages.Take(amount.Value);
            }

            return await messages.ToListAsync();
        }

        public async Task MarkAsSent(int id)
        {
            EmailMessage? messege = await _db.EmailMessages.FirstOrDefaultAsync(e => e.ID == id);

            if (messege != null)
            {
                messege.Sent = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}
