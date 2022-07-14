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
        public static List<DisplayRaces> GetPlayableDisplayRaces(this DisplayRaces displayRacesEnum)
        {
            return new List<DisplayRaces>()
            {
                DisplayRaces.BLOOD_ELF,
                DisplayRaces.DARK_IRON_DWARF,
                DisplayRaces.DRAENEI,
                DisplayRaces.DWARF,
                DisplayRaces.GNOME,
                DisplayRaces.GOBLIN,
                DisplayRaces.HIGHMOUNTAIN_TAUREN,
                DisplayRaces.HUMAN,
                DisplayRaces.KUL_TIRAN,
                DisplayRaces.LIGHTFORGED_DRAENEI,
                DisplayRaces.MAGHAR_ORC,
                DisplayRaces.MECHAGNOME,
                DisplayRaces.NIGHTBORNE,
                DisplayRaces.NIGHT_ELF,
                DisplayRaces.ORC,
                DisplayRaces.PANDAREN,
                DisplayRaces.TAUREN,
                DisplayRaces.TROLL,
                DisplayRaces.UNDEAD,
                DisplayRaces.VOID_ELF,
                DisplayRaces.VULPERA,
                DisplayRaces.WORGEN,
                DisplayRaces.ZANDALARI_TROLL
            };
        }

        public static string ToDisplayString(this DisplayRaces displayRacesEnum)
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
