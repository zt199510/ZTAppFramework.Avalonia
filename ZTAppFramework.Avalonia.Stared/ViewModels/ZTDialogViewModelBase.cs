using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Dialog;

namespace ZTAppFramework.Avalonia.Stared.ViewModels
{
    public abstract class ZTDialogViewModelBase : BasicsViewModelBase, IZTDialogAware
    {
        public string Title { get; set; }

        public event Action<IZTDialogResult> RequestClose;
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public ZTDialogViewModelBase()
        {
            SaveCommand = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public abstract void Cancel();
        public abstract void OnSave();


        public virtual void OnDialogOpened(IZTDialogParameter parameters)
        {
           
        }

        public virtual void OnDialogClosed(ButtonResult result)
        {
            ZTDialogResult dialogResult = new ZTDialogResult(result);
            RaiseRequestClose(dialogResult);
        }

        public virtual void RaiseRequestClose(IZTDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}
