using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog.Interface
{
    internal static class IZTDialogWindowExtensions
    {
        internal static ILayDialogAware GetDialogViewModel(this IZTDialogWindow dialog)
        {
            return (ILayDialogAware)dialog.DataContext;
        }
        internal static ILayDialogAware GetDialogView(this IZTDialogWindow dialog)
        {
            return (ILayDialogAware)dialog.Content;
        }
    }
}
