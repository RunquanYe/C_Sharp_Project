using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LightPatternSimulator.ViewModels
{
    public class BaseProductViewModel : BaseViewModel
    {
        public LightbarViewModel LightbarViewModel { get; set; }

        public ICommand ApplyChangesCommand { get; private set; }

        public string ProductType { get; set; }

        public string Name { get; set; }

        public Visibility LightbarSizeVisibility { get; set; }

        public Visibility NumModulesVisibility { get; set; }

        public BaseProductViewModel()
        {

        }
    }
}
