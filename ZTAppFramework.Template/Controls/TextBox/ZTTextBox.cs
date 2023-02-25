using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZTAppFramework.Template.Enums;

namespace ZTAppFramework.Template.Controls
{
    public class ZTTextBox : TextBox
    {

        private ZTButton PRAT_CloseButton = null;
        /// <summary>
        /// 是否聚焦
        /// </summary>
        public bool IsFocus
        {
            get { return GetValue(IsFocusProperty); }
            set { SetValue(IsFocusProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsFocusProperty =
       AvaloniaProperty.Register<ZTTextBox, bool>(nameof(IsFocus));


        /// <summary>
        /// Defines the <see cref="InputType"/> property.
        /// </summary>
        public static readonly StyledProperty<InputType> InputTypeProperty =
            AvaloniaProperty.Register<ZTTextBox, InputType>(nameof(InputType), InputType.Default);

        /// <summary>
        /// 输入类型
        /// </summary>
        public InputType InputType
        {
            get { return GetValue(InputTypeProperty); }
            set { SetValue(InputTypeProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            PRAT_CloseButton = e.NameScope.Find<ZTButton>("PRAT_CloseButton");

            if (PRAT_CloseButton != null) PRAT_CloseButton.Click -= PRAT_CloseButton_Click;
            if (PRAT_CloseButton != null) PRAT_CloseButton.Click += PRAT_CloseButton_Click;
        }

        private void PRAT_CloseButton_Click(object? sender, RoutedEventArgs e)
        {
            Text = "";
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            switch (InputType)
            {
                case InputType.Default:
                    break;
                case InputType.Number:
                    e.Handled = new Regex(@"[^0-9|\-|\.]").IsMatch(e.Text);
                    break;
                case InputType.Phone:
                    e.Handled = IsPhone(e);
                    break;
                case InputType.User:
                    break;
                default:
                    break;
            }
            base.OnTextInput(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        /// 检验手机号
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsPhone(TextInputEventArgs e)
        {
            if ((e.Source as TextBox).Text?.ToCharArray().Length > 10) return true;
            return Regex.IsMatch(e.Text, @"[^(1)\d{10}$]");
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsFocus = false;
        }


        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            if (IsFocus) Focus();
        }
    }
}
