using Prism.Services.Dialogs;

namespace ZTAppFramework.Template.Dialog
{
    public interface IZTDialogResult
    {
        IZTDialogParameter Parameters { get; set; }
        ButtonResult Result { get; set; }
    }
}