using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Dialog
{
   public interface  IZTDialogService:IZTDialog
    {
        /// <summary>
        /// 注入弹窗试图
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        void RegisterDialog<TView>(string dialogName);
        /// <summary>
        /// 注入弹窗试图
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <typeparam name="TViewModel">需要注入的ViewModel类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        void RegisterDialog<TView, TViewModel>(string dialogName) where TViewModel : IZTDialogAware;
    }
}
