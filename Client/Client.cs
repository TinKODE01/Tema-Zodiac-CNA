using Grpc.Core;
using System;
using System.Globalization;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            const string Host = "localhost";
            const int Port = 2020;

            var channel = new Channel($"{Host}:{Port}", ChannelCredentials.Insecure);
            var client = new Generated.ZodiacService.ZodiacServiceClient(channel);
            Console.Write("Date: ");
            var datdateRead = Console.ReadLine();
            DateTime date;
            try
            {
                string formats = "MM/dd/yyyy";
                date = DateTime.ParseExact(datdateRead, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                var response = client.GetSigns(new Generated.DateRequest
                {
                    Date = datdateRead
                });
                Console.WriteLine(response.Response);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid date.");
                //Console.WriteLine(e.Message);
            }

            // Shutdown
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
