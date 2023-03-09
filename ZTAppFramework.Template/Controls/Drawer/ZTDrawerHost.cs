using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;
namespace ZTAppFramework.Template.Controls
{
    /// <summary>
    /// 抽屉
    /// </summary>
    public class ZTDrawerHost: ContentControl
    {
        static readonly Dictionary<string, ZTDrawerHost> _ZTDrawerHostDictionary;

        readonly ObservableCollection<ZTDrawerModel> _ZTDrawers;

        public ObservableCollection<ZTDrawerModel> ZTDrawerModels => _ZTDrawers;
        /// <summary>
        /// Get the name of host. The name of host can be set only one time.
        /// 获取主机的名称。主机名只能设置一次s
        /// </summary>
        public string HostName
        {
            get => GetValue(HostNameProperty);
            set
            {
                if (string.IsNullOrEmpty(HostName))
                {
                    SetValue(HostNameProperty, value);

                    if (_ZTDrawerHostDictionary.ContainsValue(this))
                    {
                        KeyValuePair<string, ZTDrawerHost>? target = null;
                        foreach (var host in _ZTDrawerHostDictionary)
                        {
                            if (ReferenceEquals(host.Value, this))
                            {
                                target = host;
                                break;
                            }
                        }

                        if (target.HasValue)
                        {
                            _ZTDrawerHostDictionary.Remove(target.Value.Key);
                        }
                    }
                }
                else
                    throw new InvalidOperationException("这个名称已经存在！");
            }
        }

        public static readonly StyledProperty<string> HostNameProperty =
            AvaloniaProperty.Register<ZTDrawerHost, string>(nameof(HostName));

        public HorizontalAlignment ZTDrawerHorizontalAlignment
        {
            get => GetValue(ZTDrawerHorizontalAlignmentProperty);
            set => SetValue(ZTDrawerHorizontalAlignmentProperty, value);
        }

        public static readonly StyledProperty<HorizontalAlignment> ZTDrawerHorizontalAlignmentProperty =
            AvaloniaProperty.Register<ZTDrawerHost, HorizontalAlignment>(nameof(ZTDrawerHorizontalAlignment), HorizontalAlignment.Left);



        /// <summary>
        /// Defines the <see cref="ZTDrawerVerticalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> ZTDrawerVerticalAlignmentProperty =
            AvaloniaProperty.Register<ZTDrawerHost, VerticalAlignment>(nameof(ZTDrawerVerticalAlignment), VerticalAlignment.Bottom);

        /// <summary>
        /// Comment
        /// </summary>
        public VerticalAlignment ZTDrawerVerticalAlignment
        {
            get { return GetValue(ZTDrawerVerticalAlignmentProperty); }
            set { SetValue(ZTDrawerVerticalAlignmentProperty, value); }
        }


        public ZTDrawerHost()
        {
            _ZTDrawers = new ObservableCollection<ZTDrawerModel>();
        }

        private static string GetFirstHostName()
        {
            if (_ZTDrawerHostDictionary is null)
                // THIS IS IMPOSSIBLE TO HAPPEN! But I kept this for any reasons.
                //这是不可能发生的！但我出于任何原因保留了这个。
                throw new NullReferenceException("ZTDrawer hosts pool is not initialized!");

            return _ZTDrawerHostDictionary.First().Key;
        }

        private static ZTDrawerHost GetHost(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var result = _ZTDrawerHostDictionary[name];
            return result;
        }

        /// <summary>
        /// Post an snackbar with message text.发送一条消息提示
        /// </summary>
        /// <param name="text">message text.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Post(string text, string? targetHost = null,
            DispatcherPriority priority = DispatcherPriority.Normal) =>
            Post(new ZTDrawerModel(text), targetHost, priority);

        /// <summary>
        /// Post an snackbar with custom content and button (only one).
        /// 张贴带有自定义内容和按钮
        /// </summary>
        /// <param name="model">.ZTDrawer data model.</param>
        /// <param name="targetHost">the .ZTDrawer host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Post(ZTDrawerModel model, string? targetHost = null,
            DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (string.IsNullOrEmpty(targetHost))
                targetHost = GetFirstHostName();

            var host = GetHost(targetHost!);

            if (host is null)
                throw new ArgumentNullException(nameof(targetHost),
                    $"The target host named \"{targetHost}\" is not exist.");

            // If duration is TimeSpan.Zero, dont expire it.
            if (model.Duration != TimeSpan.Zero)
            {
                void OnExpired(object sender, ElapsedEventArgs args)
                {
                    if (sender is not System.Timers.Timer timer)
                        return;

                    // Remove timer.
                    timer.Stop();
                    timer.Elapsed -= OnExpired;
                    timer.Dispose();

                    OnZTDrawerDurationExpired(host, model);
                }

                var timer = new System.Timers.Timer(model.Duration.TotalMilliseconds);
                timer.Elapsed += OnExpired;
                timer.Start();
            }

            Dispatcher.UIThread.Post(delegate { host.ZTDrawerModels.Add(model); }, priority);
        }

        /// <summary>
        /// Removes a .ZTDrawer manually
        /// </summary>
        /// <param name="model">snackbar data model.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Remove(ZTDrawerModel model, string targetHost = null, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (string.IsNullOrEmpty(targetHost))
                targetHost = GetFirstHostName();

            var host = GetHost(targetHost);

            if (host is null)
                throw new ArgumentNullException(nameof(targetHost), $"The target host named \"{targetHost}\" is not exist.");

            Dispatcher.UIThread.Post(delegate
            {
                host.ZTDrawerModels.Remove(model);
            }, priority);
        }

        private static void OnZTDrawerDurationExpired(ZTDrawerHost host, ZTDrawerModel model)
        {
            Dispatcher.UIThread.Post(delegate
            {
                host.ZTDrawerModels.Remove(model);
            });
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            _ZTDrawerHostDictionary.Add(HostName, this);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            _ZTDrawerHostDictionary.Remove(HostName);

            base.OnDetachedFromLogicalTree(e);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            // Initialize snackbar host
            if (HostName is null)
                throw new ArgumentNullException(nameof(HostName),
                    "The name of ZTDrawerHost is null. Please define it.");

            base.OnApplyTemplate(e);
        }
    }
}
