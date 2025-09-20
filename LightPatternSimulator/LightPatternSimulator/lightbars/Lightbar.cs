using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LightPatternSimulator.lightbars
{
    /// <summary>
    /// Class <c>Lightbar</c> models a SOS-Lightbar
    /// </summary>
    public class Lightbar
    {

        /// <summary>
        /// Value <value>PatternName</value> stores the Lightbar's type
        /// </summary>
        public string LightbarType { get; set; }

        /// <summary>
        /// <value>Size</value> represents the length of the physical Lightbar (inches)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// <value>NumModules</value> represents how many modules are on this lightbar
        /// </summary>
        public int NumModules { get; set; }

        public string BoardImageFileName { get; set; }

        public BitmapImage BoardImage {
            get { return new BitmapImage(new Uri("pack://application:,,,/image/product/" + BoardImageFileName + ".PNG")); }     
        } 


        private List<Module> _Modules;

        /// <summary>
        /// Value <value>Modules</value> contains information on the modules associated with each lightbar
        /// </summary>
        public List<Module> Modules {
            get { return _Modules; }
            set { _Modules = value; }
        }

        public List<LightbarPattern> LightbarPatterns { get; set; }

        public List<Image> LightSet { get; set; }

    }

    /// <summary>
    /// Class <c>Module</c> models a module in a SOS-Lightbar
    /// </summary>
    public class Module
    {

        public static BitmapImage redLight = new BitmapImage(new Uri("pack://application:,,,/image/color/Red.PNG"));
        public static BitmapImage blueLight = new BitmapImage(new Uri("pack://application:,,,/image/color/Blue.PNG"));
        public static BitmapImage whiteLight = new BitmapImage(new Uri("pack://application:,,,/image/color/White.PNG"));
        public static BitmapImage greenLight = new BitmapImage(new Uri("pack://application:,,,/image/color/Green.PNG"));
        public static BitmapImage yellowLight = new BitmapImage(new Uri("pack://application:,,,/image/color/Yellow.PNG"));

        public string ModuleName { get; set; }

        public string ModuleNumber { get; set; }

        public BitmapImage MyBitmap {
            get { return GetImage(); }
        }

        private string _CurrentColor { get; set; }

        /// <summary>
        /// <value>CurrentColor</value> dictates what color the module is currently
        /// </summary>
        public string CurrentColor {
            get { return _CurrentColor; }
            //set { _CurrentColor = value; OnPropertyChanged("ImageFile"); }
            set { _CurrentColor = value; /*OnPropertyChanged("MyBitmap");*/ }
        }

        public double Opacity {
            get {
                return (double)_CurrentStrength / 100; }
        }

        private int _CurrentStrength { get; set; }
        /// <summary>
        /// <value>CurrentStrength</value> determines the intensity of the modules light
        /// </summary>
        public int CurrentStrength {
            get { return _CurrentStrength; }
            set { _CurrentStrength = value;  }
        }

        /// <summary>
        /// <value>IsBreakoutLight</value> specifies whether the module is to be used in 'Breakout' mode
        /// </summary>
        public bool IsBreakoutLight { get; set; }

        public string ImageFile {
            get { return CurrentStrength > 0 ? "image/color/" + CurrentColor + ".PNG" : ""; }
        }

        public BitmapImage GetImage()
        {

                switch (CurrentColor) {
                    case "Red":                
                        return redLight;
                    case "Blue":
                        return blueLight;
                    case "White":
                        return whiteLight;
                    case "Green":
                        return greenLight;
                    case "Yellow":
                        return yellowLight;
                }
                return null;

        }


        //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //    {
        //        Console.WriteLine("Updating {0} {1}", propertyName, ++i);

        //        if (propertyName == "All")
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        //        else
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
    }

    //TODO: rename to Pattern after the original class has been deleted
    public class LightbarPattern
    {
        public string PatternName { get; set; }

        public string PatternDescription { get; set; }

        public string[][] LightCombinations { get; set; }

        public int[] Milliseconds { get; set; }

    }
}
