using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotApp1
{
    public class SarjaRootObject
    {
        public List<Sarjataulukko> sarjataulukko { get; set; }
    }

    public class Game
    {
        public string LevelName { get; set; }
        public string StatGroupName { get; set; }
        public string UniqueID { get; set; }
        public string GameDate { get; set; }
        public string GameTime { get; set; }
        public string HomeTeamID { get; set; }
        public string AwayTeamID { get; set; }
        public string RinkID { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string RinkName { get; set; }
        public string Result { get; set; }
        public string FinishedType { get; set; }
        public string GameStatus { get; set; }
        public string GameEffTime { get; set; }
        public string ReportLink { get; set; }
        public string Statistics { get; set; }
    }

    public class RootOttelu
    {
        public List<Ottelu> ottelut { get; set; }
    }

    public class Ottelu
    {
        public string lohko { get; set; }
        public string taso { get; set; }
        public string id { get; set; }
        public string pvm { get; set; }
        public string aloitusAika { get; set; }
        public string paikka { get; set; }
        public string koti { get; set; }
        public string vieras { get; set; }
        public string tulos { get; set; }
        public string tyyppi { get; set; }
        public string peliStatus { get; set; }
        public string peliKesto { get; set; }
        public string livearena { get; set; }
        public string ottelupvkirja { get; set; }
        public string ottelutapahtumat { get; set; }
        public string md5 { get; set; }

        public string pelattu
        {
            get
            {
                if (peliStatus == "2" || peliStatus == "94")
                    return "Pelatut";
                else
                    return "Pelaamatta";
            }
            set { }
        }

        public string HomeTeamName
        {
            get
            {
                return koti;
            }
            set
            {

            }
        }

        public string AwayTeamName
        {
            get
            {
                return vieras;
            }
            set
            {

            }
        }

        public string Result
        {
            get
            {
                return tulos;
            }
            set
            {

            }
        }

        public string GameTime
        {
            get
            {
                if (pvm == null)
                    return "";

                string[] arr = pvm.Split('-');
                if (arr.Count() == 3)
                    return arr[2] + "." + arr[1] + "." + arr[0];
                else
                    return pvm;
            }
            set
            {

            }
        }

        public string GameEffTime
        {
            get
            {
                return "";
            }
            set
            {

            }
        }

        public string FinishedType
        {
            get
            {
                return peliStatus;
            }
            set
            {

            }
        }
        public string UniqueID
        {
            get
            {
                return id;
            }
            set
            {

            }
        }
        public string StatGroupName
        {
            get
            {
                return lohko;
            }
            set
            {

            }
        }

    }

    public class RootObject
    {
        public List<Game> games { get; set; }
    }


    public class Sarjataulukko
    {

        public string rank { get; set; }
        public string taso { get; set; }
        public string lohko { get; set; }
        public string joukkue { get; set; }
        public string ottelut { get; set; }
        public string voitot { get; set; }
        public string jatkoVoitot { get; set; }
        public string jatkoTappiot { get; set; }
        public string tappiot { get; set; }
        public string TM { get; set; }
        public string PM { get; set; }
        public string pisteet { get; set; }
        public string aikalisat { get; set; }
        public string YVm { get; set; }
        public string AVm { get; set; }
        public string YVt { get; set; }
        public string AVt { get; set; }
        public string AVmo { get; set; }
        public string MVvaihdot { get; set; }
        public string jaahyt { get; set; }
        public string ep_link { get; set; }
        public string ME { get; set; }
        public string yv_pros { get; set; }
        public string av_pros { get; set; }
    }

    public class Tilastot
    {
        public string rank { get; set; }
        public string lohko { get; set; }
        public string taso { get; set; }
        public string joukkue { get; set; }
        public string nro { get; set; }
        public string nimi { get; set; }
        public string ottelut { get; set; }
        public string maalit { get; set; }
        public string syotot { get; set; }
        public string pisteet { get; set; }
        public string rangaistukset { get; set; }
        public string VoM { get; set; }
        public string AV { get; set; }
        public string YV { get; set; }
        public string TM { get; set; }
        public string syntymaaika { get; set; }
        public string pituus { get; set; }
        public string paino { get; set; }
        public string pelipaikka { get; set; }
        public string torjunnat { get; set; }
        public string PM { get; set; }
        public string peliaika { get; set; }
        public string ppg { get; set; }
        public string torjuntaprosentti { get; set; }
        public string plusmiinus { get; set; }
        public string ep_link { get; set; }
    }

    public class TilastotRootObject
    {
        public List<Tilastot> tilastot { get; set; }
    }

    public class Ottelurivi
    {
        public string avain { get; set; }
        public string aika { get; set; }
        public string koti { get; set; }
        public string vieras { get; set; }
        public string tilanne { get; set; }
        public string tyyppi { get; set; }
        public string pelaaja1 { get; set; }
        public string pelaaja2 { get; set; }
        public string pelaaja3 { get; set; }
        public string alkamisaika { get; set; }
        public string torjunnat { get; set; }
        public string ottelutulos { get; set; }
        public string kotiteam { get; set; }
        public string vierasteam { get; set; }
        public string eratulokset { get; set; }
    }

    public class LohkotRootObject
    {
        public List<Lohko> lohkot { get; set; }
    }

    public class Lohko
    {
        public string taso { get; set; }
        public string lohko { get; set; }
        private bool _isChecked = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;

                }
            }
        }

        public string itaso
        {
            get
            {
                return sarjanimi_case(taso);
            }
            set
            {

            }
        }

        public string md5 { get; set; }
        public string ilohko
        {
            get
            {
                return taso + " - " + lohko;
            }
            set
            {
            }
        }
        private string sarjanimi_case(string nro)
        {

            string ret = "";
            switch (Convert.ToInt32(nro))
            {
                case 21:
                    ret = "B-nuorten Aluesarja";
                    break;


                case 19:
                    ret = "B-nuorten Mestis";
                    break;


                case 18:
                    ret = "B-nuorten SM-sarja";
                    break;


                case 20:
                    ret = "B-nuorten Suomi-Sarja";
                    break;


                case 53:
                    ret = "B-tyttöjen aluesarja";
                    break;


                case 23:
                    ret = "B2-nuorten Mestis";
                    break;


                case 22:
                    ret = "B2-nuorten SM-sarja";
                    break;


                case 24:
                    ret = "B2-nuorten Suomi-Sarja";
                    break;


                case 28:
                    ret = "C-nuorten Aluesarja";
                    break;


                case 26:
                    ret = "C-nuorten Mestis";
                    break;


                case 25:
                    ret = "C-nuorten SM-sarja";
                    break;


                case 27:
                    ret = "C-nuorten Suomi-Sarja";
                    break;


                case 55:
                    ret = "C-tyttöjen aluesarja";
                    break;


                case 32:
                    ret = "C2 A";
                    break;


                case 31:
                    ret = "C2 AA";
                    break;


                case 30:
                    ret = "C2 AAA";
                    break;


                case 29:
                    ret = "C2 nuorten valtakunnallinen sarja";
                    break;


                case 56:
                    ret = "D-tytöt";
                    break;


                case 36:
                    ret = "D1 A";
                    break;


                case 35:
                    ret = "D1 AA";
                    break;


                case 34:
                    ret = "D1 AAA";
                    break;


                case 33:
                    ret = "D1 valtakunnallinen toiminta";
                    break;


                case 40:
                    ret = "D2 A";
                    break;


                case 39:
                    ret = "D2 AA";
                    break;


                case 38:
                    ret = "D2 AAA";
                    break;


                case 37:
                    ret = "D2 valtakunnallinen toiminta";
                    break;


                case 57:
                    ret = "E-tytöt";
                    break;


                case 43:
                    ret = "E1 A";
                    break;


                case 42:
                    ret = "E1 AA";
                    break;


                case 41:
                    ret = "E1 AAA";
                    break;


                case 46:
                    ret = "E2 A";
                    break;


                case 45:
                    ret = "E2 AA";
                    break;


                case 44:
                    ret = "E2 AAA";
                    break;


                case 58:
                    ret = "F-tytöt";
                    break;


                case 47:
                    ret = "F1 Leijonaliiga";
                    break;


                case 48:
                    ret = "F2 Leijonaliiga";
                    break;


                case 49:
                    ret = "G Leijonaliiga";
                    break;


                case 8:
                    ret = "Harrastesarjat";
                    break;


                case 4:
                    ret = "II-divisioona";
                    break;


                case 5:
                    ret = "III-divisioona";
                    break;


                case 6:
                    ret = "IV-divisioona";
                    break;


                case 2:
                    ret = "Mestis";
                    break;


                case 13:
                    ret = "Naisten aluesarjat";
                    break;


                case 11:
                    ret = "Naisten Mestis";
                    break;


                case 10:
                    ret = "Naisten SM-sarja";
                    break;


                case 12:
                    ret = "Naisten Suomi-Sarja";
                    break;


                case 17:
                    ret = "Nuorten Aluesarja";
                    break;


                case 15:
                    ret = "Nuorten Mestis";
                    break;


                case 14:
                    ret = "Nuorten SM-Liiga";
                    break;


                case 16:
                    ret = "Nuorten Suomi-Sarja";
                    break;


                case 9:
                    ret = "Seniorisarjat";
                    break;


                case 3:
                    ret = "Suomi-Sarja";
                    break;


                case 7:
                    ret = "V-divisioona";
                    break;
            }

            return ret;
        }
    }


}
