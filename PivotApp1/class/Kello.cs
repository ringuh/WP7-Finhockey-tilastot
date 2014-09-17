using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotApp1
{
    class Kello
    {
        public Kello()
        {
            

        }

        public static string time(string sek)
        {
            long sekunnit = 0;
            if (sek.Trim() == "")
            {
                mm = 0;
                ss = 0;
            }
            else if (long.TryParse(sek, out sekunnit))
            {
                sekunnit = Convert.ToInt64(sek);
                mm = sekunnit / 60;

                ss = sekunnit - (mm * 60);
            }

            string _mm = mm.ToString();
            string _ss = ss.ToString();

            if (mm < 10) { _mm = "0" + mm; }

            if (ss < 10) { _ss = "0" + ss; }


            return _mm + ":" + _ss;
        }
        private static long mm;
        private static long ss;
    }
}
