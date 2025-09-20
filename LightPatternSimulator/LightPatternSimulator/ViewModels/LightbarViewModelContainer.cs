using LightPatternSimulator.lightbars;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace LightPatternSimulator.ViewModels
{
    public class LightbarViewModelContainer : BaseViewModel
    {
        public LightbarViewModel LightbarViewModel { get; set; }

        public ObservableCollection<ColorItem> ColorList { get; }

        private List<BaseProductViewModel> _ViewModels { get; set; }

        public List<BaseProductViewModel> ViewModels {
            get { return _ViewModels; }
            set { _ViewModels = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<Color> _RegionColors { get; set; }

        public ObservableCollection<Color> RegionColors {
            get { return _RegionColors; }
            set
            {
                _RegionColors = value; OnPropertyChanged(); Console.WriteLine("");
            }
        }

        private Dictionary<string, List<string>> _LightbarTypes { get; set; }

        public Dictionary<string, List<String>> LightbarTypes {
            get { return _LightbarTypes; }
            set { _LightbarTypes = value; OnPropertyChanged(); }
        }

        private string _SelectedType { get; set; }

        public string SelectedType {
            get { return _SelectedType; }
            set { _SelectedType = value; OnPropertyChanged(); OnPropertyChanged("SelectedList"); SelectedSize = SelectedList[0]; }
        }

        public List<string> SelectedList {
            get
            {

                List<string> rtn;

                try
                {
                    rtn = LightbarTypes[SelectedType];
                }
                catch (KeyNotFoundException)
                {
                    rtn = LightbarTypes[SelectedType.Substring(1, SelectedType.IndexOf(",") - 1)];
                }

                return rtn;

            }

        }

        private List<string> _PatternList { get; set; }
        public List<string> PatternList {
            get { return _PatternList; }
            set { _PatternList = value; OnPropertyChanged(); }
        }

        private string _SelectedPattern { get; set; }
        public string SelectedPattern {
            get { return _SelectedPattern; }
            set { _SelectedPattern = value; OnPropertyChanged(); }
        }

        private string _SelectedSize { get; set; }
        public string SelectedSize {
            get { return _SelectedSize; }
            set { _SelectedSize = value; OnPropertyChanged(); UpdateViewModel(); }
        }

        private void UpdateViewModel()
        {
            if (SelectedSize != null)
            {
                LightbarViewModel.LightbarOff();

                foreach (BaseProductViewModel viewModel in ViewModels)
                {
                    if (viewModel.ProductType.Equals(SelectedType) && viewModel.Name.Equals(SelectedSize))
                    {
                        SelectedViewModel = viewModel;                    
                        Console.WriteLine("Using {0} {1}", viewModel.ProductType, SelectedSize);
                        break;
                    }
                }

                foreach (Lightbar lightbar in LightbarViewModel.Lightbars)
                {

                    if (lightbar.LightbarType.Equals(SelectedType) && lightbar.Size.ToString().Equals(SelectedSize))
                    {
                        LightbarViewModel.CurrentlySelectedLightbar = lightbar;

                        break;
                    }
                }

            }


        }

        private BaseViewModel _SelectedViewModel { get; set; }
        public BaseViewModel SelectedViewModel {
            get { return _SelectedViewModel; }
            set { _SelectedViewModel = value; OnPropertyChanged(); }
        }

        public LightbarViewModelContainer(LightbarViewModel lightbarViewModel)
        {

            ColorList = new ObservableCollection<ColorItem>
            {
                new ColorItem(Colors.Black, "None"),
                new ColorItem(Colors.Red, "Red"),
                new ColorItem(Colors.Blue, "Blue"),
                new ColorItem(Colors.White, "White"),
                new ColorItem(Colors.Green, "Green"),
                new ColorItem(Colors.Yellow, "Yellow")
              };

            Color black = (Color)ColorList[0].Color;

            RegionColors = new ObservableCollection<Color>
            {
                black, black, black,
                black, black, black,
                black, black, black,
                black, black, black
            };

            LightbarViewModel = lightbarViewModel;
            lightbarViewModel.RegionColors = RegionColors;

            ViewModels = new List<BaseProductViewModel>
            {
                new nForceExterior_ViewModel_24(LightbarViewModel),
                new nForceExterior_ViewModel_36(LightbarViewModel),
                new nForceExterior_ViewModel_42(LightbarViewModel),
                new nForceExterior_ViewModel_48(LightbarViewModel),
                new nForceExterior_ViewModel_54(LightbarViewModel),
                new nForceExterior_ViewModel_60(LightbarViewModel),
                new nForceExterior_ViewModel_72(LightbarViewModel),
                new mPowerLightbar_ViewModel_4inch44(LightbarViewModel),
                new mPowerLightbar_ViewModel_4inch48(LightbarViewModel),
                new mPowerLightbar_ViewModel_4inch53(LightbarViewModel),
                new mPowerLightbar_ViewModel_6inch42(LightbarViewModel),
                new mPowerLightbar_ViewModel_6inch48(LightbarViewModel),
                new mPowerLightbar_ViewModel_6inch55(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front6_Rear4(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front6_Rear6(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front6_Rear8(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front6_RearNone(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front8_Rear4(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front8_Rear6(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front8_Rear8(LightbarViewModel),
                new nForceTrafficControl_ViewModel_Front8_RearNone(LightbarViewModel),
                new nForceTrafficControl_ViewModel_FrontNone_Rear8(LightbarViewModel),
                new nForceTrafficControl_ViewModel_FrontNone_Rear6(LightbarViewModel),
                new nForceTrafficControl_ViewModel_FrontNone_Rear4(LightbarViewModel),
                new FordFrontVisor_ViewModel(LightbarViewModel)
            };
            SelectedViewModel = ViewModels[0];

            Dictionary<string, List<string>> lightbarTypes = new Dictionary<string, List<string>>();
 

            foreach (Lightbar lightbar in LightbarViewModel.Lightbars)
            {

                if (!lightbarTypes.ContainsKey(lightbar.LightbarType))
                {
                    lightbarTypes[lightbar.LightbarType] = new List<string>();
                }
                lightbarTypes[lightbar.LightbarType].Add(lightbar.Size.ToString());
            }

            LightbarTypes = lightbarTypes;
            SelectedType = LightbarTypes.Keys.ToList()[0];
            SelectedSize = SelectedList[0];

            var task = Task.Run(async () =>
            {
                OnPropertyChanged("RegionColors");
                await Task.Delay(200);
            });
        }

    }
}
