using LightPatternSimulator.lightbars;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using Catel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace LightPatternSimulator.Data
{
    class DLL
    {
        /// <summary>
        /// List of Lightbars loaded in from the JSON file
        /// </summary>
        public List<Lightbar> Lightbars { get; set; }

        /// <summary>
        /// How long the Task delays after an iteration
        /// </summary>
        public static int FlashTime = 20;

        /// <summary>
        /// Determines what user selected color (0,1,2) will be picked
        /// </summary>
        private int loopCount = 0;

        /// <summary>
        /// List of user selected colors
        /// </summary>
        public ObservableCollection<Color> RegionColors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputFile">JSON file used to load Lightbars</param>
        public DLL(String inputFile = null)
        {
            LoadJson(inputFile);
        }

        /// <summary>
        /// Loads a JSON of lightbars
        /// </summary>
        /// <param name="file">Serialized Lightbars</param>
        private void LoadJson(string file)
        {
            Console.WriteLine("Loading Lightbar");

            if (file == null)
            {
                file = System.IO.Path.GetFullPath("..\\..\\resources\\jsons\\lightbar1.JSON");
            }

            try
            {

                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    this.Lightbars = JsonConvert.DeserializeObject<List<Lightbar>>(json);
                }

                Console.WriteLine("Finished file import");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error importing file");
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Turns off the selected modules on the provided lightbar
        /// </summary>
        /// <param name="lightbar">The lightbar being turned off</param>
        /// <param name="modules">The modules being turned off, default is all</param>
        public void Off(Lightbar lightbar, List<Module> modules = null)
        {
            //Determines if all modules or the ones in the parameter are being turned off
            List<Module> targetModules = modules ?? lightbar.Modules;

            SetColorStrength("Black", 0, lightbar, targetModules);
                
        }

        /// <summary>
        /// Sets each breakout light to White-STEADY
        /// </summary>
        public void Breakout(Lightbar lightbar, List<Module> modules = null)
        {
            //Determines if all modules or the ones in the parameter are being put in 'Breakout' mode
            List<Module> targetModules = modules ?? lightbar.Modules;

            foreach (Module module in targetModules)
            {
                if (module.IsBreakoutLight)
                {
                    SetColorStrength("White", 100, lightbar);
                }
            }
        }

        /// <summary>
        /// Flashes the provided modules at 100 power
        /// </summary>
        public async Task Flash(CancellationToken ct, Lightbar lightbar, List<Module> modules = null)
        {

            //Determines if all modules are being flashed or just the ones in the parameter
            List<Module> targetModules = modules ?? lightbar.Modules;

           // UpdateColors(lightbar);
            await FlashModules(100, 0, ct, lightbar, targetModules, true);
        }

        /// <summary>
        /// Flashes the provided modules at 50 power
        /// </summary>
        public async Task FlashLowPower(CancellationToken ct, Lightbar lightbar, List<Module> modules = null)
        {
            //Determines if all modules are being flashed or just the ones in the parameter
            List<Module> targetModules = modules ?? lightbar.Modules;

            //UpdateColors(lightbar);
            await FlashModules(50, 0, ct, lightbar, targetModules, true);
            
        }

        /// <summary>
        /// Holds the provided modules at 100 power
        /// </summary>
        public async Task Steady(CancellationToken ct, Lightbar lightbar, List<Module> modules = null)
        {
            //Determines if all modules are being flashed or just the ones in the parameter
            List<Module> targetModules = modules ?? lightbar.Modules;
            loopCount = 0;
            await UpdateColors(lightbar);
            await FlashModules(100, 100, ct, lightbar, targetModules);
        }

        /// <summary>
        /// Flashes the provided modules at 50 power
        /// </summary>
        public async Task Cruise(CancellationToken ct, Lightbar lightbar, List<Module> modules = null)
        {
            //Determines if all modules are being flashed or just the ones in the parameter
            List<Module> targetModules = modules ?? lightbar.Modules;
            loopCount = 0;
            await UpdateColors(lightbar);
            await FlashModules(50, 50, ct, lightbar, targetModules);
        }

        /// <summary>
        /// Helper method to flash modules
        /// </summary>
        private async Task FlashModules(int strength1, int strength2, CancellationToken ct, Lightbar lightbar, List<Module> modules = null, bool updateColors = false)
        {
            //Determines if all modules are being flashed or just the ones in the parameter
            List<Module> targetModules = modules ?? lightbar.Modules;

            if (updateColors)
            {
                await UpdateColors(lightbar);
            }

            foreach (Module module in targetModules)
            {
                if (!ct.IsCancellationRequested)
                {
                    module.CurrentStrength = (module.CurrentStrength == strength1) ? strength2 : strength1;
                }
                else
                {
                    break;
                }

            }        

            if (!ct.IsCancellationRequested)
            {
                await Task.Delay(FlashTime);

            }

        }

        /// <summary>
        /// Updates the colors on the Lightbar based on the loopCount variable
        /// </summary>
        private async Task UpdateColors(Lightbar lightbar)
        {

            string frontColor = ConvertHex(RegionColors[loopCount]);
            string rearColor = ConvertHex(RegionColors[loopCount + 3]);
            string driverColor = ConvertHex(RegionColors[loopCount + 6]);
            string passColor = ConvertHex(RegionColors[loopCount + 9]);

            if (!frontColor.Equals("Black"))
            {
               SetColor(lightbar, frontColor, GetFront(lightbar));
            }

            if (!rearColor.Equals("Black"))
            {
                SetColor(lightbar, rearColor, GetRear(lightbar));
            }

            if (!driverColor.Equals("Black"))
            {
                SetColor(lightbar, driverColor, GetDriver(lightbar));
            }

            if (!passColor.Equals("Black"))
            {
                SetColor(lightbar, passColor, GetPassenger(lightbar));
            }

            loopCount++;
            loopCount %= 3;

            await Task.Delay(1);
        }

        /// <summary>
        /// Converts hex values to the English equivalent of valid colors
        /// </summary>
        public string ConvertHex(Color color)
        {

            string rtn = "";

            switch (color.ToString())
            {
                case "#FF000000":
                    rtn = "Black";
                    break;
                case "#FFFF0000":
                    rtn = "Red";
                    break;
                case "#FF0000FF":
                    rtn = "Blue";
                    break;
                case "#FFFFFFFF":
                    rtn = "White";
                    break;
                case "#FF008000":
                    rtn = "Green";
                    break;
                case "#FFFFFF00":
                    rtn = "Yellow";
                    break;
            }

            return rtn;
        }

        /// <summary>
        /// Returns the Front modules
        /// </summary>
        public List<Module> GetFront(Lightbar lightbar)
        {
            return GetSpecificModules("Front", lightbar);
        }

        /// <summary>
        /// Returns the Rear modules
        /// </summary>
        public List<Module> GetRear(Lightbar lightbar)
        {
            return GetSpecificModules("Rear", lightbar);
        }

        /// <summary>
        /// Returns the Driver modules
        /// </summary>
        public List<Module> GetDriver(Lightbar lightbar)
        {
            return GetSpecificModules("Driver", lightbar);
        }

        /// <summary>
        /// Returns the Passenger modules
        /// </summary>
        public List<Module> GetPassenger(Lightbar lightbar)
        {
            return GetSpecificModules("Passenger", lightbar);
        }

        /// <summary>
        /// Returns the Corner modules
        /// </summary>
        public List<Module> GetCorners(Lightbar lightbar)
        {
            return GetSpecificModules("Corner", lightbar);
        }

        /// <summary>
        /// Returns the Inboard modules
        /// </summary>
        public List<Module> GetInboard(Lightbar lightbar)
        {
            return GetSpecificModules("Inboard", lightbar);
        }

        /// <summary>
        /// Returns breakout modules
        /// </summary>
        public List<Module> GetBreakout(Lightbar lightbar)
        {
            List<Module> rtn = new List<Module>();

            foreach (Module module in lightbar.Modules)
            {

                if (module.IsBreakoutLight)
                {
                    rtn.Add(module);
                }
            }
            return rtn;
        }

        /// <summary>
        /// Helper method to get selected modules
        /// </summary>
        private List<Module> GetSpecificModules(string target, Lightbar lightbar)
        {

            List<Module> rtn = new List<Module>();

            foreach (Module module in lightbar.Modules)
            {

                if (module.ModuleName.Contains(target))
                {
                    rtn.Add(module);
                }
            }
            return rtn;
        }

        /// <summary>
        /// Sets the color and light strength for every color
        /// </summary>
        /// <param name="color">The new color for every light</param>
        /// <param name="strength">The new intensity for every light</param>
        public void SetColorStrength(string color, int lightStrength, Lightbar lightbar, List<Module> modules = null)
        {
            SetColor(lightbar, color, modules);
            SetStrength(lightbar, lightStrength, modules);
        }

        /// <summary>
        /// Sets every module's color to the passed value
        /// </summary>
        /// <param name="color">The new color for every light</param>
        public void SetColor(Lightbar lightbar, string color, List<Module> modules = null)
        {
            //Determines if the color of all modules or the ones in the parameter is being set
            List<Module> targetModules = modules ?? lightbar.Modules;

            foreach (Module module in targetModules)
            {

                module.CurrentColor = color;

            }


        }

        /// <summary>
        /// Sets every module's light strength the passed value
        /// </summary>
        /// <param name="lightStrength">The new intensity for every light</param>
        public void SetStrength(Lightbar lightbar, int lightStrength, List<Module> modules = null)
        {
            //Determines if the intensity of all modules or the ones in the parameter is being set
            List<Module> targetModules = modules ?? lightbar.Modules;

            foreach (Module module in lightbar.Modules)
            {
                module.CurrentStrength = lightStrength;
            }

        }

    }
}
