using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using ZTAppFramework.Template.Controls.ImageCores.Behaviors.Stared;

namespace ZTAppFramework.Avalonia.Admin.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            // PART_ImageCenter.Source = new Bitmap(@"C:\Users\10335\Desktop\3af11f8f631e48ddb0eba5d23534e578.jpg");

            ddd.ImageSource = new Bitmap(@"C:\Users\10335\Desktop\3af11f8f631e48ddb0eba5d23534e578.jpg");

            ddd.AddBehaviors(new ImgeMouseWheelBehavior(ddd));

        }

       
    }
}
