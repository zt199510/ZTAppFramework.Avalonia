﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{

    /// <summary>
    /// 对话框容器
    /// </summary>
    public class ZTDialogHost : ContentControl
    {
        internal TaskCompletionSource<object> TaskCompletion;
        /// <summary>
        /// 存储对话视图容器
        /// </summary>
        internal Grid Items { get; private set; }

        /// <summary>
        /// 唯一标识ID
        /// </summary>
        internal string GUID
        {
            get { return GetValue(GUIDProperty); }
            set { SetValue(GUIDProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        internal static readonly StyledProperty<string> GUIDProperty =
       AvaloniaProperty.Register<ZTDialogHost, string>(nameof(GUID));

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            Items = e.NameScope.Find<Grid>("PART_Items");
        }
    }
}
