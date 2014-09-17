using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Resources;

namespace PivotApp1
{
    class Ottelukasittely
    {
        
        private List<List<string>> ottelupvkirja = new List<List<string>>();
        private List<Pelaaja> pelaajat = new List<Pelaaja>();
        private List<Ottelurivi> rivit = new List<Ottelurivi>();

        public string tulos { get; set; }
        public string peliaika { get; set; }
        public string koti { get; set; }
        public string vieras { get; set; }
        private string[] ottelueratuloksetkoti;
        private string[] ottelueratuloksetvieras;
        private string[] kotitorjunnat;
        private string[] vierastorjunnat;
        private string viimeisin_maali = "";

        public Ottelukasittely()
        {
            
           // string dingSoundFile = "Sound/sound.mp3";
            ///PivotApp1;component/Sound/sound2.wav
           
        }

        public List<PelaajaObject> getObject()
        {
            List<PelaajaObject> ret = new List<PelaajaObject>();

            foreach (Pelaaja i in pelaajat)
            {
                ret.Add(i.getPelaajaObject());
            }

            if (ret.Count() == 0)
            {
                PelaajaObject o1 = new PelaajaObject();
                o1.joukkue = "Ei tehomerkintöjä";
                o1.nimi = "";
                ret.Add(o1);
            }
            
            //ret.Sort((x, y) => String.Compare(y.pisteet, x.pisteet));

            ret = ret.OrderByDescending(x => Convert.ToInt32(x.pisteet)).ThenByDescending(x => Convert.ToInt32(x.maalit)).ThenByDescending(x => Convert.ToInt32(x.rangaistukset)).ToList();
           

            return ret;
        }

        public void add(string e)
        {
            string[] array = e.Split(';');

            foreach (string i in array)
            {

                List<string> tmp2 = i.Split(':').OfType<string>().ToList();
                ottelupvkirja.Add(tmp2);
            }

            foreach (List<string> i in ottelupvkirja)
            {
                kasittele(i);
            }

            foreach (Pelaaja i in pelaajat)
            {
                //MessageBox.Show(i.getPlr() + " "+i.getMaali()+"m " + i.getSyotto() + "s "+ i.getPisteet()+"p "+ i.getRangaistus()+"min ");
            }
        }

        public List<Pelaaja> getPelaajat()
        {
            return pelaajat;
        }

        public List<List<string>> ret()
        {

            return ottelupvkirja;
        }

        private int lisaaPelaaja(string nmb, string name, string team)
        {
            string plr = team + " " + nmb + " " + name;
            // MessageBox.Show("lisataan pelaaja "+plr);
            int index = 0;
            foreach (Pelaaja i in pelaajat)
            {
                if (i.getPlr() == plr)
                {
                    // MessageBox.Show(i.getPlr() + " vs " + plr);
                    return index;
                }
                ++index;
            }

            pelaajat.Add(new Pelaaja(name, nmb, team));

            return index;
        }

        private void kasittele(List<String> osat)
        {
            int index;
            Ottelurivi rivi = new Ottelurivi();

            if (osat.Count < 2)
                return;
            if (osat[1] == "1")
            {
                // tapahtumatyyppi maali
                /*
                    0 = gameID
						
                    2 = ajassa
                    3 = pelitilanne
                    4 = modet YV,TM,VL, , ,
                    5 = joukkue
                    6 = maalintekija
                    7 = 1. syottaja nro nimi
                    8 = 2. syottaja nro nimi
                    9 = era
                    10 = maalintekijnan nro
                    11 = plussat "11 23 22 21"
                    12 = miinukset "23 22 21 19"
						
						
						
                */
                string[] tmp = osat[4].Split(',');
                string modet = "";
                foreach (string s in tmp)
                {
                    if (s.Trim() != "")
                        modet += s + ' ';
                }



                if (osat[4].IndexOf("RL0") == -1) // oletettavasti karsitaan epäonnistuneet rankkarit
                {

                    rivi.avain = eraText(osat[9]); // erä


                    rivi.aika = osat[2]; //aika
                    rivi.koti = (osat[5] == koti) ? osat[5] : "";
                    rivi.vieras = (osat[5] == koti) ? "" : osat[5];
                    rivi.tilanne = osat[3] + ' ' + modet; // pelitilanne

                    rivi.pelaaja1 = osat[10] + " " + osat[6]; // maalintekija
                    rivi.pelaaja2 = osat[7]; // eka syottaja
                    rivi.pelaaja3 = osat[8]; // toka syottaja
                    rivit.Add(rivi);
                    viimeisin_maali = rivi.aika + rivi.tilanne;

                    index = lisaaPelaaja(osat[10], osat[6], osat[5]);
                    pelaajat[index].lisaaMaali();
                    string[] plr2 = osat[7].Trim().Split(' '); // eka syöttäjä
                    string[] plr3 = osat[8].Trim().Split(' '); // toka syöttäjä


                    if (plr2.Length == 3)
                    {

                        index = lisaaPelaaja(plr2[0], plr2[1] + " " + plr2[2], osat[5]);
                        pelaajat[index].lisaaSyotto(); // lisätään syöttöpiste 1
                        if (plr3.Length == 3)
                        {
                            index = lisaaPelaaja(plr3[0], plr3[1] + " " + plr3[2], osat[5]);
                            pelaajat[index].lisaaSyotto(); // lisätään syöttöpiste 2
                        }
                    }
                }
                else
                {
                    rivi.avain = eraText(osat[9]); // erä


                    rivi.aika = osat[2]; //aika
                    rivi.koti = (osat[5] == koti) ? osat[5] : "";
                    rivi.vieras = (osat[5] == koti) ? "" : osat[5];
                    rivi.tilanne = "RL fail";

                    rivi.pelaaja1 = osat[10] + " " + osat[6]; // maalintekija
                    
                    rivit.Add(rivi);
                    
                }

            }


            else if (osat[1] == "2")
            {
                // rangaistus
                /*
                    0 = gameID
                    1 = tapahtumatyyppi eli 2 for penalty
                    2 = ajassa
                    3 = rangaistuksen pituus
                    4 = joukkue
                    5 = Pelaaja
                    6 = Sijaiskarsija ( nro sukunimi etunimi )
                    7 = syy
                    8 = era-nro
                    9 = pelinro
                    v2_rangaistukset = id, pvm, peliAika, koti, vieras, joukkue, kesto, pelinro, pelaaja, syy
                */

                index = lisaaPelaaja(osat[9], osat[5], osat[4]);
                pelaajat[index].lisaaRangaistus(Convert.ToInt32(osat[3]));

                rivi.avain = eraText(osat[8]); // erä
                rivi.aika = osat[2]; //aika
                rivi.koti = (osat[4] == koti) ? osat[4] : "";
                rivi.vieras = (osat[4] == koti) ? "" : osat[4];
                rivi.tilanne = osat[3] + " min ";

                rivi.pelaaja1 = osat[9] + " " + osat[5] + " - " + osat[7]; // maalintekija
                if (osat[6].Trim() != "")
                {
                    rivi.pelaaja2 = "Kärsijä:";
                    rivi.tyyppi = osat[6]; // syy
                }
                rivit.Add(rivi);

            }
            else if (osat[1] == "3")
            {
                /*
                    pelitulos
                    0 = id
			
                    2 = tulos. esim 4-1 JA
						
						
                */
                tulos = osat[2];
            }
            else if (osat[1] == "4")
            {
                /*
                    pelin kesto.
                    0 = id
			
                    2 = aika
                    3 = kesto sekunteina, esim 3600
						
                */
                //Kello k1 = new Kello(osat[2]);
                peliaika = Kello.time( osat[2] );
                
            }
            else if (osat[1] == "5")
            {
                /*
                    pelin alku
                    0 = id
						
                    2 = eranro
						
                    ainoastaan pelin alussa, sama kuin case 9
						
                */

            }
            else if (osat[1] == "6")
            {
                /*
                    ei kasitystakaan
                    '4355':6:2
						
						
						
                */
            }
            else if (osat[1] == "7")
            {
                /*
                    koti aikalisa
						
						
                    2 = aika
                    3 = joukkue
                    4 = era
						
						
						
                */
                rivi.avain = eraText(osat[4]); // erä
                rivi.aika = osat[2]; //aika
                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];
                rivi.tilanne = "Aikalisä";

                rivit.Add(rivi);

            }
            else if (osat[1] == "8")
            {
                /*
                    vieras aikalisa.
                    0 = id
                    1 = tyyppi eli aikalisa
                    2 = aika
                    3 = joukkue
                    4 = era?
                */
                rivi.avain = eraText(osat[4]); // erä
                rivi.aika = osat[2]; //aika
                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];
                rivi.tilanne = "Aikalisä";

                rivit.Add(rivi);

            }
            else if (osat[1] == "9")
            {
                /*
                    eran alku
                    0 = id
                    1 = tyyppi eli era
                    2 = eranro
						
                    ainoastaan 2. erasta eteenpain
						
                */


            }
            else if (osat[1] == "10")
            {
                /*
                    maalivahti ulos/sisaan
						
                    '4482':10:56.42:Jokerit:JOKELA Patrik:35::MV ulos::3
						
                    2 = peliaika
                    3 = joukkue
                    4 = pelaaja
                    5 = pelinro
                    6 = tapahtuma
                    7 = era
                */

                rivi.avain = eraText(osat[9]); // erä
                rivi.aika = osat[2]; //aika
                //rivi.joukkue = osat[3]; // joukkue
                rivi.tilanne = "MV Sisään";

                rivi.pelaaja1 = osat[5] + " " + osat[4];
                rivi.pelaaja2 = "MV ulos"; // syy
                rivi.tyyppi = osat[6]; // sijaiskarsija
                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];
                rivit.Add(rivi);

            }
            else if (osat[1] == "11")
            {
                /*
                    maalivahti VAIHDOLLA ulos/sisaan
						
                    40:00		Jokipojat		38	KOSUNEN Sami	MV sisaan	35 RATILAINEN Juho MV ulos
						
                    2 = peliaika
                    3 = joukkue
                    4 = pelaaja #1
                    5 = pelinro #1
                    6 = pelinro pelaaja #2
                    7 = tapahtuma #1
                    8 = tapahtuma #2
                    9 = era
					
                */
                rivi.avain = eraText(osat[9]); // erä
                rivi.aika = osat[2]; //aika
                //rivi.joukkue = osat[3]; // joukkue
                rivi.tilanne = "MV sisään";

                rivi.pelaaja1 = osat[5] + " " + osat[4];
                rivi.pelaaja2 = "MV ulos"; // syy
                rivi.tyyppi = osat[6];

                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];

                rivit.Add(rivi);

            }
            else if (osat[1] == "12")
            {
                /*
                    kotijoukkueen aloittava maalivahti
						
                    2 = aika
                    3 = joukkue
                    4 = pelinro
                    5 = pelaaja
                    6 = era?
                */
                

                rivi.avain = eraText(osat[6]); // erä
                rivi.aika = osat[2]; //aika
                //rivi.joukkue = osat[3]; // joukkue
                rivi.tilanne = "MV aloittaa";

                rivi.pelaaja1 = osat[4] + " " + osat[5];

                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];
                rivit.Add(rivi);

            }
            else if (osat[1] == "13")
            {
                /*
                    vierasjoukkueen aloittava maalivahti
						
                    2 = aika
                    3 = joukkue
                    4 = pelinro
                    5 = pelaaja
                    6 = era?
                */
                vieras = osat[3];
                
                rivi.avain = eraText(osat[6]); // erä
                rivi.aika = osat[2]; //aika
                rivi.koti = (osat[3] == koti) ? osat[3] : "";
                rivi.vieras = (osat[3] == koti) ? "" : osat[3];
                
                //rivi.joukkue = osat[3]; // joukkue
                rivi.tilanne = "MV aloittaa";

                rivi.pelaaja1 = osat[4] + " " + osat[5];

                rivit.Add(rivi);
            }
            else if (osat[1] == "14")
            {
                /*
                    kotijoukkueen maalivahti #1 torjunnat
						
                    2 = playerid?
                    3 = pelinro
                    4 = pelaaja
                    5 = peliaika?
                    6 = torjunnat era1, era2, era3, era4
                    7 = joukkue
                    8 = vaihtoaika ja pelitilanne
                */
                koti = osat[7];
            }
            else if (osat[1] == "15")
            {
                /*
                    kotijoukkueen maalivahti #2 torjunnat
						
                    2 = playerid?
                    3 = pelinro
                    4 = pelaaja
                    5 = peliaika?
                    6 = torjunnat era1, era2, era3, era4
                    7 = joukkue
                    8 = ?
                */


            }
            else if (osat[1] == "16")
            {
                /*
                    vierasjoukkueen maalivahti #1 torjunnat
						
                    2 = playerid?
                    3 = pelinro
                    4 = pelaaja
                    5 = peliaika?
                    6 = torjunnat era1, era2, era3, era4
                    7 = joukkue
                    8 = ?
                */
                vieras = osat[7];
            }
            else if (osat[1] == "17")
            {
                /*
                    vierasjoukkueen maalivahti #2 torjunnat
						
                    2 = playerid?
                    3 = pelinro
                    4 = pelaaja
                    5 = peliaika?
                    6 = torjunnat era1, era2, era3, era4
                    7 = joukkue
                    8 = ?
                */

            }
            else if (osat[1] == "18")
            {
                /*
                    '4393':18:1,2,3,4:2,0,1,1:2,1,0,0:2,2,3,4:2,3,3,3:5,5,5,0:10,11,16,3:2,2,2,:4,4,6,:02.08,04.00,03.37,00.00:02.00,02.00,02.00,00.00:0,0,1,1:0,0,0,0:0,0,0,0:0,0,0,0:09.45:06.00
                    0 = id
						
                    2 = 60,0,3,20,0,0,0,0,0,1,5,0,1,0
						
					
						
                */
                //string[] tmp = osat[2].Split(':');
                ottelueratuloksetkoti = osat[3].Split(',');
                ottelueratuloksetvieras = osat[4].Split(',');

                kotitorjunnat = osat[7].Split(',');
                vierastorjunnat = osat[8].Split(',');

            }
            else if (osat[1] == "19")
            {
                /*
                    laukaukset?
                    0 = id
						
                    2 = 60,0,3,20,0,0,0,0,0,1,5,0,1,0
						
					
						
                */

            }

            

        }

        public void olikoUusiMaali()
        {
            
            if (viimeisin_maali != App.viimeisin_maali && !App.otteluPaattynyt && App.viimeisin_maali != null)
            {
                App.AlarmSound();
                App.vibrate(2);
                App.viimeisin_maali = viimeisin_maali;
                //MessageBox.Show( "Asetettiin "+App.viimeisin_maali );
                App.save();
            }
            else if( !App.otteluPaattynyt )
            {
                //App.vibrate(2);
                App.viimeisin_maali = viimeisin_maali;
                //MessageBox.Show("Else Asetettiin " + App.viimeisin_maali);
                App.save();
            }
            //App.vibrate(1);
        }

        public List<Ottelurivi> palautaRivit()
        {
            if (rivit.Count() == 0)
            {
                Ottelurivi o1 = new Ottelurivi();
                o1.avain = "Ottelu ei ole alkanut";
                o1.pelaaja1 = "";
                rivit.Add(o1);
            }

            rivit = rivit.OrderByDescending(x => x.aika).ToList();
            olikoUusiMaali();
            return rivit;
        }

        public event EventHandler Tapahtuma;

        public string eraText(string i)
        {
            if (i == "99")
                i = "Voittomaalikilpailu";
            else if (i == "1")
                i = "1. erä";
            else if (i == "2")
                i = "2. erä";
            else if (i == "3")
                i = "3. erä";
            else if (i == "4")
                i = "Jatkoaika";

            return i;
        }

        public string EraTulokset()
        {
            string ret = "( ";
            for (int i = 0; i < ottelueratuloksetkoti.Length; ++i)
            {
                ret += ottelueratuloksetkoti[i] + "-" + ottelueratuloksetvieras[i];
                if (i < ottelueratuloksetkoti.Length - 1)
                    ret += ", ";
            }
           
            ret += " )";

            if (ret.Length < 6)
                return "";
            return ret;
        }

        public string torjunnat()
        {
            string ret = "( ";
            int kotimax = 0;
            int vierasmax = 0;
            for (int i = 0; i < kotitorjunnat.Length; ++i)
            {
                if (i > 0 && kotitorjunnat[i].Trim() != "")
                    ret += ", ";

                if (kotitorjunnat[i].Trim() != "")
                {
                    ret += kotitorjunnat[i] + "-" + vierastorjunnat[i];
                    kotimax += Convert.ToInt32(kotitorjunnat[i]);
                    vierasmax += Convert.ToInt32(vierastorjunnat[i]);
                }

                
            }

            ret += " )";
            if (Tapahtuma != null)
            {
                Tapahtuma(this, EventArgs.Empty);
                
            }
            if (ret.Length < 5) 
                return "";
            
            return "Torjunnat: "+kotimax.ToString()+"-"+vierasmax.ToString()+"    "+ret;
        }
    }

 
}
