using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog
{
    /// <summary>
    /// 返回值类
    /// </summary>
    public class ZTDialogResult : IZTDialogResult
    {
        public ZTDialogResult()
        {
            Result = ButtonResult.None;
        }
        public ZTDialogResult(ButtonResult result)
        {
            this.Result = result;
        }
        public ZTDialogResult(ButtonResult result, IZTDialogParameter parameters)
        {
            Result = result;
            Parameters = parameters;
        }
        public ButtonResult Result { get; set; }

        public IZTDialogParameter Parameters { get; set; }

    }
}
