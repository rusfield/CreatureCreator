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
                (DisplayRaces.Human, Genders.Male) => ChrModels.HumanMale,
                (DisplayRaces.Human, Genders.Female) => ChrModels.HumanFemale,
                (DisplayRaces.Orc, Genders.Male) => ChrModels.OrcMale,
                (DisplayRaces.Orc, Genders.Female) => ChrModels.OrcFemale,
                (DisplayRaces.Dwarf, Genders.Male) => ChrModels.DwarfMale,
                (DisplayRaces.Dwarf, Genders.Female) => ChrModels.DwarfFemale,
                (DisplayRaces.NightElf, Genders.Male) => ChrModels.NightElfMale,
                (DisplayRaces.NightElf, Genders.Female) => ChrModels.NightElfFemale,
                (DisplayRaces.Undead, Genders.Male) => ChrModels.UndeadMale,
                (DisplayRaces.Undead, Genders.Female) => ChrModels.UndeadFemale,
                (DisplayRaces.Tauren, Genders.Male) => ChrModels.TaurenMale,
                (DisplayRaces.Tauren, Genders.Female) => ChrModels.TaurenFemale,
                (DisplayRaces.Gnome, Genders.Male) => ChrModels.GnomeMale,
                (DisplayRaces.Gnome, Genders.Female) => ChrModels.GnomeFemale,
                (DisplayRaces.Troll, Genders.Male) => ChrModels.TrollMale,
                (DisplayRaces.Troll, Genders.Female) => ChrModels.TrollFemale,
                (DisplayRaces.Goblin, Genders.Male) => ChrModels.GoblinMale,
                (DisplayRaces.Goblin, Genders.Female) => ChrModels.GoblinFemale,
                (DisplayRaces.BloodElf, Genders.Male) => ChrModels.BloodElfMale,
                (DisplayRaces.BloodElf, Genders.Female) => ChrModels.BloodElfFemale,
                (DisplayRaces.Draenei, Genders.Male) => ChrModels.DraeneiMale,
                (DisplayRaces.Draenei, Genders.Female) => ChrModels.DraeneiFemale,
                (DisplayRaces.FelOrc, Genders.Male) => ChrModels.FelOrcMale,
                (DisplayRaces.FelOrc, Genders.Female) => ChrModels.FelOrcFemale,
                (DisplayRaces.Naga, Genders.Male) => ChrModels.NagaMale,
                (DisplayRaces.Naga, Genders.Female) => ChrModels.NagaFemale,
                (DisplayRaces.Broken, Genders.Male) => ChrModels.BrokenMale,
                (DisplayRaces.Broken, Genders.Female) => ChrModels.BrokenFemale,
                (DisplayRaces.Skeleton, Genders.Male) => ChrModels.SkeletonMale,
                (DisplayRaces.Skeleton, Genders.Female) => ChrModels.SkeletonFemale,
                (DisplayRaces.Vrykul, Genders.Male) => ChrModels.VrykulMale,
                (DisplayRaces.Vrykul, Genders.Female) => ChrModels.VrykulFemale,
                (DisplayRaces.Tuskarr, Genders.Male) => ChrModels.TuskarrMale,
                (DisplayRaces.Tuskarr, Genders.Female) => ChrModels.TuskarrFemale,
                (DisplayRaces.ForestTroll, Genders.Male) => ChrModels.ForestTrollMale,
                (DisplayRaces.ForestTroll, Genders.Female) => ChrModels.ForestTrollFemale,
                (DisplayRaces.Taunka, Genders.Male) => ChrModels.TaunkaMale,
                (DisplayRaces.Taunka, Genders.Female) => ChrModels.TaunkaFemale,
                (DisplayRaces.NorthrendSkeleton, Genders.Male) => ChrModels.NorthrendSkeletonMale,
                (DisplayRaces.NorthrendSkeleton, Genders.Female) => ChrModels.NorthrendSkeletonFemale,
                (DisplayRaces.IceTroll, Genders.Male) => ChrModels.IceTrollMale,
                (DisplayRaces.IceTroll, Genders.Female) => ChrModels.IceTrollFemale,
                (DisplayRaces.Worgen, Genders.Male) => ChrModels.WorgenMale,
                (DisplayRaces.Worgen, Genders.Female) => ChrModels.WorgenFemale,
                (DisplayRaces.GilneanHuman, Genders.Male) => ChrModels.GilneanMale,
                (DisplayRaces.GilneanHuman, Genders.Female) => ChrModels.GilneanFemale,
                (DisplayRaces.Pandaren, Genders.Male) => ChrModels.PandarenMale,
                (DisplayRaces.Pandaren, Genders.Female) => ChrModels.PandarenFemale,
                (DisplayRaces.TushuiPandaren, Genders.Male) => ChrModels.PandarenMale, // ??
                (DisplayRaces.TushuiPandaren, Genders.Female) => ChrModels.PandarenFemale, // ??
                (DisplayRaces.HuojinPandaren, Genders.Male) => ChrModels.PandarenMale, // ??
                (DisplayRaces.HuojinPandaren, Genders.Female) => ChrModels.PandarenFemale, // ??
                (DisplayRaces.Nightborne, Genders.Male) => ChrModels.NightborneMale,
                (DisplayRaces.Nightborne, Genders.Female) => ChrModels.NightborneFemale,
                (DisplayRaces.HighmountaunTauren, Genders.Male) => ChrModels.HighmountainTaurenMale,
                (DisplayRaces.HighmountaunTauren, Genders.Female) => ChrModels.HighmountainTaurenFemale,
                (DisplayRaces.VoidElf, Genders.Male) => ChrModels.VoidElfMale,
                (DisplayRaces.VoidElf, Genders.Female) => ChrModels.VoidElfFemale,
                (DisplayRaces.LightforgedDraenei, Genders.Male) => ChrModels.LightforgedDraeneiMale,
                (DisplayRaces.LightforgedDraenei, Genders.Female) => ChrModels.LightforgedDraeneiFemale,
                (DisplayRaces.ZandalariTroll, Genders.Male) => ChrModels.ZandalariMale,
                (DisplayRaces.ZandalariTroll, Genders.Female) => ChrModels.ZandalariFemale,
                (DisplayRaces.KulTiran, Genders.Male) => ChrModels.KulTiranMale,
                (DisplayRaces.KulTiran, Genders.Female) => ChrModels.KulTiranFemale,
                (DisplayRaces.ThinHuman, Genders.Male) => ChrModels.ThinHumanMale,
                (DisplayRaces.ThinHuman, Genders.Female) => ChrModels.ThinHumanFemale,
                (DisplayRaces.DarkIronDwarf, Genders.Male) => ChrModels.DarkIronDwarfMale,
                (DisplayRaces.DarkIronDwarf, Genders.Female) => ChrModels.DarkIronDwarfFemale,
                (DisplayRaces.Vulpera, Genders.Male) => ChrModels.VoidElfMale,
                (DisplayRaces.Vulpera, Genders.Female) => ChrModels.VoidElfFemale,
                (DisplayRaces.MagharOrc, Genders.Male) => ChrModels.MagharOrcMale,
                (DisplayRaces.MagharOrc, Genders.Female) => ChrModels.MagharOrcFemale,
                (DisplayRaces.Mechagnome, Genders.Male) => ChrModels.MechagnomeMale,
                (DisplayRaces.Mechagnome, Genders.Female) => ChrModels.MechagnomeFemale,
                //_ => null
            };
        }
    }
}
