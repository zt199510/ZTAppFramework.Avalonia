using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls.ImageCores.Behaviors.Stared
{
    public class ImgeMouseWheelBehavior : Behavior<ImageCore>
    {
        public ImgeMouseWheelBehavior(ImageCore t) : base(t)
        {
        }

        protected override void OnAttached()
        {
            AssociatedObject._svDefault.ScrollChanged += Img_ScrollChanged;
            AssociatedObject.rootGrid.PointerWheelChanged += Img_MouseWheel;
        }

        private void Img_MouseWheel(object? sender, PointerWheelEventArgs e)
        {
            if (AssociatedObject.imgWidth == 0 || AssociatedObject.imgHeight == 0) return;

            //  Do：找出ViewBox的左边距和上边距
            double x_viewbox_margrn = (AssociatedObject.rootGrid.Bounds.Width - AssociatedObject.vb.Width) / 2;
            double y_viewbox_margrn = (AssociatedObject.rootGrid.Bounds.Height - AssociatedObject.vb.Height) / 2;

            //  Do：计算边距百分比
            double x_viewbox_margrn_percent = x_viewbox_margrn / AssociatedObject.rootGrid.Bounds.Width;
            double y_viewbox_margrn_percent = y_viewbox_margrn / AssociatedObject.rootGrid.Bounds.Height;

            //  Do：获取鼠标在绘图区域Canvas的位置的百分比
            var position_canvas = e.GetPosition(AssociatedObject._CenterCanvas);

            double x_position_canvas_percent = position_canvas.X / AssociatedObject._CenterCanvas.Bounds.Width;
            double y_position_canvas_percent = position_canvas.Y / AssociatedObject._CenterCanvas.Bounds.Height;

            //  Do：获取鼠标站显示窗口GridMouse中的位置
            var position_gridMouse = e.GetPosition(AssociatedObject.grid_Mouse_drag);
            var deltaY = e.Delta.Y;
            //  Message：设置图片的缩放 
            this.ChangeScale(deltaY > 0 ? AssociatedObject.WheelScale : -AssociatedObject.WheelScale);
            //  Message：根据百分比计算放大后滚轮滚动的位置
            double xvalue = x_viewbox_margrn_percent * AssociatedObject.rootGrid.Bounds.Width + x_position_canvas_percent * AssociatedObject.vb.Width - position_gridMouse.X;
            double yvalue = y_viewbox_margrn_percent * AssociatedObject.rootGrid.Bounds.Height + y_position_canvas_percent * AssociatedObject.vb.Height - position_gridMouse.Y;

            AssociatedObject._svDefault.Offset = new Vector(xvalue, yvalue);

        }

        private void Img_ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {

        }
        double matchscale;
        bool ChangeScale(double value)
        {
            matchscale = AssociatedObject.GetFullScale();

            AssociatedObject.Scale = AssociatedObject.Scale + value;

            AssociatedObject.IsMaxScaled = !(AssociatedObject.Scale < AssociatedObject.MaxScale);

            AssociatedObject.IsMinScaled = !(AssociatedObject.Scale > matchscale);

            //  AssociatedObject.OnNoticeMessaged(((int)(AssociatedObject.Scale * 100)).ToString() + "%");

            if (AssociatedObject.Scale < matchscale)
            {
                AssociatedObject.Scale = matchscale;

                //   AssociatedObject.OnNoticeMessaged("已最小");

                AssociatedObject._svDefault.IsEnabled = false;
            }

            if (AssociatedObject.Scale > AssociatedObject.MaxScale)
            {
                AssociatedObject.Scale = AssociatedObject.MaxScale;

                AssociatedObject._svDefault.IsEnabled = false;

                //   AssociatedObject.OnNoticeMessaged("已最大");
            }
            AssociatedObject._svDefault.IsEnabled = true;
          //  AssociatedObject.RefreshImageByScale();

            return true;
        }
        protected override void OnDetaching()
        {
            AssociatedObject._svDefault.ScrollChanged -= Img_ScrollChanged;
            AssociatedObject.rootGrid.PointerWheelChanged -= Img_MouseWheel;
            //  AssociatedObject.AttachedToVisualTree -= Loaded;
        }
    }
}
