using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class ItemAppearance : IDb2
    {
        public int Id { get; set; }
        public int ItemDisplayInfoId { get; set; }
        public int DisplayType { get; set; }
    }
}
