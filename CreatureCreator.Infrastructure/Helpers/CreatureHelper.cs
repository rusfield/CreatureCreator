using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Helpers
{
    public static class CreatureHelper
    {
        static void GetValuesByRaceAndGender(DisplayRaces race, Genders gender, out int modelId, out int soundId)
        {
            modelId = 0;
            soundId = 0;

            switch(race, gender)
            {
                case (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Female):
                    soundId = 5911;
                    break;
            }
        }
    }
}
