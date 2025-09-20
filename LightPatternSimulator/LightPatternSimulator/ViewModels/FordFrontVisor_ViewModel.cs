using LightPatternSimulator.Data;
using LightPatternSimulator.lightbars;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace LightPatternSimulator.ViewModels
{
    public class FordFrontVisor_ViewModel : BaseProductViewModel
    {
        public FordFrontVisor_ViewModel(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "Ford Front Visor/Rear Traffic Warning";
            this.Name = "22";
            this.LightbarSizeVisibility = Visibility.Hidden;
            this.NumModulesVisibility = Visibility.Hidden;

            LightbarViewModel = lightbarViewModel;
        }
    }
}
