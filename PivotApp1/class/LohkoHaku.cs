using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PivotApp1
{
    class LohkoHaku
    {
        public LohkoHaku()
        {
        }

        public static void nouda()
        {

            
            WebClient spinnerwebClient = new WebClient();
            string url = "http://home.tamk.fi/~e2avaaka/tilastot/servu/sql_haku.php?cmd=lohkot";
            Uri uri = new Uri(url);
            // Lähetetään web request (pyydetään säätietoa)
            spinnerwebClient.DownloadStringAsync(uri);
            spinnerwebClient.DownloadStringCompleted += spinnerwebClient_DownloadStringCompleted;


        }

        private static void spinnerwebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsoniksi = "{\"lohkot\":" + e.Result + '}';
            LohkotRootObject st = (LohkotRootObject)JsonConvert.DeserializeObject<LohkotRootObject>(jsoniksi);




            if (App.varasto.TryGetValue<List<Lohko>>("lohkolista", out App.lohkolista))
            {
                App.varasto["lohkolista"] = st.lohkot;

            }
            else
                App.varasto.Add("lohkolista", st.lohkot);
            
            
            App.lohkolista = st.lohkot;
            App.save();
        }

        public static void noudaFirst()
        {

            MessageBox.Show("haetaan lohkot uudestaan");
            WebClient spinnerwebClient = new WebClient();
            string url = "http://home.tamk.fi/~e2avaaka/tilastot/servu/sql_haku.php?cmd=lohkot";
            Uri uri = new Uri(url);
            // Lähetetään web request (pyydetään säätietoa)
            spinnerwebClient.DownloadStringAsync(uri);
            spinnerwebClient.DownloadStringCompleted += spinnerwebClient_DownloadStringCompletedFirst;


        }

        private static void spinnerwebClient_DownloadStringCompletedFirst(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsoniksi = "{\"lohkot\":" + e.Result + '}';
            LohkotRootObject st = (LohkotRootObject)JsonConvert.DeserializeObject<LohkotRootObject>(jsoniksi);

            foreach (Lohko i in st.lohkot)
            {
                if (Array.IndexOf(alkuarvot, i.taso) > -1)
                    i.IsChecked = true;
            }


            if (App.varasto.TryGetValue<List<Lohko>>("lohkolista", out App.lohkolista))
            {
                App.varasto["lohkolista"] = st.lohkot;

            }
            else
                App.varasto.Add("lohkolista", st.lohkot);


            App.lohkolista = st.lohkot;
            App.save();
        }

        private static string[] alkuarvot = new string[] { "2", "14", "18", "25" };
    }
}
