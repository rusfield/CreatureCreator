﻿using CreatureCreator.Core.Providers;
using CreatureCreator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreatureCreator.MySqlProvider.EntityFrameworkCore.Clients;
using CreatureCreator.Db2Provider.WowToolsFiles.Clients;
using CreatureCreator.Infrastructure.DtoModels;
using CreatureCreator.Core.Enums;
using CreatureCreator.Infrastructure.Extensions;
using CreatureCreator.Infrastructure.Helpers;

namespace CreatureCreator.Infrastructure.Services
{
    public class CreatureCreatorService
    {
        IMySqlProvider _mySql;
        IDb2Provider _db2;


        int _verifiedBuild;
        int _idRangeFrom;
        int _idRangeTo;
        double _idSize;

        public CreatureCreatorService(string mySqlServer, string mySqlUser, string mySqlPassword, string mySqlWorldSchemaName, string mySqlCharactersSchemaName, string mySqlHotfixesSchemaNames, int verifiedBuild, int idRangeFrom, int idRangeTo)
        {
            _mySql = new MySqlClient(mySqlServer, mySqlUser, mySqlPassword, mySqlWorldSchemaName, mySqlCharactersSchemaName, mySqlHotfixesSchemaNames);
            _db2 = new Db2Client();

            _verifiedBuild = verifiedBuild;
            _idRangeFrom = idRangeFrom;
            _idRangeTo = idRangeTo;

            // Each creature will take up this many ID slots.
            // The reason it is so big is because of customizations.
            // See docs for more explainations.
            // Changes may cause unexpected results and even loss of data.
            _idSize = 5000.0;
        }

        public List<ValidationStatusDto> ValidateCreature(CreatureDto creature)
        {
            var result = new List<ValidationStatusDto>();

            if (creature.Id >= _idRangeTo || creature.Id < _idRangeFrom)
            {
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateLevel,
                    Status = ValidationStatuses.ERROR,
                    Description = $"The level is not within the configurated range. It should be equal or greater than {_idRangeFrom} and lower than {_idRangeTo}"
                });
            }

            return result;
        }

        public async Task<int> GetNextIdAsync(bool quickScan = true)
        {
            // TODO:
            // quickScan TRUE will always find the top next Id.
            // quickScan FALSE should look for deleted slots between all IDs. 
            // Some research is needed to check whether hotfix_data may cause a conflict (or be ignored) for clients who have already loaded the deleted creature.

            int id = _idRangeFrom;
            var creaturesIdsInRange = await _mySql.GetManyAsync<CreatureTemplate>(c => c.Entry >= _idRangeFrom && c.Entry < _idRangeTo);
            if (creaturesIdsInRange.Count() > 0)
            {
                var maxId = creaturesIdsInRange.Max(c => c.Entry) + 1;
                id = (int)(Math.Ceiling(maxId / _idSize) * _idSize);
            }
            return id;
        }

        public async Task<List<ValidationStatusDto>> ValidateDatabase()
        {
            var result = new List<ValidationStatusDto>();
            var worldDbOk = await _mySql.WorldConnectionTestAsync();
            var hotfixesDbOk = await _mySql.HotfixesConnectionTestAsync();
            var charactersDbOk = await _mySql.CharactersConnectionTestAsync();

            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateWorldDb,
                Status = worldDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                Description = worldDbOk ? "" : "Unable to connect to World DB. Make sure MySQL is running and that the Schema name is set correctly."
            });
            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateCharactersDb,
                Status = charactersDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                Description = charactersDbOk ? "" : "Unable to connect to Characters DB. Make sure MySQL is running and that the Schema name is set correctly."
            });
            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateHotfixesDb,
                Status = hotfixesDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                Description = hotfixesDbOk ? "" : "Unable to connect to Hotfixes DB. Make sure MySQL is running and that the Schema name is set correctly."
            });


            return result;
        }

        public async Task DeleteCreatureAsync(int creatureId)
        {
            var creatureTemplate = await _mySql.GetAsync<CreatureTemplate>(c => c.Entry == creatureId);
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureId == creatureId);
            var creatureEquipTemplate = await _mySql.GetAsync<CreatureEquipTemplate>(c => c.CreatureId == creatureId);
            var creatureModelInfo = await _mySql.GetAsync<CreatureModelInfo>(c => c.DisplayId == creatureId);

            var creatureDisplayInfoExtra = await _mySql.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == creatureId);
            var npcModelItemSlotDisplayInfos = await _mySql.GetManyAsync<NpcModelItemSlotDisplayInfo>(c => c.Id >= creatureId && c.Id < creatureId + _idSize);
            var creatureDisplayInfoOptions = await _mySql.GetManyAsync<CreatureDisplayInfoOption>(c => c.Id >= creatureId && c.Id < creatureId + _idSize);
            var creatureDisplayInfo = await _mySql.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureId);

            var hotfixData = await _mySql.GetManyAsync<HotfixData>(h => h.UniqueId == creatureId);


            if (creatureTemplate != null)
                await _mySql.DeleteAsync(creatureTemplate);

            if (creatureTemplateModel != null)
                await _mySql.DeleteAsync(creatureTemplateModel);

            if (creatureEquipTemplate != null)
                await _mySql.DeleteAsync(creatureEquipTemplate);

            if (creatureModelInfo != null)
                await _mySql.DeleteAsync(creatureModelInfo);

            if (creatureDisplayInfoExtra != null)
                await _mySql.DeleteAsync(creatureDisplayInfoExtra);

            if (npcModelItemSlotDisplayInfos != null && npcModelItemSlotDisplayInfos.Count() > 0)
                await _mySql.DeleteManyAsync(npcModelItemSlotDisplayInfos);

            if (creatureDisplayInfoOptions != null && creatureDisplayInfoOptions.Count() > 0)
                await _mySql.DeleteManyAsync(creatureDisplayInfoOptions);

            if (creatureDisplayInfo != null)
                await _mySql.DeleteAsync(creatureDisplayInfo);

            if (hotfixData != null && hotfixData.Count() > 0)
                await _mySql.DeleteManyAsync(hotfixData);
        }

        public async Task SaveCreatureAsync(CreatureDto creature)
        {
            var existingCreature = await _mySql.GetAsync<CreatureTemplate>(c => c.Entry == creature.Id);
            var modelHelper = new ModelHelper(_verifiedBuild);
            if (existingCreature != null)
            {
                // Update
            }
            else
            {
                // Add
                var hotfixes = new List<HotfixData>();
                await _mySql.AddAsync(modelHelper.CreateCreatureTemplate(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureTemplateModel(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureEquipTemplate(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureModelInfo(creature));

                await _mySql.AddAsync(modelHelper.CreateCreatureDisplayInfoExtra(creature, hotfixes));
                await _mySql.AddManyAsync(modelHelper.CreateNpcModelItemSlotDisplayInfos(creature, hotfixes));
                await _mySql.AddManyAsync(modelHelper.CreateCreatureDisplayInfoOptions(creature, hotfixes));
                await _mySql.AddAsync(modelHelper.CreateCreatureDisplayInfo(creature, hotfixes));

                await _mySql.AddManyAsync(hotfixes);
            }
        }

        public async Task<List<DashboardCreatureDto>> GetCreatures()
        {
            var creatures = await _mySql.GetManyAsync<CreatureTemplate>(c => c.VerifiedBuild == _verifiedBuild);
            var result = new List<DashboardCreatureDto>();
            foreach (var creature in creatures)
            {
                var displayInfo = await _mySql.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == creature.Entry);
                if (displayInfo == null)
                    continue;
                result.Add(new DashboardCreatureDto()
                {
                    Id = creature.Entry,
                    Name = creature.Name,
                    Race = displayInfo.DisplayRaceId,
                    Gender = displayInfo.DisplaySexId
                });
            }
            return result;
        }

        public async Task<CreatureDto?> GetCreatureByIdAsync(int creatureId)
        {
            // This will only return the first model.
            // If the desired result has multiple displays, use the desired one directly instead.
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureId == creatureId);
            if (creatureTemplateModel != null)
            {
                var creature = await GetCreatureByDisplayIdAsync(creatureTemplateModel.CreatureDisplayId);
                if (creature != null)
                {
                    // Override the automatically generated Id
                    creature.Id = creatureId;
                    return creature;
                }
            }
            return null;
        }

        public async Task<CreatureDto?> GetCreatureByDisplayIdAsync(int creatureDisplayId)
        {
            CreatureTemplate? creatureTemplate = null;
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureDisplayId == creatureDisplayId);
            if (creatureTemplateModel != null)
            {
                creatureTemplate = await _mySql.GetAsync<CreatureTemplate>(c => c.Entry == creatureTemplateModel.CreatureId);
            }

            var displayInfo = await _mySql.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureDisplayId) ?? await _db2.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureDisplayId);
            if (displayInfo == null)
                return null;

            var displayInfoExtra = await _mySql.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == displayInfo.ExtendedDisplayInfoId) ?? await _db2.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == displayInfo.ExtendedDisplayInfoId);
            if (displayInfoExtra == null)
                return null;

            var availableCustomizations = await GetAvailableCustomizations(displayInfoExtra.DisplayRaceId, displayInfo.Gender);
            var hotfixCustomizations = await _mySql.GetManyAsync<CreatureDisplayInfoOption>(h => h.CreatureDisplayInfoExtraId == displayInfoExtra.Id);
            var db2Customizations = await _db2.GetManyAsync<CreatureDisplayInfoOption>(h => h.CreatureDisplayInfoExtraId == displayInfoExtra.Id);
            var customizations = new Dictionary<int, int>();

            foreach (var availableCustomization in availableCustomizations)
            {
                var hotfixCustomization = hotfixCustomizations.Where(hc => hc.ChrCustomizationOptionId == availableCustomization.Key.Id).FirstOrDefault();
                var db2Customization = db2Customizations.Where(dc => dc.ChrCustomizationOptionId == availableCustomization.Key.Id).FirstOrDefault();
                if (hotfixCustomization != null)
                {
                    customizations.Add(hotfixCustomization.ChrCustomizationOptionId, hotfixCustomization.ChrCustomizationChoiceId);
                }
                else if (db2Customization != null)
                {
                    customizations.Add(db2Customization.ChrCustomizationOptionId, db2Customization.ChrCustomizationChoiceId);
                }
                else
                {
                    customizations.Add(availableCustomization.Key.Id, availableCustomization.Value.First().Id);
                }
            }

            var result = new CreatureDto()
            {
                Id = await GetNextIdAsync(),
                CreatureType = creatureTemplate != null ? creatureTemplate.Type : CreatureTypes.HUMANOID,
                Faction = creatureTemplate != null ? creatureTemplate.Faction : 17,
                CreatureUnitClass = creatureTemplate != null ? creatureTemplate.UnitClass : CreatureUnitClasses.WARRIOR,
                Gender = displayInfo.Gender,
                Race = displayInfoExtra.DisplayRaceId,
                Customizations = customizations,
                Level = creatureTemplate != null ? creatureTemplate.MaxLevel : 30,
                Name = creatureTemplate != null ? creatureTemplate.Name : null,
                SubName = creatureTemplate != null ? creatureTemplate.SubName : null,
                Scale = creatureTemplate != null ? creatureTemplate.Scale : 1.0,
                Rank = creatureTemplate != null ? creatureTemplate.Rank : Ranks.NORMAL
            };

            var hotfixEquipment = await _mySql.GetManyAsync<NpcModelItemSlotDisplayInfo>(npc => npc.NpcModelId == displayInfoExtra.Id);
            var db2Equipment = await _db2.GetManyAsync<NpcModelItemSlotDisplayInfo>(npc => npc.NpcModelId == displayInfoExtra.Id);

            for (int i = 0; i <= Enum.GetValues(typeof(ArmorSlots)).Cast<int>().Max(); i++)
            {
                var itemSlot = (ArmorSlots)i;
                var hotfixItem = hotfixEquipment.FirstOrDefault(h => (int)h.ItemSlot == i);
                var db2Item = db2Equipment.FirstOrDefault(d => (int)d.ItemSlot == i);
                var item = hotfixItem != null ? hotfixItem : (db2Item != null ? db2Item : null);
                if (item == null || item.ItemDisplayInfoId == 0)
                    continue;

                switch (itemSlot)
                {
                    case ArmorSlots.HEAD:
                        result.HeadItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.SHOULDERS:
                        result.ShouldersItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.CHEST:
                        result.ChestItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.WAIST:
                        result.WaistItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.HANDS:
                        result.HandsItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.TABARD:
                        result.TabardItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.SHIRT:
                        result.ShirtItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.BACK:
                        result.BackItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.WRISTS:
                        result.WristsItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.LEGS:
                        result.LegsItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.FEET:
                        result.FeetItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                    case ArmorSlots.QUIVER:
                        result.QuiverItemDisplayInfoId = item.ItemDisplayInfoId;
                        break;
                }
            }

            if (creatureTemplate != null)
            {
                var creatureEquipTemplate = await _mySql.GetAsync<CreatureEquipTemplate>(c => c.CreatureId == creatureTemplate.Entry);
                if (creatureEquipTemplate != null)
                {
                    if (creatureEquipTemplate.ItemId1 > 0)
                        result.MainHandItemId = creatureEquipTemplate.ItemId1;
                    if (creatureEquipTemplate.AppearanceModId1 > 0)
                        result.MainHandItemAppearanceModifierId = creatureEquipTemplate.AppearanceModId1;
                    if (creatureEquipTemplate.ItemVisual1 > 0)
                        result.MainHandItemVisual = creatureEquipTemplate.ItemVisual1;

                    if (creatureEquipTemplate.ItemId2 > 0)
                        result.OffHandItemId = creatureEquipTemplate.ItemId2;
                    if (creatureEquipTemplate.AppearanceModId2 > 0)
                        result.OffHandItemAppearanceModifierId = creatureEquipTemplate.AppearanceModId2;
                    if (creatureEquipTemplate.ItemVisual2 > 0)
                        result.OffHandItemVisual = creatureEquipTemplate.ItemVisual2;

                    if (creatureEquipTemplate.ItemId3 > 0)
                        result.RangedHandItemId = creatureEquipTemplate.ItemId3;
                    if (creatureEquipTemplate.AppearanceModId3 > 0)
                        result.RangedHandItemAppearanceModifierId = creatureEquipTemplate.AppearanceModId3;
                    if (creatureEquipTemplate.ItemVisual3 > 0)
                        result.RangedHandItemVisual = creatureEquipTemplate.ItemVisual3;
                }
            }

            return result;
        }

        public async Task<CreatureDto?> GetCreatureByCharacterNameAsync(string characterName)
        {
            var character = await _mySql.GetAsync<Characters>(c => c.Name == characterName);
            if (character == null)
                return null;

            var characterCustomization = await _mySql.GetManyAsync<CharacterCustomizations>(c => c.Guid == character.Guid);
            var customizations = new Dictionary<int, int>();
            foreach (var customization in characterCustomization)
            {
                customizations.Add(customization.ChrCustomizationOptionId, customization.ChrCustomizationChoiceId);
            }
            var result = new CreatureDto()
            {
                CreatureType = CreatureTypes.HUMANOID,
                Id = await GetNextIdAsync(),
                Customizations = customizations,
                Gender = character.Gender,
                Level = character.Level,
                Scale = 1,
                Rank = Ranks.NORMAL,
                Name = character.Name,
                CreatureUnitClass = CreatureUnitClasses.WARRIOR,
                Race = character.Race
            };

            var characterItems = await _mySql.GetManyAsync<ItemInstance>(c => c.OwnerGuid == character.Guid);
            var characterEquipMap = await _mySql.GetManyAsync<CharacterInventory>(c => c.Guid == character.Guid);
            foreach (var equippedItem in characterEquipMap)
            {
                var item = characterItems.Where(i => i.Guid == equippedItem.Item).FirstOrDefault();
                if (item == null)
                    continue;

                int itemAppearanceModifierId = 0;
                if (!string.IsNullOrWhiteSpace(item.BonusListIds))
                {
                    var bonusListIds = item.BonusListIds.Trim().Split(' ').Select(int.Parse).ToList();
                    if (bonusListIds != null && bonusListIds.Any())
                    {
                        var itemBonus = await _db2.GetAsync<ItemBonus>(bonus => bonus.Type == 7 && bonusListIds.Any(bonusId => bonusId == bonus.ParentItemBonusListId));
                        if (itemBonus != null)
                        {
                            itemAppearanceModifierId = itemBonus.Value0;
                        }
                    }
                }

                var itemModifiedAppearance = await _db2.GetAsync<ItemModifiedAppearance>(c => c.ItemId == item.ItemEntry && c.ItemAppearanceModifierId == itemAppearanceModifierId);
                if (itemModifiedAppearance == null)
                    continue;

                var itemAppearance = await _db2.GetAsync<ItemAppearance>(c => c.Id == itemModifiedAppearance.ItemAppearanceId);
                if (itemAppearance == null)
                    continue;

                switch (equippedItem.Slot)
                {
                    case CharacterInventorySlots.HEAD:
                        result.HeadItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.BACK:
                        result.BackItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.SHOULDERS:
                        result.ShouldersItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.CHEST:
                        result.ChestItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.WAIST:
                        result.WaistItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.TABARD:
                        result.TabardItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.SHIRT:
                        result.ShirtItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.LEGS:
                        result.LegsItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.FEET:
                        result.FeetItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.WRISTS:
                        result.WristsItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.HANDS:
                        result.HandsItemDisplayInfoId = itemAppearance.ItemDisplayInfoId;
                        break;

                    case CharacterInventorySlots.MAIN_HAND:
                        result.MainHandItemId = item.ItemEntry;
                        result.MainHandItemAppearanceModifierId = itemAppearanceModifierId;
                        // TODO: Enchant
                        break;

                    case CharacterInventorySlots.OFF_HAND:
                        result.OffHandItemId = item.ItemEntry;
                        result.OffHandItemAppearanceModifierId = itemAppearanceModifierId;
                        // TODO: Enchant
                        break;

                    case CharacterInventorySlots.RANGED:
                        result.RangedHandItemId = item.ItemEntry;
                        result.RangedHandItemAppearanceModifierId = itemAppearanceModifierId;
                        // TODO: Enchant
                        break;
                };
            }

            return result;
        }

        public async Task<CreatureDto> GetCreatureByRaceAndGender(DisplayRaces race, Genders gender)
        {
            var id = await GetNextIdAsync();
            return new CreatureDto()
            {
                Race = race,
                Gender = gender,
                Id = id,
                Level = 30,
                Rank = Ranks.NORMAL,
                CreatureType = CreatureTypes.HUMANOID,
                CreatureUnitClass = CreatureUnitClasses.WARRIOR,
                Scale = 1,
                Customizations = new Dictionary<int, int>()
            };
        }

        public async Task<Dictionary<CustomizationOptionDto, List<CustomizationChoiceDto>>> GetAvailableCustomizations(DisplayRaces race, Genders gender)
        {
            var chrModel = new ChrModels().ConvertFromRaceAndGender(race, gender);
            var result = new Dictionary<CustomizationOptionDto, List<CustomizationChoiceDto>>();
            var options = await _db2.GetManyAsync<ChrCustomizationOption>(c => c.ChrModelId == chrModel);
            foreach (var option in options)
            {
                var choices = (await _db2.GetManyAsync<ChrCustomizationChoice>(c => c.ChrCustomizationOptionId == option.Id)).ToList();
                result.Add(new CustomizationOptionDto()
                {
                    Id = option.Id,
                    Name = option.Name
                },
                (from a in choices
                 select new CustomizationChoiceDto()
                 {
                     Id = a.Id,
                     Name = $"{choices.FindIndex(c => c.Id == a.Id) + 1} {a.Name}"
                 }).ToList()
                );
            }
            return result;
        }
    }
}