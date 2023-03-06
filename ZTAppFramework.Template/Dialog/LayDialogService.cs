using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog
{
    public class ZTDialogService : ZTDialog, IZTDialogService
    {
        public void RegisterDialog<TView>(string dialogName)
        {
            try
            {
                if (DialogViews.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                DialogViews.Add(dialogName, typeof(TView));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegisterDialog<TView, TViewModel>(string dialogName) where TViewModel : IZTDialogAware
        {
            try
            {
                if (DialogViews.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                if (DialogViewModels.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图ViewModel多次注入");
                DialogViews.Add(dialogName, typeof(TView));
                DialogViewModels.Add(dialogName, typeof(TViewModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
