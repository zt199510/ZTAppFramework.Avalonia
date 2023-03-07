using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.Stared.ViewModels
{
    public abstract class DialogViewModelBase : BasicsViewModelBase, IDialogAware
    {
        public string Title { get; set; }

        public event Action<IDialogResult>? RequestClose;
        #region Command
        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }
        #endregion
        public DialogViewModelBase()
        {
            SaveCommand = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public abstract void Cancel();
        public abstract void OnSave();
        public bool CanCloseDialog()
        {
            return true;
        }
        public virtual void OnDialogClosed(ButtonResult result)
        {
            DialogResult dialogResult = new DialogResult(result);
            RaiseRequestClose(dialogResult);
        }

        public virtual void OnDialogClosed()
        {

        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {

        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}
