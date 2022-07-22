using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Extensions
{
    public static class CreatureDisplayInfoExtension
    {

        public static int? GetDisplayInfoExtraIdByRaceAndGenders(this CreatureDisplayInfoExtra creatureDisplayInfoExtra, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.HUMAN, Genders.Male) => 25076,
                (DisplayRaces.HUMAN, Genders.Female) => 25075,
                (DisplayRaces.ORC, Genders.Male) => 25067,       
                (DisplayRaces.ORC, Genders.Female) => 13991,  
                (DisplayRaces.DWARF, Genders.Male) => 13992, 
                (DisplayRaces.DWARF, Genders.Female) => 25068,  
                (DisplayRaces.NIGHT_ELF, Genders.Male) => 13993,   
                (DisplayRaces.NIGHT_ELF, Genders.Female) => 25069,
                (DisplayRaces.UNDEAD, Genders.Male) => 25077,  
                (DisplayRaces.UNDEAD, Genders.Female) => 25078, 
                (DisplayRaces.TAUREN, Genders.Male) => 13994,  
                (DisplayRaces.TAUREN, Genders.Female) => 25070,   
                (DisplayRaces.GNOME, Genders.Male) => 25062,
                (DisplayRaces.GNOME, Genders.Female) => 13990,  
                (DisplayRaces.TROLL, Genders.Male) => 13995,     
                (DisplayRaces.TROLL, Genders.Female) => 25071, 
                (DisplayRaces.GOBLIN, Genders.Male) => 25073, 
                (DisplayRaces.GOBLIN, Genders.Female) => 25074,  
                (DisplayRaces.BLOOD_ELF, Genders.Male) => 25066,   
                (DisplayRaces.BLOOD_ELF, Genders.Female) => 13988,  
                (DisplayRaces.DRAENEI, Genders.Male) => 25064,      
                (DisplayRaces.DRAENEI, Genders.Female) => 13989,   
                (DisplayRaces.WORGEN, Genders.Male) => 25063,     
                (DisplayRaces.WORGEN, Genders.Female) => 25072,      
                (DisplayRaces.DARK_IRON_DWARF, Genders.Male) => 147484,   
                (DisplayRaces.DARK_IRON_DWARF, Genders.Female) => 147483,  
                (DisplayRaces.MAGHAR_ORC, Genders.Male) => 147486,           
                (DisplayRaces.MAGHAR_ORC, Genders.Female) => 147485,        
                (DisplayRaces.PANDAREN, Genders.Male) => 29405,        
                (DisplayRaces.PANDAREN, Genders.Female) => 29406,      
                (DisplayRaces.KUL_TIRAN, Genders.Male) => 150550,         
                (DisplayRaces.KUL_TIRAN, Genders.Female) => 150551,    
                (DisplayRaces.NIGHTBORNE, Genders.Male) => 143285,       
                (DisplayRaces.NIGHTBORNE, Genders.Female) => 143286,       
                (DisplayRaces.VOID_ELF, Genders.Male) => 143281,        
                (DisplayRaces.VOID_ELF, Genders.Female) => 143282,
                (DisplayRaces.MECHAGNOME, Genders.Male) => 150558,  
                (DisplayRaces.MECHAGNOME, Genders.Female) => 150559, 
                (DisplayRaces.VULPERA, Genders.Male) => 150554,            
                (DisplayRaces.VULPERA, Genders.Female) => 150555,          
                (DisplayRaces.ZANDALARI_TROLL, Genders.Male) => 150557,      
                (DisplayRaces.ZANDALARI_TROLL, Genders.Female) => 150556,     
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Male) => 143283,  
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Female) => 143284, 
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Male) => 143287,  
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Female) => 143288, 

                _ => null
            };
        }

        public static int? GetDisplayInfoIdByRaceAndGenders(this CreatureDisplayInfo creatureDisplayInfo, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.HUMAN, Genders.Male) => 37925, 
                (DisplayRaces.HUMAN, Genders.Female) => 37926, 
                (DisplayRaces.ORC, Genders.Male) => 37920, 
                (DisplayRaces.ORC, Genders.Female) => 20316,  
                (DisplayRaces.DWARF, Genders.Male) => 20317,     
                (DisplayRaces.DWARF, Genders.Female) => 37918,      
                (DisplayRaces.NIGHT_ELF, Genders.Male) => 20318,   
                (DisplayRaces.NIGHT_ELF, Genders.Female) => 37919, 
                (DisplayRaces.UNDEAD, Genders.Male) => 37923, 
                (DisplayRaces.UNDEAD, Genders.Female) => 37924, 
                (DisplayRaces.TAUREN, Genders.Male) => 20319, 
                (DisplayRaces.TAUREN, Genders.Female) => 37921, 
                (DisplayRaces.GNOME, Genders.Male) => 37913, 
                (DisplayRaces.GNOME, Genders.Female) => 20320,    
                (DisplayRaces.TROLL, Genders.Male) => 20321,   
                (DisplayRaces.TROLL, Genders.Female) => 37922,
                (DisplayRaces.GOBLIN, Genders.Male) => 37927,     
                (DisplayRaces.GOBLIN, Genders.Female) => 37928,       
                (DisplayRaces.BLOOD_ELF, Genders.Male) => 37917,  
                (DisplayRaces.BLOOD_ELF, Genders.Female) => 20322, 
                (DisplayRaces.DRAENEI, Genders.Male) => 37916, 
                (DisplayRaces.DRAENEI, Genders.Female) => 20323,        
                (DisplayRaces.WORGEN, Genders.Male) => 37915,   
                (DisplayRaces.WORGEN, Genders.Female) => 37914,  
                (DisplayRaces.DARK_IRON_DWARF, Genders.Male) => 88409,    
                (DisplayRaces.DARK_IRON_DWARF, Genders.Female) => 88408,  
                (DisplayRaces.MAGHAR_ORC, Genders.Male) => 88420,  
                (DisplayRaces.MAGHAR_ORC, Genders.Female) => 88410, 
                (DisplayRaces.PANDAREN, Genders.Male) => 47735,    
                (DisplayRaces.PANDAREN, Genders.Female) => 47736,  
                (DisplayRaces.KUL_TIRAN, Genders.Male) => 96145,
                (DisplayRaces.KUL_TIRAN, Genders.Female) => 96146,  
                (DisplayRaces.NIGHTBORNE, Genders.Male) => 82375,  
                (DisplayRaces.NIGHTBORNE, Genders.Female) => 82376,  
                (DisplayRaces.VOID_ELF, Genders.Male) => 82371, 
                (DisplayRaces.VOID_ELF, Genders.Female) => 82372,
                (DisplayRaces.MECHAGNOME, Genders.Male) => 96151, 
                (DisplayRaces.MECHAGNOME, Genders.Female) => 96152, 
                (DisplayRaces.VULPERA, Genders.Male) => 96147,    
                (DisplayRaces.VULPERA, Genders.Female) => 96148,  
                (DisplayRaces.ZANDALARI_TROLL, Genders.Male) => 96149,   
                (DisplayRaces.ZANDALARI_TROLL, Genders.Female) => 96150, 
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Male) => 82373, 
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Female) => 82374,
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Male) => 82377, 
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Female) => 82378, 

                _ => null
            };
        }

        public static int? GetModelIdByRaceAndGenders(this CreatureDisplayInfoExtra creatureDisplayInfoExtra, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.HUMAN, Genders.Male) => 7661,
                (DisplayRaces.HUMAN, Genders.Female) => 7599,
                (DisplayRaces.ORC, Genders.Male) => 6838,
                (DisplayRaces.ORC, Genders.Female) => 7200,
                (DisplayRaces.DWARF, Genders.Male) => 5408,
                (DisplayRaces.DWARF, Genders.Female) => 7203,
                (DisplayRaces.NIGHT_ELF, Genders.Male) => 7369,
                (DisplayRaces.NIGHT_ELF, Genders.Female) => 7300,
                (DisplayRaces.UNDEAD, Genders.Male) => 7233,
                (DisplayRaces.UNDEAD, Genders.Female) => 7578,
                (DisplayRaces.TAUREN, Genders.Male) => 7399,
                (DisplayRaces.TAUREN, Genders.Female) => 7576,
                (DisplayRaces.GNOME, Genders.Male) => 6837,
                (DisplayRaces.GNOME, Genders.Female) => 7130,
                (DisplayRaces.TROLL, Genders.Male) => 7778,
                (DisplayRaces.TROLL, Genders.Female) => 7793,
                (DisplayRaces.GOBLIN, Genders.Male) => 831,
                (DisplayRaces.GOBLIN, Genders.Female) => 832,
                (DisplayRaces.BLOOD_ELF, Genders.Male) => 8102,
                (DisplayRaces.BLOOD_ELF, Genders.Female) => 8103,
                (DisplayRaces.DRAENEI, Genders.Male) => 7629,
                (DisplayRaces.DRAENEI, Genders.Female) => 7692,
                (DisplayRaces.WORGEN, Genders.Male) => 3141,
                (DisplayRaces.WORGEN, Genders.Female) => 3142,
                (DisplayRaces.DARK_IRON_DWARF, Genders.Male) => 10784,
                (DisplayRaces.DARK_IRON_DWARF, Genders.Female) => 10785,
                (DisplayRaces.MAGHAR_ORC, Genders.Male) => 10844,
                (DisplayRaces.MAGHAR_ORC, Genders.Female) => 10845,
                (DisplayRaces.PANDAREN, Genders.Male) => 3967,
                (DisplayRaces.PANDAREN, Genders.Female) => 3968,
                (DisplayRaces.KUL_TIRAN, Genders.Male) => 10531,
                (DisplayRaces.KUL_TIRAN, Genders.Female) => 10532,
                (DisplayRaces.NIGHTBORNE, Genders.Male) => 9930,
                (DisplayRaces.NIGHTBORNE, Genders.Female) => 9931,
                (DisplayRaces.VOID_ELF, Genders.Male) => 9934,
                (DisplayRaces.VOID_ELF, Genders.Female) => 9935,
                (DisplayRaces.MECHAGNOME, Genders.Male) => 11488,
                (DisplayRaces.MECHAGNOME, Genders.Female) => 11489,
                (DisplayRaces.VULPERA, Genders.Male) => 10786,
                (DisplayRaces.VULPERA, Genders.Female) => 10787,
                (DisplayRaces.ZANDALARI_TROLL, Genders.Male) => 10394,
                (DisplayRaces.ZANDALARI_TROLL, Genders.Female) => 10395,
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Male) => 9936,
                (DisplayRaces.LIGHTFORGED_DRAENEI, Genders.Female) => 9937,
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Male) => 9932,
                (DisplayRaces.HIGHMOUNTAIN_TAUREN, Genders.Female) => 9933,

                _ => null
            };
        }
    }
}
