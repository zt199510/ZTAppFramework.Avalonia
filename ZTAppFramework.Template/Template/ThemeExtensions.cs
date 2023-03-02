using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Template.Template
{
    public static class ThemeExtensions
    {

        public static T LocateNeumorphismTheme<T>(this Application application) 
        {
            var theme = application.Styles.FirstOrDefault(style => style is T);
            if (theme == null)
            {
                throw new InvalidOperationException($"Cannot locate {nameof(T)} in Avalonia application styles. Be sure that you include NeumorphismTheme in your App.xaml in Application.Styles section");
            }

            return (T)theme;
        }

    }
}
