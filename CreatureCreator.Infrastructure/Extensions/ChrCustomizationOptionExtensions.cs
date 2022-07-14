using CreatureCreator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class ChrCustomizationOptionExtensions
    {
        public static bool IsDruidFormCustomization(this ChrCustomizationOption option)
        {
            var druidForms = new List<int>() { 6, 7, 9, 10, 11, 12 }; // IDs gotten from ChrCustomizationCategory
            return druidForms.Any(d => d == option.ChrCustomizationCategoryId);
        }
    }
}
