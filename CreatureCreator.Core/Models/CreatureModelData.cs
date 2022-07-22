using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class CreatureModelData : IDb2
    {
        public int Id { get; set; }
        public int BloodId { get; set; }
        public int SoundId { get; set; }
    }
}
