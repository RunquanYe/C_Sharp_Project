using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace LightPatternSimulator.Views
{
    /// <summary>
    /// Interaction logic for display3DView.xaml
    /// </summary>
    public partial class display3DView : UserControl
    {
    List<CubeVisual3D> lightSet { get; set; }

    //the cop car model for binding to in WPF
    Model3DGroup wholePackage;
    Model3D copCarModel;
    Model3D lightbarModel;

    //Property for the binding with the CopCar
    public Model3D ourModel { get; set; }
    public Model3D carModel { get; set; }
    public Model3D mPowerLightbar { get; set; }

        public display3DView()
        {
            InitializeComponent();
            initVars();

        }
        private void initVars()
        {
            double lightSideLength = 0.05;
            double carHeight = 0.78;
            double offsetToLightbarSpot = -0.35;
            double inFront = offsetToLightbarSpot + 0.14;
            double inBack = offsetToLightbarSpot - 0.14;
            double between = offsetToLightbarSpot;

            for (int i = 0; i < 14; i++)
            {
                lightSet[i].SideLength = lightSideLength;

            }

            //hardcoding this for now to see if I can get this to work 

            lightSet[0].Transform = new TranslateTransform3D(-0.62, between, carHeight);
            lightSet[0].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[0]);

            lightSet[1].Transform = new TranslateTransform3D(-0.5, inFront, carHeight);
            lightSet[1].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[1]);

            lightSet[2].Transform = new TranslateTransform3D(-0.3, inFront, carHeight);
            lightSet[2].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[2]);

            lightSet[3].Transform = new TranslateTransform3D(-0.1, inFront, carHeight);
            lightSet[3].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[3]);

            lightSet[4].Transform = new TranslateTransform3D(0.1, inFront, carHeight);
            lightSet[4].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[4]);

            lightSet[5].Transform = new TranslateTransform3D(0.3, inFront, carHeight);
            lightSet[5].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[5]);

            lightSet[6].Transform = new TranslateTransform3D(0.5, inFront, carHeight);
            lightSet[6].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[6]);

            lightSet[7].Transform = new TranslateTransform3D(-0.5, inBack, carHeight);
            lightSet[7].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[7]);

            lightSet[8].Transform = new TranslateTransform3D(-0.3, inBack, carHeight);
            lightSet[8].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[8]);

            lightSet[9].Transform = new TranslateTransform3D(-0.1, inBack, carHeight);
            lightSet[9].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[9]);

            lightSet[10].Transform = new TranslateTransform3D(0.1, inBack, carHeight);
            lightSet[10].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[10]);

            lightSet[11].Transform = new TranslateTransform3D(0.3, inBack, carHeight);
            lightSet[11].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[11]);

            lightSet[12].Transform = new TranslateTransform3D(0.5, inBack, carHeight);
            lightSet[12].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[12]);

            lightSet[13].Transform = new TranslateTransform3D(0.62, between, carHeight);
            lightSet[13].Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));
            m_helix_viewport.Children.Add(lightSet[13]);

            //The Importer to load .obj files
            ModelImporter importer = new ModelImporter();

            //The Material (Color) that is applyed to the importet objects
            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Silver));
            importer.DefaultMaterial = material;

            wholePackage = new Model3DGroup();

            copCarModel = importer.Load(@"..\\..\\GraphicsModels\\Vehicles\\cobraR.obj");
            lightbarModel = importer.Load(@"..\\..\\GraphicsModels\\LightBars\\EMPLB-6INX48-solid.obj");

            //rotate whole CopCar to have it upright
            RotateTransform3D copCarRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            copCarRotateTransform.CenterX = 0;
            copCarRotateTransform.CenterY = 0;
            copCarRotateTransform.CenterZ = 0;
            copCarModel.Transform = copCarRotateTransform;

            Transform3DGroup lightbarTransforms = new Transform3DGroup();

            //rotates lightbar to correct orientation
            RotateTransform3D lightbarRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            lightbarRotateTransform.CenterX = 0;
            lightbarRotateTransform.CenterY = 0;
            lightbarRotateTransform.CenterZ = 0;
            lightbarTransforms.Children.Add(lightbarRotateTransform);

            //scales lightbar to correct size (holy cats the lightbar is huge or the car is very small) 
            ScaleTransform3D lightbarModelShrink = new ScaleTransform3D(0.001, 0.001, 0.001);
            lightbarTransforms.Children.Add(lightbarModelShrink);

            //moves lightbar into place on the car
            TranslateTransform3D moveLightbar = new TranslateTransform3D(-0.61, (offsetToLightbarSpot / 2) - 0.03, carHeight);
            lightbarTransforms.Children.Add(moveLightbar);
            lightbarModel.Transform = lightbarTransforms;

            //adding the car and lightbar to the object that's going to be rendered on screen
            wholePackage.Children.Add(copCarModel);
            wholePackage.Children.Add(lightbarModel);

            this.ourModel = wholePackage;

        }
    }
}
