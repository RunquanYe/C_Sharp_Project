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
    public class nForceTrafficControl_ViewModel_Front8_Rear6 : BaseProductViewModel
    {

        public nForceTrafficControl_ViewModel_Front8_Rear6(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "nForce Interior";
            this.Name = "8 6";
            this.LightbarSizeVisibility = Visibility.Hidden;
            this.NumModulesVisibility = Visibility.Visible;

            LightbarViewModel = lightbarViewModel;
        }
    }
}
