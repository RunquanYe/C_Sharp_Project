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
    public class nForceTrafficControl_ViewModel_Front6_Rear8 : BaseProductViewModel
    {

        public nForceTrafficControl_ViewModel_Front6_Rear8(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "nForce Interior";
            this.Name = "6 8";
            this.LightbarSizeVisibility = Visibility.Hidden;
            this.NumModulesVisibility = Visibility.Visible;

            LightbarViewModel = lightbarViewModel;
        }
    }
}
