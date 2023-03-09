using Avalonia.Controls.Presenters;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Converters
{
    internal class WrapContentIntoContentPresenterConverter : IValueConverter
    {
        public static WrapContentIntoContentPresenterConverter Instance { get; } = new WrapContentIntoContentPresenterConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IControl ? value : new ContentPresenter() { Content = value };

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
