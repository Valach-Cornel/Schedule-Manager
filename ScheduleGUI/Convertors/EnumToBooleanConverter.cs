using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ScheduleGUI.Convertors
{
    public class EnumToBooleanConverter : IValueConverter
    {
        private uint _valoareCurenta;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;

            _valoareCurenta = (uint)System.Convert.ChangeType(value, typeof(uint));
            uint flag = (uint)System.Convert.ChangeType(parameter, typeof(uint));

            return (_valoareCurenta & flag) == flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            uint flag = (uint)System.Convert.ChangeType(parameter, typeof(uint));

            if (isChecked)
            {
                _valoareCurenta |= flag;
            }
            else
            {
                _valoareCurenta &= ~flag;
            }

            return Enum.ToObject(targetType, _valoareCurenta);
        }
    }
}
