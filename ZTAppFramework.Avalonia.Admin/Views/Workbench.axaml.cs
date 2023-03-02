using Avalonia;
using Avalonia.Controls;
using Prism.Mvvm;
using ZTAppFramework.Avalonia.AdminViewModel.ViewModel;

namespace ZTAppFramework.Avalonia.Admin.Views
{
    public partial class Workbench : UserControl
    {

     
        public Workbench()
        {
            InitializeComponent();
            var grid = this.FindControl<Grid>("OneGrid");

            grid.GetObservable(Grid.BoundsProperty).Subscribe(bounds =>
            {
                (this.DataContext as WorkbenchViewModel).ColumnWidth = bounds.Width;
        
            });
        }
    }
}
