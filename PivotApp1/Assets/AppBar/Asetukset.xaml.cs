using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp1
{
    public partial class Asetukset : PhoneApplicationPage
    {
        public Asetukset()
        {
            InitializeComponent();
            lataaAsetukset();
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

        private void lataaAsetukset()
        {
            this.Varina.IsChecked = App.varina;
            this.Aanimerkki.IsChecked = App.alarmNoice;
            this.LockScreen.IsChecked = App.lockScreen;
            this.Simu.IsChecked = App.simulaatio;
            this.PaivanOttelut.IsChecked = App.naytaKaikki;

            var tasoByLohko = from lohko in App.lohkolista
                                 group lohko by lohko.itaso into c
                                 orderby c.Key
                                 select new Group<Lohko>(c.Key, c);

            AsetuksetList.ItemsSource = tasoByLohko;


        }

        private void LLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // If selected item is null, do nothing
            if (AsetuksetList.SelectedItem == null)
                return;

            //MessageBox.Show( e.AddedItems.ToString() );

            Lohko tmp = ((LongListSelector)sender).SelectedItem as Lohko;
          


            for (int i = 0; i < App.lohkolista.Count(); ++i)
            {
                if (App.lohkolista[i].md5 == tmp.md5)
                {
                    App.lohkolista[i].IsChecked = tmp.IsChecked;
                    App.save();
                    i = App.lohkolista.Count();
                }
            }
            
            // Reset selected item to null
            AsetuksetList.SelectedItem = null;
        }

        private void VarinaEvent(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.vibrate(0.5);
            App.varina = ((ToggleSwitch)sender).IsChecked.GetValueOrDefault();
            App.save();
        }

        private void SimuEvent(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.simulaatio = ((ToggleSwitch)sender).IsChecked.GetValueOrDefault();
            App.simu_rivi = 1;
            App.otteluRefTime = 10;
            

            if (App.simulaatio)
            {
                App.otteluPaattynyt = false;
                App.viimeisin_maali = null;
                NavigationService.Navigate(new Uri("/MainPage.xaml?index=5", UriKind.Relative));
                
            }
        }

        private void LockScreenEvent(object sender, System.Windows.Input.GestureEventArgs e)
        {

            App.IdleDetection(((ToggleSwitch)sender).IsChecked.GetValueOrDefault());
            if (!((ToggleSwitch)sender).IsChecked.GetValueOrDefault())
                this.LockScreen.IsChecked = true;

            App.save();
        }

        private void AanimerkkiEvent(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.alarmNoice = ((ToggleSwitch)sender).IsChecked.GetValueOrDefault();
            App.save();

        }

        private void PaivanOttelutEvent(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.naytaKaikki = ((ToggleSwitch)sender).IsChecked.GetValueOrDefault();
            App.save();
        }
    }
}