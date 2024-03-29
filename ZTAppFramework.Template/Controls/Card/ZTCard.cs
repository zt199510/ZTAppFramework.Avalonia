﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{

    /// <summary>
    /// 卡片
    /// </summary>
    public class ZTCard: ContentControl
    {

        static ZTCard()
        {

        }

        private static void UpdateBoxShadow(AvaloniaPropertyChangedEventArgs obj)
        {
            if (obj.Sender is ZTCard card) card.UpdateBoxShadow();
        }

        /// <summary>
        /// 修改阴影效果
        /// </summary>
        private void UpdateBoxShadow()
        {
            BoxShadow = new BoxShadows(new BoxShadow()
            {
                Color = this.ShadowColor.Color,
                Blur = Blur,
                OffsetY = OffsetY,
                OffsetX = OffsetX,
                Spread = Spread,
                IsInset = IsInset
            });
        }

        /// <summary>
        /// Defines the <see cref="BoxShadow"/> property.
        /// </summary>
        internal static readonly StyledProperty<BoxShadows> BoxShadowProperty =
            AvaloniaProperty.Register<ZTCard, BoxShadows>(nameof(BoxShadow));

        /// <summary>
        /// BoxShadow
        /// </summary>
        internal BoxShadows BoxShadow
        {
            get { return GetValue(BoxShadowProperty); }
            private set { SetValue(BoxShadowProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IsInset"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsInsetProperty =
            AvaloniaProperty.Register<ZTCard, bool>(nameof(IsInset));

        /// <summary>
        /// IsInset
        /// </summary>
        public bool IsInset
        {
            get { return GetValue(IsInsetProperty); }
            set { SetValue(IsInsetProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ShadowColor"/> property.
        /// </summary>
        public static readonly StyledProperty<SolidColorBrush> ShadowColorProperty =
            AvaloniaProperty.Register<ZTCard, SolidColorBrush>(nameof(ShadowColor), new SolidColorBrush(Color.Parse("#d2d2d2")));

        /// <summary>
        /// 阴影颜色
        /// </summary>
        public SolidColorBrush ShadowColor
        {
            get { return GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Blur"/> property 
        /// </summary>
        public static readonly StyledProperty<double> BlurProperty =
            AvaloniaProperty.Register<ZTCard, double>(nameof(Blur));

        /// <summary>
        /// 模糊程度
        /// </summary>
        public double Blur
        {
            get { return GetValue(BlurProperty); }
            set { SetValue(BlurProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Spread"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SpreadProperty =
            AvaloniaProperty.Register<ZTCard, double>(nameof(Spread));

        /// <summary>
        /// 扩散范围
        /// </summary>
        public double Spread
        {
            get { return GetValue(SpreadProperty); }
            set { SetValue(SpreadProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="OffsetX"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetXProperty =
            AvaloniaProperty.Register<ZTCard, double>(nameof(OffsetX));

        /// <summary>
        /// 水平方向位移
        /// </summary>
        public double OffsetX
        {
            get { return GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="OffsetY"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetYProperty =
            AvaloniaProperty.Register<ZTCard, double>(nameof(OffsetY));

        /// <summary>
        /// 垂直方向位移
        /// </summary>
        public double OffsetY
        {
            get { return GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }
    }
}
