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
    public partial class LohkoValitsin : PhoneApplicationPage
    {
        public LohkoValitsin()
        {
            InitializeComponent();

            taytaLista();
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

        private void LohkoValittu(object sender, SelectionChangedEventArgs e)
        {
            Lohko l = ((LongListSelector)sender).SelectedItem as Lohko;

            

            App.Lohko_ = l.lohko;
            NavigationService.Navigate(new Uri("/MainPage.xaml?index="+App.lastPivot, UriKind.Relative));
        }

       private void taytaLista()
        {
            List<Lohko> karsittu_lohko = new List<Lohko>();

            karsittu_lohko = App.lohkoKarsinta();

            var karsitutLohkot = from lohko in karsittu_lohko
                                group lohko by lohko.itaso into c
                                orderby c.Key
                                select new Group<Lohko>(c.Key, c);


            LohkoList.ItemsSource = null;
            LohkoList.ItemsSource = karsitutLohkot;
        }
    }
}