using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class ChrCustomizationOption : IDb2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChrModels ChrModelId { get; set; }
    }
}
