﻿using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Enums;

namespace ZTAppFramework.Template.Controls
{
    public class ZTProgressBar:ProgressBar
    {

        /// <summary>
        /// Defines the <see cref="ProgressBarType"/> property.
        /// </summary>
        public static readonly StyledProperty<ProgressBarEnums> ProgressBarTypeProperty =
            AvaloniaProperty.Register<ZTProgressBar, ProgressBarEnums>(nameof(ProgressBarType), ProgressBarEnums.Default);

        /// <summary>
        /// Comment
        /// </summary>
        public ProgressBarEnums ProgressBarType
        {
            get { return GetValue(ProgressBarTypeProperty); }
            set { SetValue(ProgressBarTypeProperty, value); }
        }



        /// <summary>
        /// Defines the <see cref="IsVisibleProgressValue"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsVisibleProgressValueProperty =
            AvaloniaProperty.Register<ZTProgressBar, bool>(nameof(IsVisibleProgressValue), false);

        /// <summary>
        /// Comment
        /// </summary>
        public bool IsVisibleProgressValue
        {
            get { return GetValue(IsVisibleProgressValueProperty); }
            set { SetValue(IsVisibleProgressValueProperty, value); }
        }
    }
}
