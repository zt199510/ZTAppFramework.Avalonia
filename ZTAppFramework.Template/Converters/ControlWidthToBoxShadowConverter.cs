﻿using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Template.Template;

namespace ZTAppFramework.Template.Converters
{
    public class ControlWidthToBoxShadowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = new BoxShadows();
            bool inset = parameter != null && parameter.Equals("1");
            bool isFixedInset = parameter != null && parameter.Equals("2");
            bool isFixedOutset = parameter != null && parameter.Equals("3");
            bool insetAndOutset = parameter != null && parameter.Equals("4");
            bool smallerOutset = parameter != null && parameter.Equals("5");

           // var theme = Application.Current!.LocateNeumorphismTheme<NeumorphismTheme>();

            BoxShadow main = new BoxShadow();
            BoxShadow rest1 = new BoxShadow();

            List<BoxShadow> rests = new List<BoxShadow>();

            if (isFixedInset)
            {
                //-20 -20 60 #CCFFFFFF,20 20 60 #33000000
                main.IsInset = true;
                main.OffsetX = -3.3;
                main.OffsetY = -3.3;
                main.Blur = 10;
                main.Color =Colors.Gainsboro;

                rest1.IsInset = true;
                rest1.OffsetX = 3.3;
                rest1.OffsetY = 3.3;
                rest1.Blur = 10;
                rest1.Color = Colors.Gainsboro;
                rests.Add(rest1);
            }
            else if (isFixedOutset)
            {
                //-20 -20 60 #CCFFFFFF,20 20 60 #33000000
                main.IsInset = false;
                main.OffsetX = -3.3;
                main.OffsetY = -3.3;
                main.Blur = 10;
                main.Color = Colors.Gainsboro;

                rest1.IsInset = false;
                rest1.OffsetX = 3.3;
                rest1.OffsetY = 3.3;
                rest1.Blur = 10;
                rest1.Color = Colors.Gainsboro;
                rests.Add(rest1);
            }
            else if (value is double)
            {
                double height = (double)value;

                if (height is double.NaN)
                {
                    height = 50;
                }

                double radiusRatio = 5;
                double offsetRatio = 15;

                if (smallerOutset)
                {
                    radiusRatio = 10;
                    offsetRatio = 30;
                }

                if (height > 0)
                {
                    // for a 300x300 button shadow radius must be 60 (300/5);
                    double radius = (double)(height / radiusRatio);

                    // for a 300x300 button shadow offset must be 20 (300/15);
                    double offset = (double)(height / offsetRatio);

                    //-20 -20 60 #CCFFFFFF,20 20 60 #33000000
                    if (!insetAndOutset)
                    {
                        // outset
                        main.OffsetX = -offset;
                        main.OffsetY = -offset;
                        main.Blur = radius;
                        main.Color = Colors.Gainsboro;

                        rest1.OffsetX = offset;
                        rest1.OffsetY = offset;
                        rest1.Blur = radius;
                        rest1.Color = Colors.Gainsboro;


                        if (inset)
                        {
                            // inset
                            main.IsInset = true;
                            rest1.IsInset = true;

                            main.OffsetX = offset / 2;
                            main.OffsetY = offset / 2;

                            rest1.OffsetX = -offset / 2;
                            rest1.OffsetY = -offset / 2;

                            // invert shadow colors
                            rest1.Color = Colors.Gainsboro;
                            main.Color = Colors.Gainsboro;
                        }

                        rests.Add(rest1);
                    }
                    else
                    {
                        // inset + outset
                        main.IsInset = false;
                        main.OffsetX = -offset;
                        main.OffsetY = -offset;
                        main.Blur = radius;
                        main.Color = Colors.Gainsboro;

                        rest1.IsInset = false;
                        rest1.OffsetX = offset;
                        rest1.OffsetY = offset;
                        rest1.Blur = radius;
                        rest1.Color = Colors.Gainsboro;
                        rests.Add(rest1);

                        BoxShadow rest2 = new BoxShadow();
                        rest2.IsInset = true;
                        rest2.OffsetX = offset;
                        rest2.OffsetY = offset;
                        rest2.Blur = radius;
                        rest2.Color = new Color(1, 1, 0, 1);
                        rests.Add(rest2);

                        //BoxShadow rest3 = new BoxShadow();
                        //rest3.IsInset = true;
                        //rest3.OffsetX = -offset;
                        //rest3.OffsetY = -offset;
                        //rest3.Blur = radius;
                        //rest3.Color = new Color(1, 1, 0, 0);
                        //rests.Add(rest3);
                    }
                }
            }

            b = new BoxShadows(main, rests.ToArray());

            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
