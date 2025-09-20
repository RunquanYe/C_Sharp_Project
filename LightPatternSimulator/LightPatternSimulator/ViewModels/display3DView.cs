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
    public class display3DView
    {
        public LightbarViewModel LightbarViewModel { get; set; }

        public ICommand ApplyChangesCommand { get; private set; }

        public display3DView(LightbarViewModel lightbarViewModel)
        {
            LightbarViewModel = lightbarViewModel;
            LightbarViewModel.CurrentlySelectedLightbar = LightbarViewModel.Lightbars[0];
        }
    }
}
