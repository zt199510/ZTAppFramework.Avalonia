using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{
    /// <summary>
    /// 抽屉
    /// </summary>
    [PseudoClasses(":open", ":closed", ":left", ":right")]
    public class ZTNavigationDrawer: ContentControl
    {

        public static readonly StyledProperty<object> LeftDrawerContentProperty =
    AvaloniaProperty.Register<ZTNavigationDrawer, object>(nameof(LeftDrawerContent));

        public static readonly StyledProperty<IDataTemplate> LeftDrawerContentTemplateProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, IDataTemplate>(nameof(LeftDrawerContentTemplate));

        public static readonly StyledProperty<bool> LeftDrawerOpenedProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, bool>(nameof(LeftDrawerOpened));

        public static readonly StyledProperty<double> LeftDrawerWidthProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, double>(nameof(LeftDrawerWidth));


        public static readonly StyledProperty<object> RightDrawerContentProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, object>(nameof(RightDrawerContent));

        public static readonly StyledProperty<IDataTemplate> RightDrawerContentTemplateProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, IDataTemplate>(nameof(RightDrawerContentTemplate));

        public static readonly StyledProperty<bool> RightDrawerOpenedProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, bool>(nameof(RightDrawerOpened));

        public static readonly StyledProperty<double> RightDrawerWidthProperty =
            AvaloniaProperty.Register<ZTNavigationDrawer, double>(nameof(RightDrawerWidth));

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(LeftDrawerContentTemplate))]
        public object LeftDrawerContent
        {
            get => GetValue(LeftDrawerContentProperty);
            set => SetValue(LeftDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate LeftDrawerContentTemplate
        {
            get => GetValue(LeftDrawerContentTemplateProperty);
            set => SetValue(LeftDrawerContentTemplateProperty, value);
        }

        public bool LeftDrawerOpened
        {
            get => GetValue(LeftDrawerOpenedProperty);
            set => SetValue(LeftDrawerOpenedProperty, value);
        }

        /// <summary>
        /// 左抽屉宽度
        /// </summary>
        public double LeftDrawerWidth
        {
            get => GetValue(LeftDrawerWidthProperty);
            set => SetValue(LeftDrawerWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(RightDrawerContentTemplate))]
        public object RightDrawerContent
        {
            get => GetValue(RightDrawerContentProperty);
            set => SetValue(RightDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate RightDrawerContentTemplate
        {
            get => GetValue(RightDrawerContentTemplateProperty);
            set => SetValue(RightDrawerContentTemplateProperty, value);
        }

        public bool RightDrawerOpened
        {
            get => GetValue(RightDrawerOpenedProperty);
            set => SetValue(RightDrawerOpenedProperty, value);
        }
        /// <summary>
        /// 右抽屉宽度
        /// </summary>
        public double RightDrawerWidth
        {
            get => GetValue(RightDrawerWidthProperty);
            set => SetValue(RightDrawerWidthProperty, value);
        }

        private Border? PART_Scrim;
        static ZTNavigationDrawer()
        {
            LeftDrawerOpenedProperty.Changed.AddClassHandler<ZTNavigationDrawer>(OnDrawerOpenedChanged);
            RightDrawerOpenedProperty.Changed.AddClassHandler<ZTNavigationDrawer>(OnDrawerOpenedChanged);
        }

        private static void OnDrawerOpenedChanged(ZTNavigationDrawer drawer, AvaloniaPropertyChangedEventArgs args)
        {
            drawer.UpdatePseudoClasses();
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            if (e.NameScope.Find("PART_Scrim") is Border border)
            {
                PART_Scrim = border;

                PART_Scrim.PointerPressed += PART_Scrim_Pressed;
            }

            base.OnApplyTemplate(e);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (PART_Scrim != null)
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (PART_Scrim != null)
                PART_Scrim.PointerPressed -= PART_Scrim_Pressed;

            base.OnDetachedFromVisualTree(e);
        }

        private void PART_Scrim_Pressed(object sender, RoutedEventArgs e)
        {
            LeftDrawerOpened = false;
            RightDrawerOpened = false;
        }

        private void UpdatePseudoClasses()
        {
            var open = LeftDrawerOpened || RightDrawerOpened;
            PseudoClasses.Add(open ? ":open" : ":closed");
            PseudoClasses.Remove(!open ? ":open" : ":closed");

            PseudoClasses.Set(":left", LeftDrawerOpened);
            PseudoClasses.Set(":right", RightDrawerOpened);
        }
    }
}
