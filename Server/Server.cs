using System;
using System.Collections.Generic;
using System.Linq;
using Generated;
using Grpc.Core;
using System.Threading.Tasks;
using System.Globalization;

namespace Server
{

    class Server : IDisposable
    {
        public static List<Zodiac> listZodiac = new List<Zodiac>();

        public Grpc.Core.Server GrpcServer { get; private set; }

        public Action CloseServerAction { get; set; }


        public IEnumerable<Grpc.Core.ServerServiceDefinition> Services
        {
            get
            {
                yield return Generated.ZodiacService.BindService(new ZodiacService());
            }
        }

        public Server(string host, int port)
        {
            GrpcServer = new Grpc.Core.Server()
            {
                Ports = { new Grpc.Core.ServerPort(host, port, Grpc.Core.ServerCredentials.Insecure) }
            };

            LoadServices();
        }

        public void Start()
        {
            GrpcServer.Start();

            Console.WriteLine(string.Format("Server started ({0}:{1}).", Configuration.HOST, Configuration.PORT));

            System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\Zodiac.txt");

            string line;
           
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(' ','.','-');
                listZodiac.Add(new Zodiac(words[0], words[1], words[2], words[3], words[4]));
            }
            file.Close();

        }

        private void LoadServices()
        {
            Services.ToList().ForEach(service => GrpcServer.Services.Add(service));
        }

        public void Dispose()
        {
            CloseServerAction.Invoke();
            GrpcServer.ShutdownAsync().Wait();
            var port = GrpcServer.Ports.FirstOrDefault();
            Console.WriteLine("Server closed ({0}:{1}).", Configuration.HOST, Configuration.PORT);
        }
    }
}
