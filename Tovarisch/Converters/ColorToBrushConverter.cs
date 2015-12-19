﻿namespace Wacton.Tovarisch.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is System.Windows.Media.Color)
            {
                var color = (System.Windows.Media.Color)value;
                return new SolidColorBrush(color);
            }

            if (value is System.Drawing.Color)
            {
                var drawingColor = (System.Drawing.Color)value;
                var mediaColor = System.Windows.Media.Color.FromRgb(drawingColor.R, drawingColor.G, drawingColor.B);
                return new SolidColorBrush(mediaColor);
            }

            var type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]"); 
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
