using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class CreatureTemplateAddon : IWorldSchema
    {
        public int Entry { get; set; }
        public string Auras { get; set; }
    }
}
