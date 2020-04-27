using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ZodiacService : Generated.ZodiacService.ZodiacServiceBase
    {
        private string testDate(string date)
        {
            string formats = "MM/dd/yyyy";

            DateTime dateUsed = DateTime.ParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

            int sizeOfList = Server.listZodiac.Count;
            for (int i = 0; i < sizeOfList; ++i)
            {
                if (dateUsed.Month == Int32.Parse(Server.listZodiac[i].StartMonth)
                    && dateUsed.Day >= Int32.Parse(Server.listZodiac[i].StartDay)
                    || dateUsed.Month == Int32.Parse(Server.listZodiac[i].FinishMonth)
                    && dateUsed.Day <= Int32.Parse(Server.listZodiac[i].FinishDay))
                {
                    return Server.listZodiac[i].Name;
                }
            }

            return String.Empty;
        }

        public override Task<ZodiacResponse> GetSigns(Generated.DateRequest request, ServerCallContext context)
        {
            Console.WriteLine(request.Date);
            var response = new ZodiacResponse();
            return Task.FromResult(new ZodiacResponse() { Response = testDate(request.Date) });
        }
    }
}
