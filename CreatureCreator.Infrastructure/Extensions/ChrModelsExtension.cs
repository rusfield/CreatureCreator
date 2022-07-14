using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class ChrModelsExtension
    {
        public static ChrModels ConvertFromRaceAndGender(this ChrModels chrModel, DisplayRaces race, Genders gender)
        {
            return (race, gender) switch
            {
                (DisplayRaces.HUMAN, Genders.Male) => ChrModels.HUMAN_MALE,
                (DisplayRaces.HUMAN, Genders.Female) => ChrModels.HUMAN_FEMALE,
                (DisplayRaces.ORC, Genders.Male) => ChrModels.ORC_MALE,
                (DisplayRaces.ORC, Genders.Female) => ChrModels.ORC_FEMALE,
                (DisplayRaces.DWARF, Genders.Male) => ChrModels.DWARF_MALE,
                (DisplayRaces.DWARF, Genders.Female) => ChrModels.DWARF_FEMALE,
                (DisplayRaces.NIGHT_ELF, Genders.Male) => ChrModels.NIGHT_ELF_MALE,
                (DisplayRaces.NIGHT_ELF, Genders.Female) => ChrModels.NIGHT_ELF_FEMALE,
                (DisplayRaces.UNDEAD, Genders.Male) => ChrModels.UNDEAD_MALE,
                (DisplayRaces.UNDEAD, Genders.Female) => ChrModels.UNDEAD_FEMALE,
                (DisplayRaces.TAUREN, Genders.Male) => ChrModels.TAUREN_MALE,
                (DisplayRaces.TAUREN, Genders.Female) => ChrModels.TAUREN_FEMALE,
                (DisplayRaces.GNOME, Genders.Male) => ChrModels.GNOME_MALE,
                (DisplayRaces.GNOME, Genders.Female) => ChrModels.GNOME_FEMALE,
                (DisplayRaces.TROLL, Genders.Male) => ChrModels.TROLL_MALE,
                (DisplayRaces.TROLL, Genders.Female) => ChrModels.TROLL_FEMALE,
                (DisplayRaces.GOBLIN, Genders.Male) => ChrModels.GOBLIN_MALE,
                (DisplayRaces.GOBLIN, Genders.Female) => ChrModels.GOBLIN_FEMALE,
                (DisplayRaces.BLOOD_ELF, Genders.Male) => ChrModels.BLOOD_ELF_MALE,
                (DisplayRaces.BLOOD_ELF, Genders.Female) => ChrModels.BLOOD_ELF_FEMALE,
                (DisplayRaces.DRAENEI, Genders.Male) => ChrModels.DRAENEI_MALE,
                (DisplayRaces.DRAENEI, Genders.Female) => ChrModels.DRAENEI_FEMALE,
                (DisplayRaces.FEL_ORC, Genders.Male) => ChrModels.FEL_ORC_MALE,
                (DisplayRaces.FEL_ORC, Genders.Female) => ChrModels.FEL_ORC_FEMALE,
                (DisplayRaces.NAGA, Genders.Male) => ChrModels.NAGA_MALE,
                (DisplayRaces.NAGA, Genders.Female) => ChrModels.NAGA_FEMALE,
                (DisplayRaces.BROKEN, Genders.Male) => ChrModels.BROKEN_MALE,
                (DisplayRaces.BROKEN, Genders.Female) => ChrModels.BROKEN_FEMALE,
                (DisplayRaces.SKELETON, Genders.Male) => ChrModels.SKELETON_MALE,
                (DisplayRaces.SKELETON, Genders.Female) => ChrModels.SKELETON_FEMALE,
                (DisplayRaces.VRYKUL, Genders.Male) => ChrModels.VRYKUL_MALE,
                (DisplayRaces.VRYKUL, Genders.Female) => ChrModels.VRYKUL_FEMALE,
                (DisplayRaces.TUSKARR, Genders.Male) => ChrModels.TUSKARR_MALE,
                (DisplayRaces.TUSKARR, Genders.Female) => ChrModels.TUSKARR_FEMALE,
                (DisplayRaces.FOREST_TROLL, Genders.Male) => ChrModels.FOREST_TROLL_MALE,
                (DisplayRaces.FOREST_TROLL, Genders.Female) => ChrModels.FOREST_TROLL_FEMALE,
                (DisplayRaces.TAUNKA, Genders.Male) => ChrModels.TAUNKA_MALE,
                (DisplayRaces.TAUNKA, Genders.Female) => ChrModels.TAUNKA_FEMALE,
                (DisplayRaces.NORTHREND_SKELETON, Genders.Male) => ChrModels.NORTHREND_SKELETON_MALE,
                (DisplayRaces.NORTHREND_SKELETON, Genders.Female) => ChrModels.NORTHREND_SKELETON_FEMALE,
                (DisplayRaces.ICE_TROLL, Genders.Male) => ChrModels.ICE_TROLL_MALE,
                (DisplayRaces.ICE_TROLL, Genders.Female) => ChrModels.ICE_TROLL_FEMALE,
                (DisplayRaces.WORGEN, Genders.Male) => ChrModels.WORGEN_MALE,
                (DisplayRaces.WORGEN, Genders.Female) => ChrModels.WORGEN_FEMALE,
                (DisplayRaces.GILNEAN_HUMAN, Genders.Male) => ChrModels.GILNEAN_MALE,
                (DisplayRaces.GILNEAN_HUMAN, Genders.Female) => ChrModels.GILNEAN_FEMALE,
                (DisplayRaces.PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE,
                (DisplayRaces.PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE,
                (DisplayRaces.TUSHUI_PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE, // ??
                (DisplayRaces.TUSHUI_PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE, // ??
                (DisplayRaces.HUOJIN_PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE, // ??
                (DisplayRaces.HUOJIN_PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE, // ??
                (DisplayRaces.NIGHTBORNE, Genders.Male) => ChrModels.NIGHTBORN_MALE,
                (DisplayRaces.NIGHTBORNE, Genders.Female) => ChrModels.NIGHTBORN_FEMALE,
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Male) => ChrModels.HIGHMOUNTAIN_TAUREN_MALE,
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Female) => ChrModels.HIGHMOUNTAIN_TAUREN_FEMALE,
                (DisplayRaces.VOID_ELF, Genders.Male) => ChrModels.VOID_ELF_MALE,
                (DisplayRaces.VOID_ELF, Genders.Female) => ChrModels.VOID_ELF_FEMALE,
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Male) => ChrModels.LIGHTFORGED_DRAENEI_MALE,
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Female) => ChrModels.LIGHTFORGED_DRAENEI_FEMALE,
                (DisplayRaces.ZANDALARI_TROLL, Genders.Male) => ChrModels.ZANDALARI_MALE,
                (DisplayRaces.ZANDALARI_TROLL, Genders.Female) => ChrModels.ZANDALARI_FEMALE,
                (DisplayRaces.KUL_TIRAN, Genders.Male) => ChrModels.KUL_TIRAN_MALE,
                (DisplayRaces.KUL_TIRAN, Genders.Female) => ChrModels.KUL_TIRAN_FEMALE,
                (DisplayRaces.THIN_HUMAN, Genders.Male) => ChrModels.THIN_HUMAN_MALE,
                (DisplayRaces.THIN_HUMAN, Genders.Female) => ChrModels.THIN_HUMAN_FEMALE,
                (DisplayRaces.DARK_IRON_DWARF, Genders.Male) => ChrModels.DARK_IRON_DWARF_MALE,
                (DisplayRaces.DARK_IRON_DWARF, Genders.Female) => ChrModels.DARK_IRON_DWARF_FEMALE,
                (DisplayRaces.VULPERA, Genders.Male) => ChrModels.VOID_ELF_MALE,
                (DisplayRaces.VULPERA, Genders.Female) => ChrModels.VOID_ELF_FEMALE,
                (DisplayRaces.MAGHAR_ORC, Genders.Male) => ChrModels.MAGHAR_ORC_MALE,
                (DisplayRaces.MAGHAR_ORC, Genders.Female) => ChrModels.MAGHAR_ORC_FEMALE,
                (DisplayRaces.MECHAGNOME, Genders.Male) => ChrModels.MECHAGNOME_MALE,
                (DisplayRaces.MECHAGNOME, Genders.Female) => ChrModels.MECHAGNOME_FEMALE,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
