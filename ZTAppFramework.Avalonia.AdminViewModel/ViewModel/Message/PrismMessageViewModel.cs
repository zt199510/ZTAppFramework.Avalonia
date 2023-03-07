using DryIoc.Messages;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.Stared.Enums;
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

        private bool _CanceVisibility;
        public bool CanceVisibility
        {
            get { return _CanceVisibility; }
            set { SetProperty(ref _CanceVisibility, value); }
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

        void UpdateMessageBtn()
        {

        }
       
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>("Title");
            Message = parameters.GetValue<string>("Message");
            var MessgaeType = parameters.GetValue<MessageEnums>("MessageEnums");
            switch (MessgaeType)
            {
                case MessageEnums.Yes:
                    CanceVisibility = false;
                    break;
                case MessageEnums.YesOrNO:
                    CanceVisibility = true;
                    break;
                default:
                    CanceVisibility = false;
                    break;
            }
        }
    }
}
