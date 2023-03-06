using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using ImTools;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Enums;
using Avalonia.Collections;
using DynamicData;
using Avalonia.Interactivity;
using DynamicData.Binding;

using System.Reflection;

namespace ZTAppFramework.Template.Controls
{
    public class ZTComboBox : ComboBox
    {
        private ZTTextBox PART_PlaceholderTextbox;
        private CompositeDisposable _disposables;


        /// <summary>
        /// Defines the <see cref="ComboBoxType"/> property.下拉框样式
        /// </summary>
        public static readonly StyledProperty<ComboBoxEnums> ComboBoxTypeProperty =
            AvaloniaProperty.Register<ZTComboBox, ComboBoxEnums>(nameof(ComboBoxType), ComboBoxEnums.Default);

        /// <summary>
        /// Comment
        /// </summary>
        public ComboBoxEnums ComboBoxType
        {
            get { return GetValue(ComboBoxTypeProperty); }
            set { SetValue(ComboBoxTypeProperty, value); }
        }



        /// <summary>
        /// Defines the <see cref="TextContent"/> property.
        /// </summary>
        public static readonly StyledProperty<string> TextContentProperty =
            AvaloniaProperty.Register<ZTComboBox, string>(nameof(TextContent), "");

        /// <summary>
        /// Comment
        /// </summary>
        public string TextContent
        {
            get { return GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }



        /// <summary>
        /// Defines the <see cref="DisplayMemberPath"/> property.
        /// </summary>
        public static readonly StyledProperty<string> DisplayMemberPathProperty =
            AvaloniaProperty.Register<ZTComboBox, string>(nameof(DisplayMemberPath), "");

        /// <summary>
        /// Comment
        /// </summary>
        public string DisplayMemberPath
        {
            get { return GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        /// <summary>
        /// 重写指定下拉选项下拉控件，替换为指定下拉控件
        /// </summary>
        /// <returns></returns>
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<ZTComboBoxItem>(
                this,
                ZTComboBoxItem.ContentProperty,
                ZTComboBoxItem.ContentTemplateProperty);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            if (ComboBoxType == ComboBoxEnums.Input)
            {
                PART_PlaceholderTextbox = e.NameScope.Find<ZTTextBox>("PART_PlaceholderTextbox");
            }

            _disposables = new CompositeDisposable()
            {
                this.WhenAnyValue(x=>x.IsDropDownOpen).Subscribe(IsDropDownOpen=>{
                   if (ComboBoxType == ComboBoxEnums.Input)
                    {
                        if (IsDropDownOpen)
                             PART_PlaceholderTextbox.Focus();
                    }
                }),

                this.WhenAnyValue(x => x.PART_PlaceholderTextbox.Text).Where(x =>!string.IsNullOrEmpty(x)).Subscribe(item =>
                {
                    if(item!=GetDisplayName())
                        this.SelectedIndex=-1;
                    if (string.IsNullOrEmpty(item))
                             IsDropDownOpen = true;

                }),
               this.WhenAnyValue(x => x.SelectionBoxItem).Where(x =>x!=null).Subscribe(item =>
                {
                   TextContent=GetDisplayName();
                }),
              

            };
            PART_PlaceholderTextbox.LostFocus -= PART_PlaceholderTextbox_LostFocus;
            PART_PlaceholderTextbox.LostFocus += PART_PlaceholderTextbox_LostFocus;
        }

        private void PART_PlaceholderTextbox_LostFocus(object? sender, RoutedEventArgs e)
        {
            IsDropDownOpen = false;
            this.Focusable = false;
        }

        string GetDisplayName()
        {
            if (SelectionBoxItem == null) return "";
            if (string.IsNullOrEmpty(DisplayMemberPath))
            {
                return SelectionBoxItem?.ToString();
            }
            else
            {
                Type type = SelectionBoxItem.GetType();
                PropertyInfo ValueProp = type.GetProperty(DisplayMemberPath);
                if (ValueProp != null)
                {
                    object nameValue = ValueProp.GetValue(SelectionBoxItem);
                    return nameValue?.ToString();
                }
            }
            return "";
        }



        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            PART_PlaceholderTextbox.LostFocus -= PART_PlaceholderTextbox_LostFocus;
            _disposables?.Dispose();//释放

        }
    }
}
