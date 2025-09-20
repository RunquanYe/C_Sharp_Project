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
    public class mPowerLightbar_ViewModel_6inch55 : BaseProductViewModel
    {

        public mPowerLightbar_ViewModel_6inch55(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "mPower 6inch";
            this.Name = "55";
            this.LightbarSizeVisibility = Visibility.Visible;
            this.NumModulesVisibility = Visibility.Hidden;

            LightbarViewModel = lightbarViewModel;
        }
    }
}
