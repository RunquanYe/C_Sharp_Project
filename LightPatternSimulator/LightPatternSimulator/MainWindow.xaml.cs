using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LightPatternSimulator.ViewModels;
using Xceed.Wpf.Toolkit;

namespace LightPatternSimulator
{
    public partial class MainWindow : Window
    {
        public LightbarViewModelContainer LightbarViewModelContainer { get; set; }

    public MainWindow()
        {
            InitializeComponent();

            LightbarViewModelContainer = new LightbarViewModelContainer(new LightbarViewModel());

            DataContext = LightbarViewModelContainer;
        }


        private void show3D(object sender, RoutedEventArgs e)
        {
            DataContext = new display3DView(new LightbarViewModel());
        }

    }


}