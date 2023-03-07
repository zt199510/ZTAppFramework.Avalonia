using DryIoc.Messages;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class PrismMessageViewModel : DialogViewModelBase
    {
        #region UI
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        #endregion
        public override void Cancel()
        {
            OnDialogClosed(ButtonResult.No);
        }

        public override void OnSave()
        {
            OnDialogClosed(ButtonResult.Yes);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);

            Title = parameters.GetValue<string>("Title");
            Message = parameters.GetValue<string>("Message");
        }
    }
}
