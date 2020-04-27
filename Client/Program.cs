using Grpc.Core;
using System;
using System.Globalization;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Host = "localhost";
            const int Port = 2020;

            var channel = new Channel($"{Host}:{Port}", ChannelCredentials.Insecure);
            var client = new Generated.ZodiacService.ZodiacServiceClient(channel);
            Console.Write("Date of Birth: ");
            var dateOfBirthRead = Console.ReadLine();
            DateTime date;
            try
            {
                string formats = "MM/dd/yyyy";
                date = DateTime.ParseExact(dateOfBirthRead, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                var response = client.GetSigns(new Generated.DateRequest
                {
                    Date = dateOfBirthRead
                });
                Console.WriteLine(response.Response);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Date of Birth!");
            }

            // Shutdown
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
