using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class CharacterInventorySlotExtensions
    {
        public static bool IsWeaponSlot(this CharacterInventorySlots slot)
        {
            return slot == CharacterInventorySlots.MAIN_HAND || slot == CharacterInventorySlots.OFF_HAND || slot == CharacterInventorySlots.RANGED;
        }
    }
}
