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

        public static int GetDisplayInfoExtraIdByRaceAndGenders(this CreatureDisplayInfoExtra creatureDisplayInfoExtra, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.Human, Genders.Male) => 25076,
                (DisplayRaces.Human, Genders.Female) => 25075,
                (DisplayRaces.Orc, Genders.Male) => 25067,       
                (DisplayRaces.Orc, Genders.Female) => 13991,  
                (DisplayRaces.Dwarf, Genders.Male) => 13992, 
                (DisplayRaces.Dwarf, Genders.Female) => 25068,  
                (DisplayRaces.NightElf, Genders.Male) => 13993,   
                (DisplayRaces.NightElf, Genders.Female) => 25069,
                (DisplayRaces.Undead, Genders.Male) => 25077,  
                (DisplayRaces.Undead, Genders.Female) => 25078, 
                (DisplayRaces.Tauren, Genders.Male) => 13994,  
                (DisplayRaces.Tauren, Genders.Female) => 25070,   
                (DisplayRaces.Gnome, Genders.Male) => 25062,
                (DisplayRaces.Gnome, Genders.Female) => 13990,  
                (DisplayRaces.Troll, Genders.Male) => 13995,     
                (DisplayRaces.Troll, Genders.Female) => 25071, 
                (DisplayRaces.Goblin, Genders.Male) => 25073, 
                (DisplayRaces.Goblin, Genders.Female) => 25074,  
                (DisplayRaces.BloodElf, Genders.Male) => 25066,   
                (DisplayRaces.BloodElf, Genders.Female) => 13988,  
                (DisplayRaces.Draenei, Genders.Male) => 25064,      
                (DisplayRaces.Draenei, Genders.Female) => 13989,   
                (DisplayRaces.Worgen, Genders.Male) => 25063,     
                (DisplayRaces.Worgen, Genders.Female) => 25072,      
                (DisplayRaces.DarkIronDwarf, Genders.Male) => 147484,   
                (DisplayRaces.DarkIronDwarf, Genders.Female) => 147483,  
                (DisplayRaces.MagharOrc, Genders.Male) => 147486,           
                (DisplayRaces.MagharOrc, Genders.Female) => 147485,        
                (DisplayRaces.Pandaren, Genders.Male) => 29405,        
                (DisplayRaces.Pandaren, Genders.Female) => 29406,      
                (DisplayRaces.KulTiran, Genders.Male) => 150550,         
                (DisplayRaces.KulTiran, Genders.Female) => 150551,    
                (DisplayRaces.Nightborne, Genders.Male) => 143285,       
                (DisplayRaces.Nightborne, Genders.Female) => 143286,       
                (DisplayRaces.VoidElf, Genders.Male) => 143281,        
                (DisplayRaces.VoidElf, Genders.Female) => 143282,
                (DisplayRaces.Mechagnome, Genders.Male) => 150558,  
                (DisplayRaces.Mechagnome, Genders.Female) => 150559, 
                (DisplayRaces.Vulpera, Genders.Male) => 150554,            
                (DisplayRaces.Vulpera, Genders.Female) => 150555,          
                (DisplayRaces.ZandalariTroll, Genders.Male) => 150557,      
                (DisplayRaces.ZandalariTroll, Genders.Female) => 150556,     
                (DisplayRaces.LightforgedDraenei, Genders.Male) => 143283,  
                (DisplayRaces.LightforgedDraenei, Genders.Female) => 143284, 
                (DisplayRaces.HighmountaunTauren, Genders.Male) => 143287,  
                (DisplayRaces.HighmountaunTauren, Genders.Female) => 143288, 

                //_ => null
            };
        }

        public static int GetDisplayInfoIdByRaceAndGenders(this CreatureDisplayInfoExtra creatureDisplayInfoExtra, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.Human, Genders.Male) => 37925, 
                (DisplayRaces.Human, Genders.Female) => 37926, 
                (DisplayRaces.Orc, Genders.Male) => 37920, 
                (DisplayRaces.Orc, Genders.Female) => 20316,  
                (DisplayRaces.Dwarf, Genders.Male) => 20317,     
                (DisplayRaces.Dwarf, Genders.Female) => 37918,      
                (DisplayRaces.NightElf, Genders.Male) => 20318,   
                (DisplayRaces.NightElf, Genders.Female) => 37919, 
                (DisplayRaces.Undead, Genders.Male) => 37923, 
                (DisplayRaces.Undead, Genders.Female) => 37924, 
                (DisplayRaces.Tauren, Genders.Male) => 20319, 
                (DisplayRaces.Tauren, Genders.Female) => 37921, 
                (DisplayRaces.Gnome, Genders.Male) => 37913, 
                (DisplayRaces.Gnome, Genders.Female) => 20320,    
                (DisplayRaces.Troll, Genders.Male) => 20321,   
                (DisplayRaces.Troll, Genders.Female) => 37922,
                (DisplayRaces.Goblin, Genders.Male) => 37927,     
                (DisplayRaces.Goblin, Genders.Female) => 37928,       
                (DisplayRaces.BloodElf, Genders.Male) => 37917,  
                (DisplayRaces.BloodElf, Genders.Female) => 20322, 
                (DisplayRaces.Draenei, Genders.Male) => 37916, 
                (DisplayRaces.Draenei, Genders.Female) => 20323,        
                (DisplayRaces.Worgen, Genders.Male) => 37915,   
                (DisplayRaces.Worgen, Genders.Female) => 37914,  
                (DisplayRaces.DarkIronDwarf, Genders.Male) => 88409,    
                (DisplayRaces.DarkIronDwarf, Genders.Female) => 88408,  
                (DisplayRaces.MagharOrc, Genders.Male) => 88420,  
                (DisplayRaces.MagharOrc, Genders.Female) => 88410, 
                (DisplayRaces.Pandaren, Genders.Male) => 47735,    
                (DisplayRaces.Pandaren, Genders.Female) => 47736,  
                (DisplayRaces.KulTiran, Genders.Male) => 96145,
                (DisplayRaces.KulTiran, Genders.Female) => 96146,  
                (DisplayRaces.Nightborne, Genders.Male) => 82375,  
                (DisplayRaces.Nightborne, Genders.Female) => 82376,  
                (DisplayRaces.VoidElf, Genders.Male) => 82371, 
                (DisplayRaces.VoidElf, Genders.Female) => 82372,
                (DisplayRaces.Mechagnome, Genders.Male) => 96151, 
                (DisplayRaces.Mechagnome, Genders.Female) => 96152, 
                (DisplayRaces.Vulpera, Genders.Male) => 96147,    
                (DisplayRaces.Vulpera, Genders.Female) => 96148,  
                (DisplayRaces.ZandalariTroll, Genders.Male) => 96149,   
                (DisplayRaces.ZandalariTroll, Genders.Female) => 96150, 
                (DisplayRaces.LightforgedDraenei, Genders.Male) => 82373, 
                (DisplayRaces.LightforgedDraenei, Genders.Female) => 82374,
                (DisplayRaces.HighmountaunTauren, Genders.Male) => 82377, 
                (DisplayRaces.HighmountaunTauren, Genders.Female) => 82378, 

                //_ => null
            };
        }

        public static int GetModelIdByRaceAndGenders(this CreatureDisplayInfoExtra creatureDisplayInfoExtra, DisplayRaces displayRace, Genders Genders)
        {
            return (displayRace, Genders) switch
            {
                (DisplayRaces.Human, Genders.Male) => 7661,
                (DisplayRaces.Human, Genders.Female) => 7599,
                (DisplayRaces.Orc, Genders.Male) => 6838,
                (DisplayRaces.Orc, Genders.Female) => 7200,
                (DisplayRaces.Dwarf, Genders.Male) => 5408,
                (DisplayRaces.Dwarf, Genders.Female) => 7203,
                (DisplayRaces.NightElf, Genders.Male) => 7369,
                (DisplayRaces.NightElf, Genders.Female) => 7300,
                (DisplayRaces.Undead, Genders.Male) => 7233,
                (DisplayRaces.Undead, Genders.Female) => 7578,
                (DisplayRaces.Tauren, Genders.Male) => 7399,
                (DisplayRaces.Tauren, Genders.Female) => 7576,
                (DisplayRaces.Gnome, Genders.Male) => 6837,
                (DisplayRaces.Gnome, Genders.Female) => 7130,
                (DisplayRaces.Troll, Genders.Male) => 7778,
                (DisplayRaces.Troll, Genders.Female) => 7793,
                (DisplayRaces.Goblin, Genders.Male) => 831,
                (DisplayRaces.Goblin, Genders.Female) => 832,
                (DisplayRaces.BloodElf, Genders.Male) => 8102,
                (DisplayRaces.BloodElf, Genders.Female) => 8103,
                (DisplayRaces.Draenei, Genders.Male) => 7629,
                (DisplayRaces.Draenei, Genders.Female) => 7692,
                (DisplayRaces.Worgen, Genders.Male) => 3141,
                (DisplayRaces.Worgen, Genders.Female) => 3142,
                (DisplayRaces.DarkIronDwarf, Genders.Male) => 10784,
                (DisplayRaces.DarkIronDwarf, Genders.Female) => 10785,
                (DisplayRaces.MagharOrc, Genders.Male) => 10844,
                (DisplayRaces.MagharOrc, Genders.Female) => 10845,
                (DisplayRaces.Pandaren, Genders.Male) => 3967,
                (DisplayRaces.Pandaren, Genders.Female) => 3968,
                (DisplayRaces.KulTiran, Genders.Male) => 10531,
                (DisplayRaces.KulTiran, Genders.Female) => 10532,
                (DisplayRaces.Nightborne, Genders.Male) => 9930,
                (DisplayRaces.Nightborne, Genders.Female) => 9931,
                (DisplayRaces.VoidElf, Genders.Male) => 9934,
                (DisplayRaces.VoidElf, Genders.Female) => 9935,
                (DisplayRaces.Mechagnome, Genders.Male) => 11488,
                (DisplayRaces.Mechagnome, Genders.Female) => 11489,
                (DisplayRaces.Vulpera, Genders.Male) => 10786,
                (DisplayRaces.Vulpera, Genders.Female) => 10787,
                (DisplayRaces.ZandalariTroll, Genders.Male) => 10394,
                (DisplayRaces.ZandalariTroll, Genders.Female) => 10395,
                (DisplayRaces.LightforgedDraenei, Genders.Male) => 9936,
                (DisplayRaces.LightforgedDraenei, Genders.Female) => 9937,
                (DisplayRaces.HighmountaunTauren, Genders.Male) => 9932,
                (DisplayRaces.HighmountaunTauren, Genders.Female) => 9933,

                //_ => null
            };
        }
    }
}
