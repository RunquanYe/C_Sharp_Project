using LightPatternSimulator.Data;
using LightPatternSimulator.lightbars;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LightPatternSimulator.ViewModels
{
    public class LightbarViewModel : BaseViewModel
    {
        private DLL dll { get; set; }

        Boolean isStart = false;

        CancellationTokenSource tokenSource;
        CancellationToken ct;

     

        private Lightbar _CurrentlySelectedLightbar { get; set; }

        public Lightbar CurrentlySelectedLightbar {
            get { return _CurrentlySelectedLightbar; }
            set { _CurrentlySelectedLightbar = value; OnPropertyChanged(); Modules = _CurrentlySelectedLightbar.Modules; }
        }


        private List<Module> _Modules { get; set; }

        public List<Module> Modules {
            get {return _Modules; }
            set { _Modules = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Lightbar> _Lightbars = new ObservableCollection<Lightbar>();

        public ObservableCollection<Lightbar> Lightbars {
            get { return _Lightbars; }
            set { _Lightbars = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Color> _RegionColors { get; set; }

        public ObservableCollection<Color> RegionColors { get
            {
                return _RegionColors;
            }
            set
            {
                _RegionColors = value; dll.RegionColors = value;
            }
        }

        public LightbarViewModel()
        {
            dll = new DLL();

            Lightbars = new ObservableCollection<Lightbar>(dll.Lightbars);
            CurrentlySelectedLightbar = Lightbars[0];
            Modules = CurrentlySelectedLightbar.Modules;

        }

        private void Start()
        {

            //Create a new token source.  After being canceled, a token source can't be restarted
            tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token;

            //timer.Start();
            isStart = true;


            var task = Task.Run(async () =>
            {

                //Continuously run pattern until tokenSource is canceled
                while (!ct.IsCancellationRequested)
                {
                    // await dll.Flash(ct, currentlySelectedLightbar);
                    await RunLightbar(CurrentlySelectedLightbar, ct);
                }
            }, ct);



        }

        private void Stop()
        {

            //timer.Stop();
            isStart = false;

            //Cancels the token source
            tokenSource.Cancel();

            dll.Off(CurrentlySelectedLightbar);

            OnPropertyChanged("Lightbars");

            Console.WriteLine();
            Console.WriteLine("Finished Running pattern");

        }

        public void LightbarOff()
        {
            dll.Off(CurrentlySelectedLightbar);
        }

        private async Task RunLightbar(Lightbar lightbar, CancellationToken ct)
        {

            //Run the specified pattern
            //await dll.FlashSelectedPattern(lightbar, ct);

        }


        private void Flash()
        {
            RunMethod(0);
        }

        private void FlashLowPower()
        {
            RunMethod(1);
        }

        private void Steady()
        {
            RunMethod(2);
        }

        private void Cruise()
        {
            RunMethod(3);
        }

        private void RunMethod(int method)
        {
            tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token;

            if (!isStart)
            {
                isStart = true;
                if (CurrentlySelectedLightbar == null)
                {
                    //TODO: Replace with user selectiuon
                    CurrentlySelectedLightbar = Lightbars[0]; ;
                }

                //TODO: remove when user can select color/options from the GUI
                //dll.SetColorStrength("Blue", 100, CurrentlySelectedLightbar);


                var task = Task.Run(async () =>
                {

                    //Continuously run pattern until tokenSource is cancelled
                    while (!ct.IsCancellationRequested)
                    {
                        

                        if (method == 0)
                        {
                            await dll.Flash(ct, CurrentlySelectedLightbar);
                        }
                        else if (method == 1)
                        {
                            await dll.FlashLowPower(ct, CurrentlySelectedLightbar);                  
                        }

                        else if (method == 2)
                        {
                            await dll.Steady(ct, CurrentlySelectedLightbar);
                        }

                        else if (method == 3)
                        {
                            await dll.Cruise(ct, CurrentlySelectedLightbar);
                        }

                        OnPropertyChanged("Modules");
                    }
                }, ct);

            }

        }

        private RelayCommand<ICommand> _commandStart;

        public ICommand CmdStart {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => Start(), param => CanRun());
                return _commandStart;
            }
        }

        public ICommand CmdStop {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => Stop(), param => !CanRun());
                return _commandStart;
            }
        }

        public ICommand CmdFlash {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => Flash(), param => CanRun());
                return _commandStart;
            }
        }

        public ICommand CmdFlashLowPower {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => FlashLowPower(), param => CanRun());
                return _commandStart;
            }
        }

        public ICommand CmdSteady {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => Steady(), param => CanRun());
                return _commandStart;
            }
        }

        public ICommand CmdCruise {
            get
            {
                _commandStart = new RelayCommand<ICommand>(param => Cruise(), param => CanRun());
                return _commandStart;
            }
        }

        public bool CanRun()
        {
            return !isStart;
        }
    }

}
