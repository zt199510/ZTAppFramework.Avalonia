using Avalonia.Controls;
using Prism.Services.Dialogs;

namespace ZTAppFramework.Avalonia.Admin.Windows
{
    public partial class DefaultWindow : Window, IDialogWindow
    {
        public DefaultWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get ; set ; }
    }
}
