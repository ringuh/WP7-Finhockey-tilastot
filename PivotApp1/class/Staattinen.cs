using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotApp1
{
    class Staattinen
    {
        private static string myStaticProperty = "mun teksti";

        public static string MyStaticProperty
        {
            get { return myStaticProperty; }
            set { myStaticProperty = value; }
        }
    }
}
