using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Controls
{
    public class ZTDrawerModel
    {
        private object _content;
        public object Content => _content;

        private Orientation _orientation;
        public Orientation Orientation => _orientation;

        // Still not sure I should use control instead of method call
        // because I want more flexible button property on snackbar but
        // that means we cant dismiss snackbar after pressed button.
        //仍然不确定我应该使用控件而不是方法调用
        //因为我希望snackbar上的按钮属性更灵活，
        //但这意味着我们不能在按下按钮后关闭snackbar。
        private object _button;
        public object Button => _button;

        // Setting duration to TimeSpan.Zero, will make it stay forever/til you manually delete it 
        // 将持续时间设置为TimeSpan.Zero将使其永久保留，直到手动删除
        private TimeSpan _duration = TimeSpan.FromSeconds(5);
        public TimeSpan Duration => _duration;

        public ZTDrawerModel(object content, Orientation orientation = Orientation.Horizontal)
        {
            _content = content;
            _orientation = orientation;
        }

        public ZTDrawerModel(object content, TimeSpan duration, Orientation orientation = Orientation.Horizontal) : this(content, orientation)
        {
            _duration = duration;
        }
    }
}
