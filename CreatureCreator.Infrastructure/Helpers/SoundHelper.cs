using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Helpers
{
    public static class SoundHelper
    {
        public static int GetSoundIdForDatabase(Races race, Genders gender, int creatureDisplayInfoSoundId)
        {
            /*
             * I am not sure how SoundID works 100%, but this setup seems to work:
             * There is a SoundID on CreatureDisplayInfo and also on CreatureModelData. The one on CreatureModelData seems to not really make a difference, so code focuses on the CreatureDisplayInfo one.
             * If SoundID is set to 0, the sounds seems to default to the model somehow. If SoundID is set to something else, the SoundID will point to CreatureSoundData where the row contains references 
             * to the sound files. It is not guaranteed that the row is complete with all updated sounds (for attacks, emotes, etc), and it is not easy to track down the best row. So what this method does
             * is to set the SoundID in Database to 0 if the SoundID equals the default SoundID (to guarantee the most correct sounds). If the SoundID is overriden to an ID that is not the race being
             * saved, it will save this ID instead.
             */

            var defaultSoundId = GetDefaultSoundId(race, gender);
            if (defaultSoundId == creatureDisplayInfoSoundId)
                return 0;
            return creatureDisplayInfoSoundId;
        }

        public static int GetDefaultSoundId(Races race, Genders gender)
        {
            // TODO: Needs more digging. 
            // Some missing, and some of them are missing crit attack sound.
            return (race, gender) switch
            {
                (Races.HUMAN, Genders.Male) => 6847,
                (Races.HUMAN, Genders.Female) => 6688,
                (Races.ORC, Genders.Male) => 51,
                (Races.ORC, Genders.Female) => 4314,
                (Races.DWARF, Genders.Male) => 53,
                (Races.DWARF, Genders.Female) => 54,
                (Races.NIGHT_ELF, Genders.Male) => 55,
                (Races.NIGHT_ELF, Genders.Female) => 56,
                (Races.UNDEAD, Genders.Male) => 57,
                (Races.UNDEAD, Genders.Female) => 58,
                (Races.TAUREN, Genders.Male) => 59,
                (Races.TAUREN, Genders.Female) => 60,
                (Races.GNOME, Genders.Male) => 294,
                (Races.GNOME, Genders.Female) => 295,
                (Races.TROLL, Genders.Male) => 296,
                (Races.TROLL, Genders.Female) => 297,
                (Races.GOBLIN, Genders.Male) => 4315,
                (Races.GOBLIN, Genders.Female) => 4316,
                (Races.BLOOD_ELF, Genders.Male) => 2155,
                (Races.BLOOD_ELF, Genders.Female) => 2156,
                (Races.DRAENEI, Genders.Male) => 2153,
                (Races.DRAENEI, Genders.Female) => 2154,
                (Races.WORGEN, Genders.Male) => 3058,
                (Races.WORGEN, Genders.Female) => 3061,
                (Races.DARK_IRON_DWARF, Genders.Male) => 5958,
                (Races.DARK_IRON_DWARF, Genders.Female) => 5959,
                (Races.MAGHAR_ORC, Genders.Male) => 6087,
                (Races.MAGHAR_ORC, Genders.Female) => 6086,
                (Races.PANDAREN, Genders.Male) => 4560,
                (Races.PANDAREN, Genders.Female) => 4559,
                (Races.KUL_TIRAN, Genders.Male) => 6008,
                (Races.KUL_TIRAN, Genders.Female) => 6296,
                (Races.NIGHTBORNE, Genders.Male) => 5919,
                (Races.NIGHTBORNE, Genders.Female) => 5918,
                (Races.VOID_ELF, Genders.Male) => 5912,
                (Races.VOID_ELF, Genders.Female) => 5913,
                (Races.MECHAGNOME, Genders.Male) => 6644,
                (Races.MECHAGNOME, Genders.Female) => 6643,
                (Races.VULPERA, Genders.Male) => 6278,
                (Races.VULPERA, Genders.Female) => 6279,
                (Races.ZANDALARI_TROLL, Genders.Male) => 6453,
                (Races.ZANDALARI_TROLL, Genders.Female) => 6448,
                (Races.LIGHTFORGED_DRAENEI, Genders.Male) => 5917,
                (Races.LIGHTFORGED_DRAENEI, Genders.Female) => 5916,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Male) => 5915,
                (Races.HIGHMOUNTAIN_TAUREN, Genders.Female) => 5911,

                _ => 0
            };
        }
    }
}
