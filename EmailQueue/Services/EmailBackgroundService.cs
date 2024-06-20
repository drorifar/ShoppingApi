
using EmailQueue.Repository;

namespace EmailQueue.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private IServiceProvider _provider;

        public EmailBackgroundService(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                IMailService mail = scope.ServiceProvider.GetRequiredService<IMailService>(); //get the mailServie manually because we cant get it here with dependency injection
                IEmailMessageRepository repo = scope.ServiceProvider.GetRequiredService<IEmailMessageRepository>(); //get the IEmailMessageRepository manually because we cant get it here with dependency injection

                while (!stoppingToken.IsCancellationRequested)
                {
                    IEnumerable<Entities.EmailMessage> emailMessages = await repo.GetPendingMessages(10);

                    foreach (var message in emailMessages)
                    {
                        try
                        {
                            await mail.SendEmail(message);
                        }
                        catch (Exception)
                        {
                         
                        }
                        await repo.MarkAsSent(message.ID);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }
    }
}
