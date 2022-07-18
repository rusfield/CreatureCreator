﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static T ToMask<T>(this IEnumerable<T> values) where T : Enum
        {
            long builtValue = 0;
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (values.Contains(value))
                {
                    builtValue |= Convert.ToInt64(value);
                }
            }
            return (T)Enum.Parse(typeof(T), builtValue.ToString());
        }

        public static IEnumerable<T> ToValues<T>(this T flags) where T : Enum
        {
            var input = (long)(object)flags;
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                var val = (long)(object)value;
                if (0 != (val & input))
                {
                    yield return value;
                }
            }
        }
    }
}
