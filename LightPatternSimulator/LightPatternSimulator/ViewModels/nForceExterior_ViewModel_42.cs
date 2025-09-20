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
    public class nForceExterior_ViewModel_42 : BaseProductViewModel
    {

        public nForceExterior_ViewModel_42(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "nForce Exterior";
            this.Name = "42";
            this.LightbarSizeVisibility = Visibility.Visible;
            this.NumModulesVisibility = Visibility.Hidden;

            LightbarViewModel = lightbarViewModel;  
        }
    }
}
