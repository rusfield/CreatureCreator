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
        public static ChrModels? ConvertFromRaceAndGender(this ChrModels chrModel, Races race, Genders gender)
        {
            return (race, gender) switch
            {
                (Races.HUMAN, Genders.Male) => ChrModels.HUMAN_MALE,
                (Races.HUMAN, Genders.Female) => ChrModels.HUMAN_FEMALE,
                (Races.ORC, Genders.Male) => ChrModels.ORC_MALE,
                (Races.ORC, Genders.Female) => ChrModels.ORC_FEMALE,
                (Races.DWARF, Genders.Male) => ChrModels.DWARF_MALE,
                (Races.DWARF, Genders.Female) => ChrModels.DWARF_FEMALE,
                (Races.NIGHT_ELF, Genders.Male) => ChrModels.NIGHT_ELF_MALE,
                (Races.NIGHT_ELF, Genders.Female) => ChrModels.NIGHT_ELF_FEMALE,
                (Races.UNDEAD, Genders.Male) => ChrModels.UNDEAD_MALE,
                (Races.UNDEAD, Genders.Female) => ChrModels.UNDEAD_FEMALE,
                (Races.TAUREN, Genders.Male) => ChrModels.TAUREN_MALE,
                (Races.TAUREN, Genders.Female) => ChrModels.TAUREN_FEMALE,
                (Races.GNOME, Genders.Male) => ChrModels.GNOME_MALE,
                (Races.GNOME, Genders.Female) => ChrModels.GNOME_FEMALE,
                (Races.TROLL, Genders.Male) => ChrModels.TROLL_MALE,
                (Races.TROLL, Genders.Female) => ChrModels.TROLL_FEMALE,
                (Races.GOBLIN, Genders.Male) => ChrModels.GOBLIN_MALE,
                (Races.GOBLIN, Genders.Female) => ChrModels.GOBLIN_FEMALE,
                (Races.BLOOD_ELF, Genders.Male) => ChrModels.BLOOD_ELF_MALE,
                (Races.BLOOD_ELF, Genders.Female) => ChrModels.BLOOD_ELF_FEMALE,
                (Races.DRAENEI, Genders.Male) => ChrModels.DRAENEI_MALE,
                (Races.DRAENEI, Genders.Female) => ChrModels.DRAENEI_FEMALE,
                (Races.FEL_ORC, Genders.Male) => ChrModels.FEL_ORC_MALE,
                (Races.FEL_ORC, Genders.Female) => ChrModels.FEL_ORC_FEMALE,
                (Races.NAGA, Genders.Male) => ChrModels.NAGA_MALE,
                (Races.NAGA, Genders.Female) => ChrModels.NAGA_FEMALE,
                (Races.BROKEN, Genders.Male) => ChrModels.BROKEN_MALE,
                (Races.BROKEN, Genders.Female) => ChrModels.BROKEN_FEMALE,
                (Races.SKELETON, Genders.Male) => ChrModels.SKELETON_MALE,
                (Races.SKELETON, Genders.Female) => ChrModels.SKELETON_FEMALE,
                (Races.VRYKUL, Genders.Male) => ChrModels.VRYKUL_MALE,
                (Races.VRYKUL, Genders.Female) => ChrModels.VRYKUL_FEMALE,
                (Races.TUSKARR, Genders.Male) => ChrModels.TUSKARR_MALE,
                (Races.TUSKARR, Genders.Female) => ChrModels.TUSKARR_FEMALE,
                (Races.FOREST_TROLL, Genders.Male) => ChrModels.FOREST_TROLL_MALE,
                (Races.FOREST_TROLL, Genders.Female) => ChrModels.FOREST_TROLL_FEMALE,
                (Races.TAUNKA, Genders.Male) => ChrModels.TAUNKA_MALE,
                (Races.TAUNKA, Genders.Female) => ChrModels.TAUNKA_FEMALE,
                (Races.NORTHREND_SKELETON, Genders.Male) => ChrModels.NORTHREND_SKELETON_MALE,
                (Races.NORTHREND_SKELETON, Genders.Female) => ChrModels.NORTHREND_SKELETON_FEMALE,
                (Races.ICE_TROLL, Genders.Male) => ChrModels.ICE_TROLL_MALE,
                (Races.ICE_TROLL, Genders.Female) => ChrModels.ICE_TROLL_FEMALE,
                (Races.WORGEN, Genders.Male) => ChrModels.WORGEN_MALE,
                (Races.WORGEN, Genders.Female) => ChrModels.WORGEN_FEMALE,
                (Races.GILNEAN_HUMAN, Genders.Male) => ChrModels.GILNEAN_MALE,
                (Races.GILNEAN_HUMAN, Genders.Female) => ChrModels.GILNEAN_FEMALE,
                (Races.PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE,
                (Races.PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE,
                (Races.TUSHUI_PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE, // ??
                (Races.TUSHUI_PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE, // ??
                (Races.HUOJIN_PANDAREN, Genders.Male) => ChrModels.PANDAREN_MALE, // ??
                (Races.HUOJIN_PANDAREN, Genders.Female) => ChrModels.PANDAREN_FEMALE, // ??
                (Races.NIGHTBORNE, Genders.Male) => ChrModels.NIGHTBORN_MALE,
                (Races.NIGHTBORNE, Genders.Female) => ChrModels.NIGHTBORN_FEMALE,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Male) => ChrModels.HIGHMOUNTAIN_TAUREN_MALE,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Female) => ChrModels.HIGHMOUNTAIN_TAUREN_FEMALE,
                (Races.VOID_ELF, Genders.Male) => ChrModels.VOID_ELF_MALE,
                (Races.VOID_ELF, Genders.Female) => ChrModels.VOID_ELF_FEMALE,
                (Races.LIGHTFORGED_DRAENEI, Genders.Male) => ChrModels.LIGHTFORGED_DRAENEI_MALE,
                (Races.LIGHTFORGED_DRAENEI, Genders.Female) => ChrModels.LIGHTFORGED_DRAENEI_FEMALE,
                (Races.ZANDALARI_TROLL, Genders.Male) => ChrModels.ZANDALARI_MALE,
                (Races.ZANDALARI_TROLL, Genders.Female) => ChrModels.ZANDALARI_FEMALE,
                (Races.KUL_TIRAN, Genders.Male) => ChrModels.KUL_TIRAN_MALE,
                (Races.KUL_TIRAN, Genders.Female) => ChrModels.KUL_TIRAN_FEMALE,
                (Races.THIN_HUMAN, Genders.Male) => ChrModels.THIN_HUMAN_MALE,
                (Races.THIN_HUMAN, Genders.Female) => ChrModels.THIN_HUMAN_FEMALE,
                (Races.DARK_IRON_DWARF, Genders.Male) => ChrModels.DARK_IRON_DWARF_MALE,
                (Races.DARK_IRON_DWARF, Genders.Female) => ChrModels.DARK_IRON_DWARF_FEMALE,
                (Races.VULPERA, Genders.Male) => ChrModels.VOID_ELF_MALE,
                (Races.VULPERA, Genders.Female) => ChrModels.VOID_ELF_FEMALE,
                (Races.MAGHAR_ORC, Genders.Male) => ChrModels.MAGHAR_ORC_MALE,
                (Races.MAGHAR_ORC, Genders.Female) => ChrModels.MAGHAR_ORC_FEMALE,
                (Races.MECHAGNOME, Genders.Male) => ChrModels.MECHAGNOME_MALE,
                (Races.MECHAGNOME, Genders.Female) => ChrModels.MECHAGNOME_FEMALE,
                _ => null
            };
        }
    }
}
