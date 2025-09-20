using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LightPatternSimulator.ViewModels
{

    public class nForceTrafficControl_ViewModel_FrontNone_Rear8 : BaseProductViewModel
    {

        public nForceTrafficControl_ViewModel_FrontNone_Rear8(LightbarViewModel lightbarViewModel)
        {

            this.ProductType = "nForce Interior";
            this.Name = "None 8";
            this.LightbarSizeVisibility = Visibility.Hidden;
            this.NumModulesVisibility = Visibility.Visible;

            LightbarViewModel = lightbarViewModel;
        }
    }
}
