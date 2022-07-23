using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class DisplayRaceExtensions
    {
        public static List<Races> GetPlayableDisplayRaces(this Races displayRacesEnum)
        {
            return new List<Races>()
            {
                Races.BLOOD_ELF,
                Races.DARK_IRON_DWARF,
                Races.DRAENEI,
                Races.DWARF,
                Races.GNOME,
                Races.GOBLIN,
                Races.HIGHMOUNTAIN_TAUREN,
                Races.HUMAN,
                Races.KUL_TIRAN,
                Races.LIGHTFORGED_DRAENEI,
                Races.MAGHAR_ORC,
                Races.MECHAGNOME,
                Races.NIGHTBORNE,
                Races.NIGHT_ELF,
                Races.ORC,
                Races.PANDAREN,
                Races.TAUREN,
                Races.TROLL,
                Races.UNDEAD,
                Races.VOID_ELF,
                Races.VULPERA,
                Races.WORGEN,
                Races.ZANDALARI_TROLL
            };
        }

        public static string ToDisplayString(this Races displayRacesEnum)
        {
            var split = displayRacesEnum.ToString().Split('_');
            string name = "";
            foreach(var word in split)
            {
                string subWord = word.Substring(0, 1).ToUpper();
                subWord += word.Substring(1, word.Length - 1).ToLower();
                name += subWord + " ";
            }
            return name.Trim();
        }
    }
}
