using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models;
using CreatureCreator.Infrastructure.DtoModels;
using CreatureCreator.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.Helpers
{
    public class DatabaseHelper
    {
        int _verifiedBuild;

        public long TableHashCreatureDisplayInfoOption { get; } = 2294684390;
        public long TableHashNpcModelItemSlotDisplayInfo { get; } = 1992314007;
        public long TableHashCreatureDisplayInfoExtra { get; } = 1722140087;
        public long TableHashCreatureDisplayInfo { get; } = 3218799089;



        public DatabaseHelper(int verifiedBuild)
        {
            _verifiedBuild = verifiedBuild;
        }

        public CreatureTemplate CreateCreatureTemplate(CreatureDto creature)
        {
            return new CreatureTemplate()
            {
                Entry = creature.Id,
                Name = creature.Name,
                Faction = creature.Faction,
                MaxLevel = creature.Level,
                MinLevel = creature.Level,
                Rank = creature.Rank,
                SubName = creature.SubName,
                Type = creature.CreatureType,
                UnitClass = creature.CreatureUnitClass,
                Scale = creature.Scale,
                HealthModifier = creature.HealthModifier,
                DamageModifier = creature.DamageModifier,
                ArmorModifier = creature.ArmorModifier,
                UnitFlags = (long)creature.UnitFlags,
                UnitFlags2 = (long)creature.UnitFlags2,
                UnitFlags3 = (long)creature.UnitFlags3,
                FlagsExtra = (long)creature.FlagsExtra,
                RegenHealth = creature.RegenHealth,

                VerifiedBuild = _verifiedBuild,

                // Custom default values
                AiName = "SmartAI",
                BaseAttackTime = 2000,
                BaseVariance = 1,
                ExperienceModifier = 1,
                HealthModifierExtra = 1,
                HoverHeight = 1,
                ManaModifier = 1,
                ManaModifierExtra = 1,
                RangeAttackTime = 2000,
                RangeVariance = 1,

                SpeedRun = 1.14286,
                SpeedWalk = 1
            };
        }

        public CreatureTemplate UpdateCreatureTemplate(CreatureDto creature, CreatureTemplate creatureTemplate)
        {
            creatureTemplate.Name = creature.Name;
            creatureTemplate.Faction = creature.Faction;
            creatureTemplate.MaxLevel = creature.Level;
            creatureTemplate.MinLevel = creature.Level;
            creatureTemplate.Rank = creature.Rank;
            creatureTemplate.SubName = creature.SubName;
            creatureTemplate.Type = creature.CreatureType;
            creatureTemplate.UnitClass = creature.CreatureUnitClass;
            creatureTemplate.Scale = creature.Scale;
            creatureTemplate.HealthModifier = creature.HealthModifier;
            creatureTemplate.DamageModifier = creature.DamageModifier;
            creatureTemplate.ArmorModifier = creature.ArmorModifier;
            creatureTemplate.UnitFlags = (long)creature.UnitFlags;
            creatureTemplate.UnitFlags2 = (long)creature.UnitFlags2;
            creatureTemplate.UnitFlags3 = (long)creature.UnitFlags3;
            creatureTemplate.FlagsExtra = (long)creature.FlagsExtra;
            creatureTemplate.RegenHealth = creature.RegenHealth;

            return creatureTemplate;
        }

        public CreatureEquipTemplate CreateCreatureEquipTemplate(CreatureDto creature)
        {
            return new CreatureEquipTemplate()
            {
                Id = 1,
                CreatureId = creature.Id,
                ItemId1 = creature.MainHandItemId ?? 0,
                AppearanceModId1 = creature.MainHandItemAppearanceModifierId ?? 0,
                ItemVisual1 = creature.MainHandItemVisual ?? 0,
                ItemId2 = creature.OffHandItemId ?? 0,
                AppearanceModId2 = creature.OffHandItemAppearanceModifierId ?? 0,
                ItemVisual2 = creature.OffHandItemVisual ?? 0,
                ItemId3 = creature.RangedItemId ?? 0,
                AppearanceModId3 = creature.RangedItemAppearanceModifierId ?? 0,
                ItemVisual3 = creature.RangedItemVisual ?? 0,
                VerifiedBuild = _verifiedBuild
            };
        }

        public CreatureEquipTemplate UpdateCreatureEquipTemplate(CreatureDto creature, CreatureEquipTemplate creatureEquipTemplate)
        {
            creatureEquipTemplate.ItemId1 = creature.MainHandItemId ?? 0;
            creatureEquipTemplate.AppearanceModId1 = creature.MainHandItemAppearanceModifierId ?? 0;
            creatureEquipTemplate.ItemVisual1 = creature.MainHandItemVisual ?? 0;

            creatureEquipTemplate.ItemId2 = creature.OffHandItemId ?? 0;
            creatureEquipTemplate.AppearanceModId2 = creature.OffHandItemAppearanceModifierId ?? 0;
            creatureEquipTemplate.ItemVisual2 = creature.OffHandItemVisual ?? 0;

            creatureEquipTemplate.ItemId3 = creature.RangedItemId ?? 0;
            creatureEquipTemplate.AppearanceModId3 = creature.RangedItemAppearanceModifierId ?? 0;
            creatureEquipTemplate.ItemVisual3 = creature.RangedItemVisual ?? 0;

            return creatureEquipTemplate;
        }

        public CreatureTemplateModel CreateCreatureTemplateModel(CreatureDto creature)
        {
            return new CreatureTemplateModel()
            {
                CreatureId = creature.Id,
                CreatureDisplayId = creature.Id,
                Probability = 1,
                DisplayScale = 1,
                VerifiedBuild = _verifiedBuild
            };
        }

        public CreatureTemplateModel UpdateCreatureTemplateModel(CreatureDto creature, CreatureTemplateModel creatureTemplateModel)
        {
            // There are currently no values in CreatureTemplateModel that are customizable. 
            return creatureTemplateModel;
        }

        public CreatureModelInfo CreateCreatureModelInfo(CreatureDto creature)
        {
            return new CreatureModelInfo()
            {
                DisplayId = creature.Id,
                VerifiedBuild = _verifiedBuild
            };
        }

        public CreatureModelInfo UpdateCreatureModelInfo(CreatureDto creature, CreatureModelInfo creatureModelInfo)
        {
            // There are currently no values in CreatureTemplateModel that are customizable. 
            return creatureModelInfo;
        }

        public CreatureTemplateAddon CreateCreatureTemplateAddon(CreatureDto creature)
        {
            return new CreatureTemplateAddon()
            {
                Entry = creature.Id,
                Auras = string.Join(" ", creature.Auras)
            };
        }

        public CreatureTemplateAddon UpdateCreatureTemplateAddon(CreatureDto creature, CreatureTemplateAddon creatureTemplateAddon)
        {
            creatureTemplateAddon.Auras = string.Join(" ", creature.Auras);
            return creatureTemplateAddon;
        }

        public CreatureDisplayInfo CreateCreatureDisplayInfo(CreatureDto creature, List<HotfixData> hotfixes, int hotfixId)
        {
            hotfixes.Add(new HotfixData()
            {
                Id = hotfixes.Count > 0 ? hotfixes.Max(h => h.Id) + 1 : hotfixId,
                RecordId = creature.Id,
                Status = HotfixStatuses.VALID,
                TableHash = TableHashCreatureDisplayInfo,
                UniqueId = creature.Id,
                VerifiedBuild = _verifiedBuild
            });

            return new CreatureDisplayInfo()
            {
                Id = creature.Id,
                ExtendedDisplayInfoId = creature.Id,
                Gender = creature.Gender,
                ModelId = ModelHelper.GetModelIdByRaceAndGenders(creature.Race, creature.Gender, creature.Customizations) ?? throw new NotImplementedException(),
                UnarmedWeaponType = -1,
                CreatureModelAlpha = 255,
                CreatureModelScale = 1,
                PetInstanceScale = 1,
                SizeClass = 1,
                SoundId = creature.SoundId,
                VerifiedBuild = _verifiedBuild
            };
        }

        public List<NpcModelItemSlotDisplayInfo> CreateNpcModelItemSlotDisplayInfos(CreatureDto creature, List<HotfixData> hotfixes, int hotfixId)
        {
            int npcModelId = creature.Id;

            var result = new List<NpcModelItemSlotDisplayInfo>();
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.HEAD,
                ItemDisplayInfoId = creature.HeadItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.HEAD,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.SHOULDERS,
                ItemDisplayInfoId = creature.ShouldersItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.SHOULDERS,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.SHIRT,
                ItemDisplayInfoId = creature.ShirtItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.SHIRT,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.CHEST,
                ItemDisplayInfoId = creature.ChestItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.CHEST,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.WAIST,
                ItemDisplayInfoId = creature.WaistItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.WAIST,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.LEGS,
                ItemDisplayInfoId = creature.LegsItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.LEGS,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.FEET,
                ItemDisplayInfoId = creature.FeetItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.FEET,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.WRISTS,
                ItemDisplayInfoId = creature.WristsItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.WRISTS,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.HANDS,
                ItemDisplayInfoId = creature.HandsItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.HANDS,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.TABARD,
                ItemDisplayInfoId = creature.TabardItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.TABARD,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.BACK,
                ItemDisplayInfoId = creature.BackItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.BACK,
                NpcModelId = npcModelId
            });
            result.Add(new NpcModelItemSlotDisplayInfo()
            {
                Id = creature.Id + (int)ArmorSlots.QUIVER,
                ItemDisplayInfoId = creature.QuiverItemDisplayInfoId ?? 0,
                ItemSlot = ArmorSlots.QUIVER,
                NpcModelId = npcModelId
            });

            foreach (var item in result)
            {
                hotfixes.Add(new HotfixData()
                {
                    Id = hotfixes.Count > 0 ? hotfixes.Max(h => h.Id) + 1 : hotfixId,
                    RecordId = item.Id,
                    Status = HotfixStatuses.VALID,
                    TableHash = TableHashNpcModelItemSlotDisplayInfo,
                    UniqueId = creature.Id,
                    VerifiedBuild = _verifiedBuild
                });
            }

            return result;
        }

        public List<CreatureDisplayInfoOption> CreateCreatureDisplayInfoOptions(CreatureDto creature, List<HotfixData> hotfixes, int hotfixId)
        {
            int creatureDisplayInfoExtraId = creature.Id;
            var result = new List<CreatureDisplayInfoOption>();
            foreach (var customization in creature.Customizations)
            {
                result.Add(new CreatureDisplayInfoOption()
                {
                    Id = creature.Id + customization.Key,
                    ChrCustomizationOptionId = customization.Key,
                    ChrCustomizationChoiceId = customization.Value,
                    CreatureDisplayInfoExtraId = creatureDisplayInfoExtraId
                });
                hotfixes.Add(new HotfixData()
                {
                    Id = hotfixes.Count > 0 ? hotfixes.Max(h => h.Id) + 1 : hotfixId,
                    RecordId = creature.Id + customization.Key,
                    Status = HotfixStatuses.VALID,
                    UniqueId = creature.Id,
                    TableHash = TableHashCreatureDisplayInfoOption,
                    VerifiedBuild = _verifiedBuild
                });
            }
            return result;
        }

        public CreatureDisplayInfoExtra CreateCreatureDisplayInfoExtra(CreatureDto creature, List<HotfixData> hotfixes, int hotfixId)
        {
            hotfixes.Add(new HotfixData()
            {
                Id = hotfixes.Count > 0 ? hotfixes.Max(h => h.Id) + 1 : hotfixId,
                RecordId = creature.Id,
                Status = HotfixStatuses.VALID,
                UniqueId = creature.Id,
                TableHash = TableHashCreatureDisplayInfoExtra,
                VerifiedBuild = _verifiedBuild
            });
            return new CreatureDisplayInfoExtra()
            {
                Id = creature.Id,
                DisplayRaceId = creature.Race,
                DisplaySexId = creature.Gender
            };
        }
    }
}
