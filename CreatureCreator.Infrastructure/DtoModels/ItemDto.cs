using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.DtoModels
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public int ItemDisplayInfoId { get; set; }
        public int ItemModifiedAppearanceId { get; set; }
        public ArmorSlots? ArmorSlot { get; set; }
        public WeaponSlots? WeaponSlot { get; set; }
        public ItemSourceTypes ItemSourceType { get; set; }
    }
}
