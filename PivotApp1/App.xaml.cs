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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Devices;
using System.IO.IsolatedStorage;
using System.Windows.Threading;

namespace PivotApp1
{
    public partial class App : Application
    {

        
        
        public static bool simulaatio = false;
        public static int simu_rivi = 1;

        public static int otteluRefTime = 15;
        public static int PaivanOttelutRefTime = 1;

        public static bool otteluPaattynyt = true;
        public static bool varina = true;
        public static bool alarmNoice = false;
        public static bool lockScreen = false;
        public static bool naytaKaikki = true;

        public static string Lohko_ = "B-nuorten SM-sarja";
        public static string GameId_ = "4456";
        public static bool tallennettu = false;
        public static List<Ottelurivi> talteen = new List<Ottelurivi>();
        public static string viimeisin_maali = null;
        public static SoundEffectInstance soundInstance;
        public static List<Lohko> lohkolista = new List<Lohko>();
        public static IsolatedStorageSettings varasto = IsolatedStorageSettings.ApplicationSettings;
        public static int lastPivot = 0;

        public static IEnumerable<Group<Ottelu>> LadataanDataa()
        {
            Ottelu o1 = new Ottelu { lohko = "Ladataan dataa" };
            List<Ottelu> tmp = new List<Ottelu>();
            tmp.Add(o1);

            IEnumerable<Group<Ottelu>> ladataanDataa = from ottelu in tmp
                                group ottelu by ottelu.lohko into c
                                orderby c.Key descending
                                select new Group<Ottelu>(c.Key, c);

            return ladataanDataa;
        }


        public static List<Lohko> lohkoKarsinta()
        {
            List<Lohko> karsittu_lohko = new List<Lohko>();

            foreach (Lohko i in App.lohkolista)
            {
                if (i.IsChecked)
                    karsittu_lohko.Add(i);
            }

            return karsittu_lohko;
        }

        public static void AlarmSound()
        {
            if (alarmNoice)
            {
                StreamResourceInfo _stream = Application.GetResourceStream(new Uri("PivotApp1;component/Sound/miley.wav", UriKind.Relative));
                SoundEffect _soundeffect = SoundEffect.FromStream(_stream.Stream);
                App.soundInstance = _soundeffect.CreateInstance();
                FrameworkDispatcher.Update();
                App.soundInstance.Play();
            }
        }

        public static void vibrate(double sek)
        {
            if (varina)
            {
                VibrateController testVibrateController = VibrateController.Default;
                if (sek > 0)
                    testVibrateController.Start(TimeSpan.FromSeconds(sek));
            }
        }


        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        private void haeVarastot()
        {


            if (varasto.TryGetValue<List<Lohko>>("lohkolista", out lohkolista))
            {


                DateTime last;
                if (varasto.TryGetValue<DateTime>("viimeksi_tallennettu", out last))
                {
                    DateTime now = DateTime.Now;
                    if (last.Date != now.Date)
                    {
                        LohkoHaku.nouda();
                    }
                }

            }
            else
            {



                bool tmp;
                if (varasto.TryGetValue<bool>("first_start", out tmp))
                {
                    LohkoHaku.nouda();
                }
                else
                {
                    LohkoHaku.noudaFirst();
                    varasto.Add("first_start", true);
                }
            }

            if (!varasto.TryGetValue<bool>("varina", out varina))
                varina = true;


            if (!varasto.TryGetValue<string>("lohko", out Lohko_))
                Lohko_ = "B-nuorten SM-sarja";

            if (!varasto.TryGetValue<string>("GameId", out GameId_))
                GameId_ = "4456";

            if (!varasto.TryGetValue<bool>("otteluPaattynyt", out otteluPaattynyt))
                otteluPaattynyt = true;
            /*
            if (!varasto.TryGetValue<string>("viimeisinMaali", out viimeisin_maali))
                viimeisin_maali = null;
            */
            if (!varasto.TryGetValue<bool>("naytaKaikki", out naytaKaikki))
                naytaKaikki = true;

            if (!varasto.TryGetValue<bool>("alarmNoice", out alarmNoice))
                alarmNoice = false;

            IdleDetection(App.lockScreen);
        }

        public static void save()
        {

            DateTime now;
            bool booli;
            string stringi;
            if (varasto.TryGetValue<DateTime>("viimeksi_tallennettu", out now))
            {
                now = DateTime.Now;
                varasto["viimeksi_tallennettu"] = now;
            }
            else
            {
                now = DateTime.Now;
                varasto.Add("viimeksi_tallennettu", now);
            }

            if (varasto.TryGetValue<bool>("varina", out booli))
            {
                
                varasto["varina"] = varina;
            }
            else
            {
                
                varasto.Add("varina", varina);
            }

            if (varasto.TryGetValue<string>("lohko", out stringi))
            {
                varasto["lohko"] = Lohko_;
            }
            else
            {
                varasto.Add("lohko", Lohko_);
            }

            if (varasto.TryGetValue<string>("GameId", out stringi))
            {
                varasto["GameId"] = GameId_;
            }
            else
            {
                varasto.Add("GameId", GameId_);
            }

            if (varasto.TryGetValue<bool>("otteluPaattynyt", out booli))
            {

                varasto["otteluPaattynyt"] = otteluPaattynyt;
            }
            else
            {

                varasto.Add("otteluPaattynyt", otteluPaattynyt);
            }
            /*
            if (varasto.TryGetValue<string>("viimeisinMaali", out stringi))
            {

                varasto["viimeisinMaali"] = viimeisin_maali;
            }
            else
            {
                varasto.Add("viimeisinMaali", viimeisin_maali);
            }
            */
            if (varasto.TryGetValue<bool>("alarmNoice", out booli))
                varasto["alarmNoice"] = alarmNoice;
            else
                varasto.Add("alarmNoice", alarmNoice);

            if (varasto.TryGetValue<bool>("naytaKaikki", out booli))
                varasto["naytaKaikki"] = naytaKaikki;
            else
                varasto.Add("naytaKaikki", naytaKaikki);

            varasto.Save();

        }

        

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            haeVarastot();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            // Ensure that application state is restored appropriately

        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        public static void IdleDetection( bool value )
        {
            App.lockScreen = value;
            try
            {
                if (App.lockScreen)
                {
                    Microsoft.Phone.Shell.PhoneApplicationService.Current.ApplicationIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Disabled;

                    Microsoft.Phone.Shell.PhoneApplicationService.Current.UserIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Disabled;
                }
                else
                {
                    Microsoft.Phone.Shell.PhoneApplicationService.Current.ApplicationIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Enabled;

                    Microsoft.Phone.Shell.PhoneApplicationService.Current.UserIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Enabled;
                }
                
            }
            catch (InvalidOperationException ex)
            {
                // This exception is expected in the current release.
                MessageBox.Show(ex.Message);
            }

            // Possibly use the value of didEnable to decide what to do next.
            // If it is 'true', then your app will be deactivated. 
            // If it is 'false', then your app will keep running.
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}