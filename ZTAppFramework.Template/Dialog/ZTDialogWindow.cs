using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Controls;
using ZTAppFramework.Template.Dialog.Interface;

namespace ZTAppFramework.Template.Dialog
{
    /// <summary>
    /// DialogWindow类
    /// </summary>
    internal class ZTDialogWindow : ContentControl, IZTDialogWindow
    {

        private ZTDialogHost Host;
        private Action<IZTDialogResult> callback;

        public ZTDialogWindow(Action<IZTDialogResult> action, ZTDialogHost host)
        {
            Host = host;
            callback = action;
            RequestCloseHandler = GetRequestCloseHandler();
        }
        public IZTDialogResult Result { get; set; }

        [Bindable(true)]

        /// <summary>
        /// Defines the <see cref="IsOpen"/> property.
        /// </summary>
        public static readonly DirectProperty<ZTDialogWindow, bool> IsOpenProperty =
            AvaloniaProperty.RegisterDirect<ZTDialogWindow, bool>(nameof(IsOpen),
                o => o.IsOpen,
                (o, v) => o.IsOpen = v);
        private bool _IsOpen;
        /// <summary>
        /// 对话框是否开启
        /// </summary>
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetAndRaise(IsOpenProperty, ref _IsOpen, value); IsOpenChanged(); }
        }
        private Action<IZTDialogResult> GetRequestCloseHandler()
        {
            Action<IZTDialogResult> requestCloseHandler = null;
            //窗体关闭的回调方法
            requestCloseHandler = (o) =>
            {
                Result = o;
                IsOpen = false;
            };
            return requestCloseHandler;
        }
        private async void IsOpenChanged()
        {
            if (!IsOpen)
            {
                await Task.Delay(90);
                Host.Items.Children.Remove(this);
            }
            else Host.Items.Children.Add(this);
        }
        public static readonly DirectProperty<ZTDialogWindow, Action<IZTDialogResult>> RequestCloseHandlerProperty =
        AvaloniaProperty.RegisterDirect<ZTDialogWindow, Action<IZTDialogResult>>(nameof(RequestCloseHandler),
            o => o.RequestCloseHandler,
                (o, v) => o.RequestCloseHandler = v);

        private Action<IZTDialogResult> _RequestCloseHandler;
        // Provide CLR accessors for the event
        public Action<IZTDialogResult> RequestCloseHandler
        {
            get => _RequestCloseHandler;
            set => SetAndRaise(RequestCloseHandlerProperty, ref _RequestCloseHandler, value);
        }
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
            this.GetDialogViewModel().RequestClose += RequestCloseHandler;
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            this.GetDialogViewModel().RequestClose -= RequestCloseHandler;
            //抓取回调后的数据并回传
            if (Result == null) Result = new ZTDialogResult() { Result = ButtonResult.None };
            callback?.Invoke(Result);
            Host.TaskCompletion?.TrySetResult(null);
        }
    }
}
