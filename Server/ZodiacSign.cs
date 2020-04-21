using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ZodiacSign
    {
        public ZodiacSign(string name, string startMonth, string startDay,
            string stopMonth, string stopDay)
        {
            this.name = name;
            this.startMonth = startMonth;
            this.startDay = startDay;
            this.finishMonth = stopMonth;
            this.finishDay = stopDay;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string StartMonth
        {
            get
            {
                return startMonth;
            }
        }

        public string StartDay
        {
            get
            {
                return startDay;
            }
        }

        public string FinishMonth
        {
            get
            {
                return finishMonth;
            }
        }

        public string FinishDay
        {
            get
            {
                return finishDay;
            }
        }

        private string name;
        private string startMonth;
        private string startDay;
        private string finishMonth;
        private string finishDay;
    }
}
