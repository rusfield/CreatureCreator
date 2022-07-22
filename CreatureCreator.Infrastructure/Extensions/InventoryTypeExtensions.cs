using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class InventoryTypeExtensions
    {
        public static ArmorSlots? ToArmorSlot(this InventoryTypes inventoryType)
        {
            return (inventoryType) switch
            {
                InventoryTypes.HANDS => ArmorSlots.HANDS,
                InventoryTypes.WAIST => ArmorSlots.WAIST,
                InventoryTypes.CHEST_ROBE => ArmorSlots.CHEST,
                InventoryTypes.LEGS => ArmorSlots.LEGS,
                InventoryTypes.CHEST_BODY => ArmorSlots.CHEST,
                InventoryTypes.BACK => ArmorSlots.BACK,
                InventoryTypes.HEAD => ArmorSlots.HEAD,
                InventoryTypes.SHOULDERS => ArmorSlots.SHOULDERS,
                InventoryTypes.SHIRT => ArmorSlots.SHIRT,
                InventoryTypes.TABARD => ArmorSlots.TABARD,
                InventoryTypes.WRISTS => ArmorSlots.WRISTS,
                InventoryTypes.FEET => ArmorSlots.FEET,
                _ => null
            };
        }

        public static WeaponSlots? ToWeaponSlot(this InventoryTypes inventoryTypes)
        {
            return (inventoryTypes) switch
            {
                InventoryTypes.MAIN_HAND => WeaponSlots.MAIN_HAND,
                InventoryTypes.ONE_HAND => WeaponSlots.MAIN_HAND,
                InventoryTypes.TWO_HAND => WeaponSlots.MAIN_HAND,
                InventoryTypes.OFF_HAND_WEAPON => WeaponSlots.OFF_HAND,
                InventoryTypes.OFF_HAND_ARMOR => WeaponSlots.OFF_HAND,
                InventoryTypes.OFF_HAND_SHIELD => WeaponSlots.OFF_HAND,
                InventoryTypes.RANGED_LEFT => WeaponSlots.RANGED,
                InventoryTypes.RANGED_RIGHT => WeaponSlots.RANGED,
                InventoryTypes.THROWN => WeaponSlots.RANGED,
                _ => null
            };
        }
    }
}
