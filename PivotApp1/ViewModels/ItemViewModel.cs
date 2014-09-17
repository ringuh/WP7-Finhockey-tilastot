using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PivotApp1
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _AAika;
        private string _koti;
        private string _tulos;
        private string _vieras;
        private string _peliaika;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string AAika
        {
            get
            {
                return _AAika;
            }
            set
            {
                if (value != _AAika)
                {
                    _AAika = value;
                    NotifyPropertyChanged("AAika");
                }
            }
        }

        
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Koti
        {
            get
            {
                return _koti;
            }
            set
            {
                if (value != _koti)
                {
                    _koti = value;
                    NotifyPropertyChanged("Koti");
                }
            }
        }

        public string Tulos
        {
            get
            {
                return _tulos;
            }
            set
            {
                if (value != _tulos)
                {
                    _tulos = value;
                    NotifyPropertyChanged("Tulos");
                }
            }
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Vieras
        {
            get
            {
                return _vieras;
            }
            set
            {
                if (value != _vieras)
                {
                    _vieras = value;
                    NotifyPropertyChanged("Vieras");
                }
            }
        }

        public string Peliaika
        {
            get
            {
                return _peliaika;
            }
            set
            {
                if (value != _peliaika)
                {
                    _peliaika = value;
                    NotifyPropertyChanged("Peliaika");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}