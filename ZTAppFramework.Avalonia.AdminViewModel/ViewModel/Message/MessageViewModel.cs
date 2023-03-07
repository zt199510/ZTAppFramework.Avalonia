using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramework.Template.Dialog;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class MessageViewModel : BasicsViewModelBase, IZTDialogAware
    {
        public MessageViewModel()
        {

        }
        public event Action<IZTDialogResult> RequestClose;

        private DelegateCommand _CloseCommand;
        public DelegateCommand CloseCommand =>
         _CloseCommand ?? (_CloseCommand = new DelegateCommand(ExecuteCloseCommand));
        private DelegateCommand _GoCommand;
        public DelegateCommand GoCommand =>
            _GoCommand ?? (_GoCommand = new DelegateCommand(ExecuteGoCommand));

        void ExecuteGoCommand()
        {
         
            RequestClose?.Invoke(null);
        }

        void ExecuteCloseCommand()
        {
            RequestClose?.Invoke(null);
        }
        public void OnDialogOpened(IZTDialogParameter parameters)
        {
           
        }
    }
}
