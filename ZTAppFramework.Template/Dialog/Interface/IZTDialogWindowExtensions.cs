using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog.Interface
{
    internal static class IZTDialogWindowExtensions
    {
        internal static IZTDialogAware GetDialogViewModel(this IZTDialogWindow dialog)
        {
            return (IZTDialogAware)dialog.DataContext;
        }
        internal static IZTDialogAware GetDialogView(this IZTDialogWindow dialog)
        {
            return (IZTDialogAware)dialog.Content;
        }
    }
}
