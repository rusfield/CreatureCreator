using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Helpers
{
    public static class ModelHelper
    {
        public static int? GetModelIdByRaceAndGenders(Races race, Genders gender, Dictionary<int, int> customizations)
        {

            /* Comments:
             * These models are from CreatureModelData. The FileDataId points correctly to character/{race}/{gender}/{race}{gender}.m2.
             * Most have 1 but some have 0 (worgen) in SizeClass. Also, Flags are different. Idk what they are all used for.
             * */


            // For orcs and mag'har orcs male
            bool upright = true;
            if (gender == Genders.Male && race == Races.ORC)
            {
                upright = customizations.Any(c => c.Value == 439);
            }
            else if (gender == Genders.Male && race == Races.MAGHAR_ORC)
            {
                upright = customizations.Any(c => c.Value == 3427);
            }


            return (race, gender) switch
            {
                (Races.HUMAN, Genders.Male) => 7661,
                (Races.HUMAN, Genders.Female) => 7599,
                (Races.ORC, Genders.Male) => upright ? 10882 : 6838,
                (Races.ORC, Genders.Female) => 7200,
                (Races.DWARF, Genders.Male) => 5408,
                (Races.DWARF, Genders.Female) => 7203,
                (Races.NIGHT_ELF, Genders.Male) => 7369,
                (Races.NIGHT_ELF, Genders.Female) => 7300,
                (Races.UNDEAD, Genders.Male) => 7233,
                (Races.UNDEAD, Genders.Female) => 7578,
                (Races.TAUREN, Genders.Male) => 7399,
                (Races.TAUREN, Genders.Female) => 7576,
                (Races.GNOME, Genders.Male) => 6837,
                (Races.GNOME, Genders.Female) => 7130,
                (Races.TROLL, Genders.Male) => 7778,
                (Races.TROLL, Genders.Female) => 7793,
                (Races.GOBLIN, Genders.Male) => 831,
                (Races.GOBLIN, Genders.Female) => 832,
                (Races.BLOOD_ELF, Genders.Male) => 8102,
                (Races.BLOOD_ELF, Genders.Female) => 8103,
                (Races.DRAENEI, Genders.Male) => 7629,
                (Races.DRAENEI, Genders.Female) => 7692,
                (Races.WORGEN, Genders.Male) => 3141,
                (Races.WORGEN, Genders.Female) => 3142,
                (Races.DARK_IRON_DWARF, Genders.Male) => 10784,
                (Races.DARK_IRON_DWARF, Genders.Female) => 10785,
                (Races.MAGHAR_ORC, Genders.Male) => upright ? 10883 : 10844, // Referencing orc?
                (Races.MAGHAR_ORC, Genders.Female) => 10845, // Referencing orc?
                (Races.PANDAREN, Genders.Male) => 3967,
                (Races.PANDAREN, Genders.Female) => 3968,
                (Races.KUL_TIRAN, Genders.Male) => 10531,
                (Races.KUL_TIRAN, Genders.Female) => 10532,
                (Races.NIGHTBORNE, Genders.Male) => 9930,
                (Races.NIGHTBORNE, Genders.Female) => 9931,
                (Races.VOID_ELF, Genders.Male) => 9934,
                (Races.VOID_ELF, Genders.Female) => 9935,
                (Races.MECHAGNOME, Genders.Male) => 11488,
                (Races.MECHAGNOME, Genders.Female) => 11489,
                (Races.VULPERA, Genders.Male) => 10786,
                (Races.VULPERA, Genders.Female) => 10787,
                (Races.ZANDALARI_TROLL, Genders.Male) => 10394,
                (Races.ZANDALARI_TROLL, Genders.Female) => 10395,
                (Races.LIGHTFORGED_DRAENEI, Genders.Male) => 9936,
                (Races.LIGHTFORGED_DRAENEI, Genders.Female) => 9937,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Male) => 9932,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Female) => 9933,

                _ => null
            };
        }
    }
}
