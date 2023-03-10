using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using ZTAppFramework.Template.Controls.ImageCores.Behaviors.Stared;

namespace TestAppFramework
{
    public partial class MainWindow : Window
    {
        private const double ZoomFactor = 0.1;
        private Point zoomPosition;

        public MainWindow()
        {
            InitializeComponent();

            sd.ImageSource = new Bitmap(@"C:\Users\10335\Desktop\3af11f8f631e48ddb0eba5d23534e578.jpg");
            sd.AddBehaviors(new ImgeMouseWheelBehavior(sd));

         
        }

      
    }
}
