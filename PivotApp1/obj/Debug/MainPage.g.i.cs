﻿#pragma checksum "C:\Users\Anssi\Documents\Visual Studio 2012\Projects\PivotApp1\PivotApp1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "789F8BBEB38E235AD503AB714CCC6CCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PivotApp1 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage myMainPage;
        
        internal System.Windows.DataTemplate spinner;
        
        internal System.Windows.DataTemplate OtteluOtsikko;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton BackFront;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton VaihdaLohkoa;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton Asetukset;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot MainPivot;
        
        internal Microsoft.Phone.Controls.PivotItem OttelutPivot;
        
        internal Microsoft.Phone.Controls.LongListSelector PaivanOttelutList;
        
        internal Microsoft.Phone.Controls.LongListSelector SarjataulukkoList;
        
        internal Microsoft.Phone.Controls.LongListSelector TilastotList;
        
        internal Microsoft.Phone.Controls.PivotItem VanhatOttelutPivot;
        
        internal Microsoft.Phone.Controls.LongListSelector VanhatOttelutList;
        
        internal Microsoft.Phone.Controls.LongListSelector TehopisteetList;
        
        internal System.Windows.Controls.TextBlock PeliAika;
        
        internal System.Windows.Controls.TextBlock KotiJoukkue;
        
        internal System.Windows.Controls.TextBlock PeliTulos;
        
        internal System.Windows.Controls.TextBlock VierasJoukkue;
        
        internal System.Windows.Controls.TextBlock OtteluEratulokset;
        
        internal System.Windows.Controls.TextBlock OtteluTorjunnat;
        
        internal Microsoft.Phone.Controls.LongListSelector OtteluGridList;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PivotApp1;component/MainPage.xaml", System.UriKind.Relative));
            this.myMainPage = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("myMainPage")));
            this.spinner = ((System.Windows.DataTemplate)(this.FindName("spinner")));
            this.OtteluOtsikko = ((System.Windows.DataTemplate)(this.FindName("OtteluOtsikko")));
            this.BackFront = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("BackFront")));
            this.VaihdaLohkoa = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("VaihdaLohkoa")));
            this.Asetukset = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("Asetukset")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("MainPivot")));
            this.OttelutPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("OttelutPivot")));
            this.PaivanOttelutList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("PaivanOttelutList")));
            this.SarjataulukkoList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("SarjataulukkoList")));
            this.TilastotList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("TilastotList")));
            this.VanhatOttelutPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("VanhatOttelutPivot")));
            this.VanhatOttelutList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("VanhatOttelutList")));
            this.TehopisteetList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("TehopisteetList")));
            this.PeliAika = ((System.Windows.Controls.TextBlock)(this.FindName("PeliAika")));
            this.KotiJoukkue = ((System.Windows.Controls.TextBlock)(this.FindName("KotiJoukkue")));
            this.PeliTulos = ((System.Windows.Controls.TextBlock)(this.FindName("PeliTulos")));
            this.VierasJoukkue = ((System.Windows.Controls.TextBlock)(this.FindName("VierasJoukkue")));
            this.OtteluEratulokset = ((System.Windows.Controls.TextBlock)(this.FindName("OtteluEratulokset")));
            this.OtteluTorjunnat = ((System.Windows.Controls.TextBlock)(this.FindName("OtteluTorjunnat")));
            this.OtteluGridList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("OtteluGridList")));
        }
    }
}

