namespace Shopping.services
{
    public class LocalMailService: IMailService
    {
        private string from;
        private string to;

        private IConfiguration _config;

        public LocalMailService(IConfiguration config)
        {
                _config = config ?? throw new ArgumentNullException(nameof(config));

            from = _config["mailSetting:fromAddress"];
            to = _config["mailSetting:toAddress"];
        }

        public void send(string subject, string body)
        {
            Console.WriteLine($"Mail from:{from} to:{to} - using local");
            Console.WriteLine(subject);
            Console.WriteLine("-------------------------");
            Console.WriteLine(body);
        }
    }
}
