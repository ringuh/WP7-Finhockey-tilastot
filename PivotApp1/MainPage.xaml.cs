using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Resources;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Windows.Navigation;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        DispatcherTimer newTimer = null;
        DispatcherTimer paivanOttelutTimer = null;


        public MainPage()
        {

            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //FirstListBox.DataContext = App.PaivanOttelut;
            //SarjataulukkoBox.DataContext = App.SarjataulukkoMod;


        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            this.OtteluGridList.ItemsSource = App.LadataanDataa();
            this.SarjataulukkoList.ItemsSource = App.LadataanDataa();
            this.PaivanOttelutList.ItemsSource = App.LadataanDataa();
            this.TehopisteetList.ItemsSource = App.LadataanDataa();
            this.TilastotList.ItemsSource = App.LadataanDataa();
            this.VanhatOttelutList.ItemsSource = App.LadataanDataa();



            HaePaivanJson();
            refresh_sarja();
            refresh();

            paivitaPaivanOttelutAjastin();

        }




        public void refresh_sarja()
        {

            haeSarjataulukko();
            haeTilastot();
            HaeLohkonOttelut();
        }

        public void refresh()
        {
            paivitaOttelu();
        }

        public void HaePaivanJson()
        {
            WebClient webClient = new WebClient();
            Random random = new Random();
            string url = "http://home.tamk.fi/~e2avaaka/kartta/paivan_ottelut.php?rnd=" + random.Next(100000);
            Uri uri = new Uri(url);

            webClient.DownloadStringAsync(uri);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {


            RootObject ottelut = (RootObject)JsonConvert.DeserializeObject<RootObject>(e.Result);
            List<Game> karsitut_ottelut = new List<Game>();
            foreach (Game i in ottelut.games)
            {
                i.GameTime = i.GameTime.Substring(0, i.GameTime.Length - 3);

                int j = 0;

                bool result = int.TryParse(i.GameEffTime, out j);

                if (result)
                    i.GameEffTime = Kello.time(i.GameEffTime);

                if (naytetaankoOttelu(i.StatGroupName))
                    karsitut_ottelut.Add(i);
            }

            if (App.naytaKaikki)
            {
                karsitut_ottelut = ottelut.games;
            }
            if (karsitut_ottelut.Count() == 0)
            {
                Game g1 = new Game();
                g1.LevelName = "Näytettävissä sarjoissa ei otteluita";
                karsitut_ottelut.Add(g1);
            }

            var otteluByLohko = from cottelu in karsitut_ottelut
                                group cottelu by cottelu.LevelName into c
                                orderby c.Key
                                select new Group<Game>(c.Key, c);


            this.PaivanOttelutList.ItemsSource = null;
            this.PaivanOttelutList.ItemsSource = otteluByLohko;

        }

        public void HaeLohkonOttelut()
        {
            WebClient webClient = new WebClient();
            Random random = new Random();
            string url = "http://home.tamk.fi/~e2avaaka/tilastot/servu/sql_haku.php?cmd=ottelut&sarja=" + App.Lohko_ + "&rnd=" + random.Next(100000);
            Uri uri = new Uri(url);

            webClient.DownloadStringAsync(uri);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(lohkonOttelut_DownloadStringCompleted);
        }

        void lohkonOttelut_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {


            string jsoniksi = "{\"ottelut\":" + e.Result + '}';

            RootOttelu ottelut = (RootOttelu)JsonConvert.DeserializeObject<RootOttelu>(jsoniksi);

            foreach (Ottelu i in ottelut.ottelut)
            {
                i.GameTime = i.GameTime.Substring(0, i.GameTime.Length - 3);

                int j = 0;

                bool result = int.TryParse(i.GameEffTime, out j);

                if (result)
                {

                    i.GameEffTime = Kello.time(i.GameEffTime);
                }
            }

            var otteluByLohko = from ottelu in ottelut.ottelut
                                group ottelu by ottelu.pelattu into c
                                orderby c.Key descending
                                select new Group<Ottelu>(c.Key, c);

            this.VanhatOttelutList.ItemsSource = null;
            this.VanhatOttelutList.ItemsSource = otteluByLohko;



        }

        public void paivitaOttelu()
        {

            WebClient webClient = new WebClient();
            Random random = new Random();
            string url = "http://tilastopalvelu.fi/ih/modules/mod_gamereport/helper/actions.php?gameid=" + App.GameId_ + "&rnd=" + random.Next(100000);

            if (App.simulaatio)
                url = "http://home.tamk.fi/~e2avaaka/kartta/simulaatio.php?nro=" + App.simu_rivi + "&rnd=" + random.Next(100000); ;

            Uri uri = new Uri(url);
            webClient.DownloadStringAsync(uri);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ottelu_DownloadStringCompleted);
        }

        public void ottelu_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            if (App.simulaatio)
            {
                ++App.simu_rivi;

                if (App.simu_rivi > 99)
                {
                    lopetaSimu();
                }
            }
            List<List<string>> ottelutapahtumat = new List<List<string>>();


            Ottelukasittely o1 = new Ottelukasittely();
            o1.add(e.Result);

            List<PelaajaObject> plrObj = o1.getObject();

            var plrByTeam = from playa in plrObj
                            group playa by playa.joukkue into c
                            orderby c.Key
                            select new Group<PelaajaObject>(c.Key, c);


            this.TehopisteetList.ItemsSource = null;
            this.TehopisteetList.ItemsSource = plrByTeam;

            o1.Tapahtuma += new System.EventHandler(this.teeJotain);


            this.PeliTulos.Text = o1.tulos;
            this.PeliAika.Text = o1.peliaika;
            this.KotiJoukkue.Text = o1.koti;
            this.VierasJoukkue.Text = o1.vieras;
            this.OtteluEratulokset.Text = o1.EraTulokset();
            this.OtteluTorjunnat.Text = o1.torjunnat();




            var riviByera = from rivi in o1.palautaRivit()
                            group rivi by rivi.avain into c
                            orderby c.Key descending
                            select new Group<Ottelurivi>(c.Key, c);

            this.OtteluGridList.ItemsSource = null;
            this.OtteluGridList.ItemsSource = riviByera;

            paivitaAjastin();

        }



        public void teeJotain(object sender, EventArgs e)
        {
            //this.PeliTulos.Text = "IHQ";
        }

        public void haeSarjataulukko()
        {
            WebClient webClient = new WebClient();
            // Luodaan Uri -objekti
            string url = "http://home.tamk.fi/~e2avaaka/tilastot/servu/sql_haku.php?cmd=sarjataulukko&order=pisteet&desc=desc&sarja=" + App.Lohko_;
            Uri uri = new Uri(url);
            // Lähetetään web request (pyydetään säätietoa)
            webClient.DownloadStringAsync(uri);
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(sarjataulukko_DownloadStringCompleted);
        }

        public void haeTilastot()
        {
            WebClient webClient = new WebClient();
            // Luodaan Uri -objekti
            string url = "http://home.tamk.fi/~e2avaaka/tilastot/servu/sql_haku.php?cmd=pisteet&order=pisteet&desc=desc&sarja=" + App.Lohko_;
            Uri uri = new Uri(url);
            // Lähetetään web request (pyydetään säätietoa)
            webClient.DownloadStringAsync(uri);
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tilastot_DownloadStringCompleted);
        }

        void tilastot_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            string jsoniksi = "{\"tilastot\":" + e.Result + '}';
            TilastotRootObject st = (TilastotRootObject)JsonConvert.DeserializeObject<TilastotRootObject>(jsoniksi);

            int i = 1;
            foreach (var plr in st.tilastot)
            {
                plr.rank = i.ToString();
                ++i;
            }

            var pelaajaByLohko = from pelaaja in st.tilastot
                                 group pelaaja by pelaaja.lohko into c
                                 orderby c.Key
                                 select new Group<Tilastot>(c.Key, c);

            this.TilastotList.ItemsSource = null;

            this.TilastotList.ItemsSource = pelaajaByLohko;




        }

        void sarjataulukko_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {


            string jsoniksi = "{\"sarjataulukko\":" + e.Result + '}';
            SarjaRootObject st = (SarjaRootObject)JsonConvert.DeserializeObject<SarjaRootObject>(jsoniksi);

            int i = 1;
            foreach (var plr in st.sarjataulukko)
            {
                plr.rank = i.ToString();

                ++i;
            }

            var joukkueByLohko = from joukkue in st.sarjataulukko
                                 group joukkue by joukkue.lohko into c
                                 orderby c.Key
                                 select new Group<Sarjataulukko>(c.Key, c);

            this.SarjataulukkoList.ItemsSource = null;
            this.SarjataulukkoList.ItemsSource = joukkueByLohko;

        }

        private void PaivanOttelut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var geim = ((LongListSelector)sender).SelectedItem as Game;

            if (geim != null)
            {
                lopetaSimu();

                this.OtteluGridList.ItemsSource = App.LadataanDataa();

                App.Lohko_ = geim.StatGroupName;
                App.GameId_ = geim.UniqueID;
                this.KotiJoukkue.Text = geim.HomeTeamName;
                this.VierasJoukkue.Text = geim.AwayTeamName;

                if (geim.FinishedType != "2" && geim.FinishedType != "94")
                    App.otteluPaattynyt = false;



                App.viimeisin_maali = null;

                App.save();

                refresh_sarja();
                refresh();
                MainPivot.SelectedIndex = 5;
            }

        }

        private void VanhatOttelut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var geim = ((LongListSelector)sender).SelectedItem as Ottelu;

            if (geim != null)
            {
                lopetaSimu();

                this.OtteluGridList.ItemsSource = App.LadataanDataa();

                App.Lohko_ = geim.lohko;
                App.GameId_ = geim.id;
                this.KotiJoukkue.Text = geim.koti;
                this.VierasJoukkue.Text = geim.vieras;

                if (geim.FinishedType != "2" && geim.FinishedType != "94")
                    App.otteluPaattynyt = false;



                App.viimeisin_maali = null;

                App.save();

                refresh_sarja();
                refresh();
                MainPivot.SelectedIndex = 5;
            }

        }

        private void paivitaAjastin()
        {
            if (newTimer != null)
            {
                if (App.otteluPaattynyt && newTimer.IsEnabled)
                {
                    newTimer.Stop();
                    newTimer = null;
                }
            }

            if (newTimer == null)
            {
                newTimer = new DispatcherTimer();
                newTimer.Interval = TimeSpan.FromSeconds(App.otteluRefTime);
                newTimer.Tick += newTimer_Tick;

                if (!App.otteluPaattynyt)
                    newTimer.Start();
            }

        }

        void newTimer_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("timer");

            refresh();
        }

        private void paivitaPaivanOttelutAjastin()
        {
            if (paivanOttelutTimer == null)
            {
                paivanOttelutTimer = new DispatcherTimer();

                paivanOttelutTimer.Interval = TimeSpan.FromMinutes(App.PaivanOttelutRefTime);

                paivanOttelutTimer.Tick += paivanOttelutTimer_Tick;

                paivanOttelutTimer.Start();
            }
        }

        private void paivanOttelutTimer_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("minuutti");

            HaePaivanJson();
        }



        private void BackToFront(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void VahdaLohkoa(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Assets/AppBar/LohkoValitsin.xaml", UriKind.Relative));
        }

        private void GoToAsetukset(object sender, System.EventArgs e)
        {

            NavigationService.Navigate(new Uri("/Assets/AppBar/Asetukset.xaml", UriKind.Relative));

        }

        private void spinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lohko = ((ListPicker)sender).SelectedItem as Lohko;
            App.Lohko_ = lohko.lohko;

            refresh_sarja();

        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            newTimer.Stop();
            newTimer = null;
            paivanOttelutTimer.Stop();
            paivanOttelutTimer = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //newTimer.Tick -= newTimer_Tick;
            App.viimeisin_maali = null;

            base.OnNavigatedTo(e);

            paivitaAjastin();
            paivitaPaivanOttelutAjastin();

            if (NavigationContext.QueryString.ContainsKey("index"))
            {
                var index = NavigationContext.QueryString["index"];
                var indexParsed = int.Parse(index);
                MainPivot.SelectedIndex = indexParsed;
            }

        }

        private void CurrentPivot(object sender, SelectionChangedEventArgs e)
        {
            App.lastPivot = MainPivot.SelectedIndex;
        }

        private void lopetaSimu()
        {


            App.simu_rivi = 1;
            App.simulaatio = false;
            App.otteluPaattynyt = true;
            App.viimeisin_maali = null;

            newTimer.Stop();
            newTimer = null;

        }

        private bool naytetaankoOttelu(string i)
        {


            foreach (Lohko j in App.lohkoKarsinta())
            {
                if (i == j.lohko)
                    return true;
            }

            return false;
        }


    }
}