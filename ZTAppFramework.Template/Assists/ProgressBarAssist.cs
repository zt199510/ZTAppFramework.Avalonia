using Avalonia.Controls;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Assists
{
    public static class ProgressBarAssist
    {
        public static readonly AvaloniaProperty<object> CircularInnerContentProperty = AvaloniaProperty.RegisterAttached<ProgressBar, object>(
            "CircularInnerContent", typeof(ProgressBarAssist), null, true);

        public static object GetCircularInnerContent(AvaloniaObject element)
        {
            return (object)element.GetValue(CircularInnerContentProperty);
        }

        public static void SetCircularInnerContent(AvaloniaObject element, object value)
        {
            element.SetValue(CircularInnerContentProperty, value);
        }
    }
}
