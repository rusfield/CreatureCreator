using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class ItemSourceTypeExtensions
    {
        public static string ToDisplayName(this ItemSourceTypes itemSourceType)
        {
            return (itemSourceType) switch
            {
                ItemSourceTypes.NONE => "",
                ItemSourceTypes.RAID_LFR => "Looking for Raid",
                ItemSourceTypes.RAID_NORMAL => "Normal",
                ItemSourceTypes.RAID_HEROIC => "Heroic",
                ItemSourceTypes.RAID_MYTHIC => "Mythic",
                ItemSourceTypes.ARTIFACT_CAMPAIGN_1 => "Basic 1",
                ItemSourceTypes.ARTIFACT_CAMPAIGN_2 => "Basic 2",
                ItemSourceTypes.ARTIFACT_CAMPAIGN_3 => "Basic 3",
                ItemSourceTypes.ARTIFACT_CAMPAIGN_4 => "Basic 4",
                ItemSourceTypes.ARTIFACT_CLASS_HALL_1 => "Class Hall 1",
                ItemSourceTypes.ARTIFACT_CLASS_HALL_2 => "Class Hall 2",
                ItemSourceTypes.ARTIFACT_CLASS_HALL_3 => "Class Hall 3",
                ItemSourceTypes.ARTIFACT_CLASS_HALL_4 => "Class Hall 4",
                ItemSourceTypes.ARTIFACT_BALANCE_OF_POWER_1 => "Balance of Power 1",
                ItemSourceTypes.ARTIFACT_BALANCE_OF_POWER_2 => "Balance of Power 2",
                ItemSourceTypes.ARTIFACT_BALANCE_OF_POWER_3 => "Balance of Power 3",
                ItemSourceTypes.ARTIFACT_BALANCE_OF_POWER_4 => "Balance of Power 4",
                ItemSourceTypes.ARTIFACT_PVP_1 => "Honor 1",
                ItemSourceTypes.ARTIFACT_PVP_2 => "Honor 2",
                ItemSourceTypes.ARTIFACT_PVP_3 => "Honor 3",
                ItemSourceTypes.ARTIFACT_PVP_4 => "Honor 4",
                ItemSourceTypes.ARTIFACT_MAGE_TOWER_1 => "Challenge 1",
                ItemSourceTypes.ARTIFACT_MAGE_TOWER_2 => "Challenge 2",
                ItemSourceTypes.ARTIFACT_MAGE_TOWER_3 => "Challenge 3",
                ItemSourceTypes.ARTIFACT_MAGE_TOWER_4 => "Challenge 4",
                ItemSourceTypes.ARTIFACT_HIDDEN_1 => "Hidden 1",
                ItemSourceTypes.ARTIFACT_HIDDEN_2 => "Hidden 2",
                ItemSourceTypes.ARTIFACT_HIDDEN_3 => "Hidden 3",
                ItemSourceTypes.ARTIFACT_HIDDEN_4 => "Hidden 4",
                _ => itemSourceType.ToString()
            };
        }
    }
}
