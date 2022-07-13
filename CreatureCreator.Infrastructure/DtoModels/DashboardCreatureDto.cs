using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.DtoModels
{
    public class DashboardCreatureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DisplayRaces Race { get; set; }
        public Genders Gender { get; set; }
    }
}
