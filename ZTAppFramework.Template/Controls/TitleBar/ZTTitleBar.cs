using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Enums;

namespace ZTAppFramework.Template.Controls
{
    /// <summary>
    /// 创建标题栏
    /// </summary>
    // PseudoClasses属性通常用于样式表中，它们允许开发人员为不同的控件状态定义不同的样式
    // [PseudoClasses可以用于以下状态]：
    //:pointerover：当鼠标指针悬停在控件上方时
    //:pressed：当控件被按下时
    //:disabled：当控件被禁用时
    //:readonly：当控件为只读时
    //:focused：当控件拥有焦点时
    //:unchecked：当控件未被选中时（例如复选框或单选按钮）
    //:checked：当控件被选中时（例如复选框或单选按钮）
    //:indeterminate：当控件处于不确定状态时（例如复选框）
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public class ZTTitleBar : ContentControl
    {
        private Panel PART_HeaderBody = null;
        private Panel PART_WindowButtonGrid = null;
        private CompositeDisposable _disposables;



        /// <summary>
        /// Defines the <see cref="TitleBarType"/> property.
        /// </summary>
        public static readonly StyledProperty<TitleBarEnums> TitleBarTypeProperty =
            AvaloniaProperty.Register<ZTTitleBar, TitleBarEnums>(nameof(TitleBarType), TitleBarEnums.Default);

        /// <summary>
        /// Comment
        /// </summary>
        public TitleBarEnums TitleBarType
        {
            get { return GetValue(TitleBarTypeProperty); }
            set { SetValue(TitleBarTypeProperty, value); }
        }
        /// <summary>
        /// 顶部标题栏文字颜色
        /// </summary>
        public IBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBrush"/>属性
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderForegroundProperty =
       AvaloniaProperty.Register<ZTTitleBar, IBrush>(nameof(HeaderForeground), null);

        /// <summary>
        /// Defines the <see cref="HeaderVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> HeaderVisibleProperty =
            AvaloniaProperty.Register<Control, bool>(nameof(HeaderVisible), true);

        /// <summary>
        /// 是否显示顶部状态栏
        /// </summary>
        public bool HeaderVisible
        {
            get { return GetValue(HeaderVisibleProperty); }
            set { SetValue(HeaderVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> HeaderHeightProperty =
            AvaloniaProperty.Register<ZTTitleBar, double>(nameof(HeaderHeight));

        /// <summary>
        /// 顶部状态栏高度
        /// </summary>
        public double HeaderHeight
        {
            get { return GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="HeaderContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> HeaderContentProperty =
            AvaloniaProperty.Register<ZTTitleBar, object>(nameof(HeaderContent));

        /// <summary>
        /// 顶部类容
        /// </summary>
        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }


        /// <summary>
        /// 鼠标按下拖动判断
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (PART_HeaderBody != null && e.Source == PART_HeaderBody)
            {
                if (VisualRoot is Window window)
                {
                    if (window.WindowState == WindowState.FullScreen)
                        return;
                    window.BeginMoveDrag(e);
                }
            }
        }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBrush?"/>属性
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
       AvaloniaProperty.Register<ZTTitleBar, IBrush>(nameof(HeaderBackground), null);

        //当一个控件在视觉树中被创建或者其模板被更改时触发
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            if (VisualRoot is Window window)
            {

                window.CanResize = false;
                //创建一个新的 CompositeDisposable 对象，并添加一个订阅者，用于监听 window 的 WindowStateProperty 属性的变化。当属性发生变化时，执行一个委托函数，根据不同的窗口状态，设置控件的伪类，使用 CompositeDisposable 可以方便地管理多个可释放的对象，比如订阅者或者定时器。当你不再需要这些对象时，你可以一次性地释放它们，避免内存泄漏或者资源浪费。当控件被移除时直接调用CompositeDisposable.Dispose() 帮助释放对象
                _disposables = new CompositeDisposable
                {
                      window.GetObservable(Window.WindowStateProperty)
                        .Subscribe(delegate(WindowState x)
                        {
                            //设置控件状态和触发方法
                            PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                            PseudoClasses.Set(":normal", x == WindowState.Normal);
                            PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                            PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                        }),

                    this.WhenAnyValue(x => x.TitleBarType).Subscribe(x =>
                    {
                        if(x!=TitleBarEnums.Default)
                            window.CanResize=false;

                        else
                            window.CanResize=true;
                    })
                   
                };

                
            }

            PART_HeaderBody = e.NameScope.Find<Panel>("PART_HeaderBody");
            PART_WindowButtonGrid = e.NameScope.Find<Panel>("PART_WindowButtonGrid");
            if (PART_HeaderBody != null) AddOreRemoveHandler(true);
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped += PART_HeaderBody_DoubleTapped;
        }


        private void AddOreRemoveHandler(bool isAdd)
        {
            var items = PART_WindowButtonGrid.Children.ToList();
            foreach (var item in items)
            {
                if (item is ZTButton button)
                {
                    if (button.CommandParameter is WindowButtonType)
                    {
                        if (isAdd) item.AddHandler(Button.ClickEvent, UpdateWindowState);
                        else item.RemoveHandler(Button.ClickEvent, UpdateWindowState);
                    }
                }
            }
        }
        private void UpdateWindowState(object sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is ZTButton button)
            {
                if (button.CommandParameter is WindowButtonType type)
                {
                    if (VisualRoot is Window window)
                    {
                        switch (type)
                        {
                            case WindowButtonType.FullScreen:
                                if (window.WindowState == WindowState.FullScreen) window.WindowState = WindowState.Normal;
                                else window.WindowState = WindowState.FullScreen;
                                break;
                            case WindowButtonType.Minimized:
                                window.WindowState = WindowState.Minimized;
                                break;
                            case WindowButtonType.MaximizedOrNormal:
                                if (window.WindowState == WindowState.Maximized) window.WindowState = WindowState.Normal;
                                else window.WindowState = WindowState.Maximized;
                                break;
                            case WindowButtonType.Closed:
                                window.Close();
                                Environment.Exit(0);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 双击时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_HeaderBody_DoubleTapped(object sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (e.Source != PART_HeaderBody) return;
            if (TitleBarType != TitleBarEnums.Default) return;
            if (VisualRoot is Window window)
            {
                if (window.WindowState == WindowState.Normal) window.WindowState = WindowState.Maximized;
                else window.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// 控件从视觉树中被移除时执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            _disposables?.Dispose();//释放
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) AddOreRemoveHandler(false);
        }
    }
}
