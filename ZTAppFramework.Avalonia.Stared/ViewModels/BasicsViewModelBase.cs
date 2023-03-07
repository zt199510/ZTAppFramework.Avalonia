using AutoMapper;
using Prism.Mvvm;

using System.Text;
using System.Xml.Serialization;
using Prism.Ioc;
using ZTAppFramework.Avalonia.Stared.Validations;
using FluentValidation.Results;
using ZTAppFramework.Template.Dialog;
using Prism.Services.Dialogs;

namespace ZTAppFramework.Avalonia.Stared.ViewModels
{
    /// <summary>
    /// 基础
    /// </summary>
    public class BasicsViewModelBase: BindableBase
    {
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsNotBusy));
            }
        }
        public bool IsNotBusy => !IsBusy;

        private readonly IMapper mapper;
        protected readonly GlobalValidator validator;
        private IZTDialogService ZTDialog;
        private IDialogService _DialogService;

        public BasicsViewModelBase()
        {
            mapper = ContainerLocator.Container.Resolve<IMapper>();
            validator = ContainerLocator.Container.Resolve<GlobalValidator>();
            ZTDialog = ContainerLocator.Container.Resolve<IZTDialogService>();
            _DialogService= ContainerLocator.Container.Resolve<IDialogService>(); 
        }
        public void Show(IZTDialogParameter parameter, Action<IZTDialogResult> callback, string Token)
        {
            ZTDialog.Show(AppPages.MessagePage, parameter,callback, Token);
        }
     
        public void Show(string Title,string Message)
        {
            DialogParameters Param = new DialogParameters();
            Param.Add("Title", Title);
            Param.Add("Message", Message);
            _DialogService.ShowDialog(AppPages.MessagePage, Param, r => { }, "DefaultWindow");
        }
        public void ShowDialog(string Title, string Message,Action<IDialogResult> callback)
        {
            DialogParameters Param = new DialogParameters();
            Param.Add("Title", Title);
            Param.Add("Message", Message);
            _DialogService.ShowDialog(AppPages.MessagePage, Param, callback, "DefaultWindow");
        }
        /// <summary>
        /// 实体映射方法
        /// </summary>
        /// <typeparam name="T">最终类型</typeparam>
        /// <param name="model">映射实体</param>
        /// <returns></returns>
        public T Map<T>(object model) => mapper.Map<T>(model);

        public virtual async Task SetBusyAsync(Func<Task> func)
        {
            IsBusy = true;
            try
            {
                await func();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region 深拷贝
        public T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
        #endregion

        #region 实体验证

        /// <summary>
        /// 实体验证器方法
        /// </summary>
        /// <typeparam name="T">验证结果</typeparam>
        /// <param name="model">验证实体</param>
        /// <returns></returns>
        public virtual ValidationResult Verify<T>(T model, bool ShowError = true)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid && ShowError)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in validationResult.Errors)
                {
                    stringBuilder.AppendLine(item.ErrorMessage);
                }
            }
            return validationResult;
        }
        #endregion


    }
}
