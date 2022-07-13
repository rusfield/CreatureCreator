using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class CreatureDisplayInfoExtra : IHotfixesSchema, IDb2
    {
        [Key]
        public int Id { get; set; }
        public DisplayRaces DisplayRaceId { get; set; }
        public Genders DisplaySexId { get; set; }

    }
}
