using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog
{
    public interface ILayDialogAware
    {
        event Action<IZTDialogResult> RequestClose;
        void OnDialogOpened(IZTDialogParameter parameters);
    }
}
