using CreatureCreator.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.DtoModels
{
    public class CreatureDto
    {
        public CreatureDto(int id, Genders gender, Races race)
        {
            Id = id;
            Gender = gender;
            Race = race;
        }

        public int Id { get; set; }
        public Genders Gender { get; set; }
        public Races Race { get; set; }
        public string? Name { get; set; } = "New Creature";
        public string? SubName { get; set; } = null;
        public int Level { get; set; } = 30;
        public int Faction { get; set; } = 1;
        public double Scale { get; set; } = 1;
        public Ranks Rank { get; set; } = Ranks.NORMAL;
        public CreatureTypes CreatureType { get; set; } = CreatureTypes.HUMANOID;
        public CreatureUnitClasses CreatureUnitClass { get; set; } = CreatureUnitClasses.WARRIOR;
        public UnitFlags UnitFlags { get; set; } = 0;
        public UnitFlags2 UnitFlags2 { get; set; } = 0;
        public UnitFlags3 UnitFlags3 { get; set; } = 0;
        public FlagsExtra FlagsExtra { get; set; } = 0;
        public double HealthModifier { get; set; } = 1;
        public double DamageModifier { get; set; } = 1;
        public double ArmorModifier { get; set; } = 1;
        public int SoundId { get; set; } = 0;
        public bool RegenHealth { get; set; } = true;
        public List<int> Auras { get; set; } = new List<int>();

        // Armor
        public int? HeadItemDisplayInfoId { get; set; }
        public int? ShouldersItemDisplayInfoId { get; set; }
        public int? BackItemDisplayInfoId { get; set; }
        public int? ShirtItemDisplayInfoId { get; set; }
        public int? TabardItemDisplayInfoId { get; set; }
        public int? ChestItemDisplayInfoId { get; set; }
        public int? HandsItemDisplayInfoId { get; set; }
        public int? WaistItemDisplayInfoId { get; set; }
        public int? WristsItemDisplayInfoId { get; set; }
        public int? LegsItemDisplayInfoId { get; set; }
        public int? FeetItemDisplayInfoId { get; set; }
        public int? QuiverItemDisplayInfoId { get; set; }

        // Weapons
        public int? MainHandItemId { get; set; }
        public int? MainHandItemVisual { get; set; }
        public int? MainHandItemAppearanceModifierId { get; set; }
        public int? OffHandItemId { get; set; }
        public int? OffHandItemVisual { get; set; }
        public int? OffHandItemAppearanceModifierId { get; set; }
        public int? RangedItemId { get; set; }
        public int? RangedItemVisual { get; set; }
        public int? RangedItemAppearanceModifierId { get; set; }

        public Dictionary<int, int> Customizations { get; set; } = new Dictionary<int, int>();

        public bool IsCustomizable { get; set; } = true;
        public bool IsUpdate { get; set; } = false;

    }
}
