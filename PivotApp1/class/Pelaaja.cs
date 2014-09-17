using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotApp1
{

    class Pelaaja
    {
        private string nimi;
        private string joukkue;
        private string nro;
        private int syotot = 0;
        private int maalit = 0;
        private int rangaistukset = 0;

        public Pelaaja(string name, string nmb, string team)
        {
            nimi = name;
            nro = nmb;
            joukkue = team;

        }

        public void lisaaSyotto()
        {
            ++syotot;
        }

        public int getSyotto()
        {
            return syotot;
        }

        public void lisaaRangaistus(int x)
        {
            rangaistukset += x;
        }

        public int getRangaistus()
        {
            return rangaistukset;
        }

        public void lisaaMaali()
        {
            ++maalit;
        }

        public int getMaali()
        {
            return maalit;
        }

        public int getPisteet()
        {
            return maalit + syotot;
        }

        public string getPlr()
        {
            return joukkue + " " + nro + " " + nimi;
        }

        public PelaajaObject getPelaajaObject()
        {
            PelaajaObject p1 = new PelaajaObject();
            p1.joukkue = joukkue;
            p1.nimi = nimi;
            p1.nro = nro;
            p1.maalit = maalit.ToString();
            p1.syotot = syotot.ToString();
            p1.pisteet = getPisteet().ToString();
            p1.rangaistukset = rangaistukset.ToString();

            return p1;
        }
    }

    public class PelaajaObject
    {
        public string joukkue { get; set; }
        public string nimi { get; set; }
        public string nro { get; set; }
        public string maalit { get; set; }
        public string syotot { get; set; }
        public string pisteet { get; set; }
        public string rangaistukset { get; set; }
    }
}
