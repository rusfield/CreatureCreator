using CreatureCreator.Core.Providers;
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
using System.Diagnostics;

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
                    Name = ValidationHelper.ValidateCreatureId,
                    Status = ValidationStatuses.ERROR,
                    Description = $"The Id is not within the configurated range. It should be equal or greater than {_idRangeFrom} and lower than {_idRangeTo}"
                });
            }

            return result;
        }

        public async Task<int> GetNextCreatureIdAsync(bool quickScan = true)
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

        public async Task<int> GetNextHotfixIdAsync(bool quickScan = true)
        {
            int id = _idRangeFrom;
            var hotfixIdsInRange = await _mySql.GetManyAsync<HotfixData>(c => c.Id >= _idRangeFrom && c.Id < _idRangeTo);
            if (hotfixIdsInRange.Count() > 0)
            {
                return hotfixIdsInRange.Max(c => c.Id) + 1;
            }
            return id;
        }

        public async Task<List<ValidationStatusDto>> ValidateDatabase()
        {
            var result = new List<ValidationStatusDto>();

            bool worldServerIsRunning = false;
            bool bNetServerIsRunning = false;
            bool mySqlIsRunning = false;

            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var processName = process.ProcessName.ToLower();
                if (processName.Contains("bnetserver"))
                {
                    bNetServerIsRunning = true;
                    Console.WriteLine($"{processName} is running.");
                }
                else if (processName.Contains("worldserver"))
                {
                    worldServerIsRunning = true;
                    Console.WriteLine($"{processName} is running.");
                }
                else if (processName.Contains("mysql"))
                {
                    mySqlIsRunning = true;
                    Console.WriteLine($"{processName} is running.");
                }
            }

            var worldDbOk = mySqlIsRunning && await _mySql.WorldConnectionTestAsync();
            var hotfixesDbOk = mySqlIsRunning && await _mySql.HotfixesConnectionTestAsync();
            var charactersDbOk = mySqlIsRunning && await _mySql.CharactersConnectionTestAsync();

            var tableNpcModelItemSlotDisplayInfoExists = hotfixesDbOk && await _mySql.TableExists<NpcModelItemSlotDisplayInfo>();
            var tableCreatureDisplayInfoOptionExists = hotfixesDbOk && await _mySql.TableExists<CreatureDisplayInfoOption>();

            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateBNetServerProcess,
                Status = bNetServerIsRunning ? ValidationStatuses.OK : ValidationStatuses.WARNING,
                Description = bNetServerIsRunning ? "" : "Process is not running."
            });
            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateWorldServerProcess,
                Status = worldServerIsRunning ? ValidationStatuses.OK : ValidationStatuses.WARNING,
                Description = worldServerIsRunning ? "" : "Process is not running."
            });
            result.Add(new ValidationStatusDto()
            {
                Name = ValidationHelper.ValidateMySqlProcess,
                Status = mySqlIsRunning ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                Description = mySqlIsRunning ? "" : "Process is not running."
            });

            if (mySqlIsRunning)
            {
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateWorldDb,
                    Status = worldDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                    Description = worldDbOk ? "" : "Unable to connect to World DB."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateCharactersDb,
                    Status = charactersDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                    Description = charactersDbOk ? "" : "Unable to connect to Characters DB."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateHotfixesDb,
                    Status = hotfixesDbOk ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                    Description = hotfixesDbOk ? "" : "Unable to connect to Hotfixes DB."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateCreatureDisplayInfoOptionTable,
                    Status = tableCreatureDisplayInfoOptionExists ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                    Description = tableCreatureDisplayInfoOptionExists ? "" : "Table missing."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateNpcModelItemSlotDisplayInfoTable,
                    Status = tableNpcModelItemSlotDisplayInfoExists ? ValidationStatuses.OK : ValidationStatuses.ERROR,
                    Description = tableNpcModelItemSlotDisplayInfoExists ? "" : "Table missing."
                });
            }
            else
            {
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateWorldDb,
                    Status = ValidationStatuses.UNKNOWN,
                    Description = "MySQL is not running."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateCharactersDb,
                    Status = ValidationStatuses.UNKNOWN,
                    Description = "MySQL is not running."
                });
                result.Add(new ValidationStatusDto()
                {
                    Name = ValidationHelper.ValidateHotfixesDb,
                    Status = ValidationStatuses.UNKNOWN,
                    Description = "MySQL is not running."
                });
            }


            return result;
        }

        public async Task DeleteCreatureAsync(int creatureId)
        {
            await DeleteFromHotfix(creatureId);
            await DeleteFromWorld(creatureId);
        }

        async Task DeleteFromWorld(int creatureId)
        {
            var creatureTemplate = await _mySql.GetAsync<CreatureTemplate>(c => c.Entry == creatureId);
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureId == creatureId);
            var creatureEquipTemplate = await _mySql.GetAsync<CreatureEquipTemplate>(c => c.CreatureId == creatureId);
            var creatureModelInfo = await _mySql.GetAsync<CreatureModelInfo>(c => c.DisplayId == creatureId);
            var creatureTemplateAddon = await _mySql.GetAsync<CreatureTemplateAddon>(c => c.Entry == creatureId);


            if (creatureTemplateAddon != null)
                await _mySql.DeleteAsync(creatureTemplateAddon);

            if (creatureTemplate != null)
                await _mySql.DeleteAsync(creatureTemplate);

            if (creatureTemplateModel != null)
                await _mySql.DeleteAsync(creatureTemplateModel);

            if (creatureEquipTemplate != null)
                await _mySql.DeleteAsync(creatureEquipTemplate);

            if (creatureModelInfo != null)
                await _mySql.DeleteAsync(creatureModelInfo);
        }

        async Task DeleteFromHotfix(int creatureId)
        {
            var creatureDisplayInfoExtra = await _mySql.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == creatureId);
            var npcModelItemSlotDisplayInfos = await _mySql.GetManyAsync<NpcModelItemSlotDisplayInfo>(c => c.Id >= creatureId && c.Id < creatureId + _idSize);
            var creatureDisplayInfoOptions = await _mySql.GetManyAsync<CreatureDisplayInfoOption>(c => c.Id >= creatureId && c.Id < creatureId + _idSize);
            var creatureDisplayInfo = await _mySql.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureId);

            var hotfixData = await _mySql.GetManyAsync<HotfixData>(h => h.UniqueId == creatureId);

            if (creatureDisplayInfoExtra != null)
                await _mySql.DeleteAsync(creatureDisplayInfoExtra);


            if (npcModelItemSlotDisplayInfos != null && npcModelItemSlotDisplayInfos.Count() > 0)
                await _mySql.DeleteManyAsync(npcModelItemSlotDisplayInfos);

            if (creatureDisplayInfoOptions != null && creatureDisplayInfoOptions.Count() > 0)
                await _mySql.DeleteManyAsync(creatureDisplayInfoOptions);

            if (creatureDisplayInfo != null)
                await _mySql.DeleteAsync(creatureDisplayInfo);

            if (hotfixData != null && hotfixData.Count() > 0)
            {
                foreach (var hotfix in hotfixData)
                {
                    hotfix.Status = HotfixStatuses.INVALID;
                }
                await _mySql.UpdateManyAsync(hotfixData);
            }
        }

        public async Task SaveCreatureAsync(CreatureDto creature)
        {
            var modelHelper = new ModelHelper(_verifiedBuild);
            if (creature.IsUpdate)
            {
                await _mySql.UpdateAsync(modelHelper.CreateCreatureTemplate(creature));
                await _mySql.UpdateAsync(modelHelper.CreateCreatureTemplateModel(creature));
                await _mySql.UpdateAsync(modelHelper.CreateCreatureEquipTemplate(creature));
                await _mySql.UpdateAsync(modelHelper.CreateCreatureModelInfo(creature));
                await _mySql.UpdateAsync(modelHelper.CreateCreatureTemplateAddon(creature));

                // Delete from Hotfixes (re-added later in method)
                await DeleteFromHotfix(creature.Id);
            }
            else
            {
                // Add
                await _mySql.AddAsync(modelHelper.CreateCreatureTemplate(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureTemplateModel(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureEquipTemplate(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureModelInfo(creature));
                await _mySql.AddAsync(modelHelper.CreateCreatureTemplateAddon(creature));
            }

            int hotfixId = await GetNextHotfixIdAsync();
            var hotfixes = new List<HotfixData>();
            await _mySql.AddAsync(modelHelper.CreateCreatureDisplayInfoExtra(creature, hotfixes, hotfixId));
            await _mySql.AddManyAsync(modelHelper.CreateNpcModelItemSlotDisplayInfos(creature, hotfixes, hotfixId));
            await _mySql.AddManyAsync(modelHelper.CreateCreatureDisplayInfoOptions(creature, hotfixes, hotfixId));
            await _mySql.AddAsync(modelHelper.CreateCreatureDisplayInfo(creature, hotfixes, hotfixId));

            await _mySql.AddManyAsync(hotfixes);

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
            // Newest on top
            result.Reverse();
            return result;
        }

        public async Task<CreatureDto?> GetCreatureByIdAsync(int creatureId, Action<string, string, int>? progressCallback = null)
        {
            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            progressCallback("Creature", $"Retrieving first Display ID for {creatureId}", 5);
            // This will only return the first model.
            // If the desired result has multiple displays, use the desired one directly instead.
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureId == creatureId);
            if (creatureTemplateModel != null)
            {
                var creature = await GetCreatureByDisplayIdAsync(creatureTemplateModel.CreatureDisplayId, progressCallback);
                if (creature != null)
                {
                    if (creatureId < _idRangeTo && creatureId >= _idRangeFrom)
                    {
                        // Override the automatically generated Id, as this is most likely an update.
                        creature.Id = creatureId;
                        creature.IsUpdate = true;
                    }

                    return creature;
                }
            }
            return null;
        }

        public async Task<CreatureDto?> GetCreatureByDisplayIdAsync(int creatureDisplayId, Action<string, string, int>? progressCallback = null)
        {

            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            progressCallback("Creature", $"Retrieving Creature Template Model", 10);
            CreatureTemplate? creatureTemplate = null;
            var creatureTemplateModel = await _mySql.GetAsync<CreatureTemplateModel>(c => c.CreatureDisplayId == creatureDisplayId);
            if (creatureTemplateModel != null)
            {
                progressCallback("Creature", $"Retrieving Creature Template", 15);
                creatureTemplate = await _mySql.GetAsync<CreatureTemplate>(c => c.Entry == creatureTemplateModel.CreatureId);
                if (creatureTemplate != null)
                    progressCallback("Creature", $"Found Creature Template ({creatureTemplate.Entry})", 15);
            }

            progressCallback("Creature", $"Retrieving Display Info", 20);
            var displayInfo = await _mySql.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureDisplayId) ?? await _db2.GetAsync<CreatureDisplayInfo>(c => c.Id == creatureDisplayId);
            if (displayInfo == null)
            {
                progressCallback("Failed", $"Display Info for Display Id {creatureDisplayId} not found", 100);
                return null;
            }

            var result = new CreatureDto(await GetNextCreatureIdAsync(), displayInfo.Gender, DisplayRaces.NONE)
            {
                CreatureType = creatureTemplate != null ? creatureTemplate.Type : CreatureTypes.HUMANOID,
                Faction = creatureTemplate != null ? creatureTemplate.Faction : 17,
                CreatureUnitClass = creatureTemplate != null ? creatureTemplate.UnitClass : CreatureUnitClasses.WARRIOR,
                Level = creatureTemplate != null && creatureTemplate.MaxLevel > 0 ? creatureTemplate.MaxLevel : 30,
                Name = creatureTemplate?.Name,
                SubName = creatureTemplate?.SubName,
                Scale = creatureTemplate?.Scale ?? 1.0,
                Rank = creatureTemplate?.Rank ?? Ranks.NORMAL,
                HealthModifier = creatureTemplate?.HealthModifier ?? 1,
                DamageModifier = creatureTemplate?.DamageModifier ?? 1,
                FlagsExtra = (FlagsExtra)(creatureTemplate?.FlagsExtra ?? 0),
                UnitFlags = (UnitFlags)(creatureTemplate?.UnitFlags ?? 0),
                UnitFlags2 = (UnitFlags2)(creatureTemplate?.UnitFlags2 ?? 0),
                UnitFlags3 = (UnitFlags3)(creatureTemplate?.UnitFlags3 ?? 0),
                IsCustomizable = false,
                IsUpdate = false
            };

            progressCallback("Creature", $"Retrieving Display Info Extra", 30);
            var displayInfoExtra = await _mySql.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == displayInfo.ExtendedDisplayInfoId) ?? await _db2.GetAsync<CreatureDisplayInfoExtra>(c => c.Id == displayInfo.ExtendedDisplayInfoId);
            if (displayInfoExtra == null)
            {
                progressCallback("Creature", $"Display Info Extra not found", 100);
                return result;
            }

            result.Race = displayInfoExtra.DisplayRaceId;
            result.IsCustomizable = true;

            progressCallback("Customizations", $"Retrieving available customizations", 40);
            var availableCustomizations = await GetAvailableCustomizations(displayInfoExtra.DisplayRaceId, displayInfo.Gender);
            progressCallback("Customizations", $"Retrieving creature customizations", 60);
            var hotfixCustomizations = await _mySql.GetManyAsync<CreatureDisplayInfoOption>(h => h.CreatureDisplayInfoExtraId == displayInfoExtra.Id);
            var db2Customizations = await _db2.GetManyAsync<CreatureDisplayInfoOption>(h => h.CreatureDisplayInfoExtraId == displayInfoExtra.Id);
            var customizations = new Dictionary<int, int>();

            foreach (var (availableCustomization, index) in availableCustomizations.Select((value, idx) => (value, idx)))
            {
                progressCallback("Customizations", $"Preparing {availableCustomization.Key.Name}", Math.Min(60 + index, 70));

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
            result.Customizations = customizations;

            progressCallback("Equipment", $"Retrieving equipment", 80);
            var hotfixEquipment = await _mySql.GetManyAsync<NpcModelItemSlotDisplayInfo>(npc => npc.NpcModelId == displayInfoExtra.Id);
            var db2Equipment = await _db2.GetManyAsync<NpcModelItemSlotDisplayInfo>(npc => npc.NpcModelId == displayInfoExtra.Id);

            for (int i = 0; i <= Enum.GetValues(typeof(ArmorSlots)).Cast<int>().Max(); i++)
            {
                var itemSlot = (ArmorSlots)i;
                progressCallback("Equipment", $"Handling {itemSlot.ToString().ToLower().Replace("_", "")} slot", Math.Min(80 + (int)itemSlot, 90));

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

            progressCallback("Equipment", $"Handling weapons", 95);
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
                        result.RangedItemId = creatureEquipTemplate.ItemId3;
                    if (creatureEquipTemplate.AppearanceModId3 > 0)
                        result.RangedItemAppearanceModifierId = creatureEquipTemplate.AppearanceModId3;
                    if (creatureEquipTemplate.ItemVisual3 > 0)
                        result.RangedItemVisual = creatureEquipTemplate.ItemVisual3;
                }
            }
            progressCallback("Done", "Returning creature", 100);

            return result;
        }

        public async Task<CreatureDto?> GetCreatureByCharacterNameAsync(string characterName, Action<string, string, int>? progressCallback = null)
        {
            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            progressCallback("Character", $"Retrieving {characterName}", 15);
            var character = await _mySql.GetAsync<Characters>(c => c.Name == characterName);
            if (character == null)
            {
                progressCallback("Failed", $"{characterName} not found", 100);
                return null;
            }

            progressCallback("Customizations", "Retrieving customizations", 30);
            var characterCustomization = await _mySql.GetManyAsync<CharacterCustomizations>(c => c.Guid == character.Guid);
            progressCallback("Customizations", $"Found {characterCustomization.Count()} customizations", 40);

            var customizations = new Dictionary<int, int>();
            foreach (var customization in characterCustomization)
            {
                customizations.Add(customization.ChrCustomizationOptionId, customization.ChrCustomizationChoiceId);
            }
            var result = new CreatureDto(await GetNextCreatureIdAsync(), character.Gender, character.Race)
            {
                Customizations = customizations,
                Level = character.Level,
                Name = character.Name
            };

            progressCallback("Equipment", "Retrieving equipment", 50);
            var characterItems = await _mySql.GetManyAsync<ItemInstance>(c => c.OwnerGuid == character.Guid);
            var characterEquipMap = await _mySql.GetManyAsync<CharacterInventory>(c => c.Guid == character.Guid);
            foreach (var equippedItem in characterEquipMap.OrderBy(c => c.Slot))
            {
                progressCallback("Equipment", $"Handling {equippedItem.Slot.ToString().ToLower().Replace("_", "")} slot", Math.Min(50 + 2 * (int)equippedItem.Slot, 99));

                var item = characterItems.Where(i => i.Guid == equippedItem.Item).FirstOrDefault();
                if (item == null || (int)equippedItem.Slot > Enum.GetValues(typeof(CharacterInventorySlots)).Cast<int>().Max())
                    continue;

                var itemAppearanceId = 0;
                var itemAppearanceModifierId = 0;
                var itemId = item.ItemEntry;
                var itemVisual = 0;
                var transmogItem = await _mySql.GetAsync<ItemInstanceTransmog>(c => c.ItemGuid == equippedItem.Item);

                // Try get ItemAppearanceId by transmog.
                // Note: Dont mix up ItemModifiedAppearance.Id with ItemAppearanceModifierId.
                if (transmogItem != null)
                {
                    var itemModifiedAppearance = await _db2.GetAsync<ItemModifiedAppearance>(c => c.Id == transmogItem.ItemModifiedAppearanceAllSpecs);
                    if (itemModifiedAppearance != null)
                    {
                        itemAppearanceId = itemModifiedAppearance.ItemAppearanceId;
                        itemAppearanceModifierId = itemModifiedAppearance.ItemAppearanceModifierId;
                        itemId = itemModifiedAppearance.ItemId;
                    }
                    if (transmogItem.SpellItemEnchantmentAllSpecs > 0 && equippedItem.Slot.IsWeaponSlot())
                    {
                        var spellItemEnchantment = await _db2.GetAsync<SpellItemEnchantment>(s => s.Id == transmogItem.SpellItemEnchantmentAllSpecs);
                        if (spellItemEnchantment != null)
                            itemVisual = spellItemEnchantment.ItemVisual;
                    }
                }

                // Get ItemAppearanceId the default way, either because transmog does not exist or transmog failed.
                if (itemAppearanceId == 0)
                {
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

                    itemAppearanceId = itemModifiedAppearance.ItemAppearanceId;
                }

                if (itemVisual == 0 && equippedItem.Slot.IsWeaponSlot())
                {
                    foreach (var enchantment in item.Enchantments.Split(' '))
                    {
                        if (int.TryParse(enchantment.Trim(), out var enchantmentId))
                        {
                            if (enchantmentId > 0)
                            {
                                var spellItemEnchantment = await _db2.GetAsync<SpellItemEnchantment>(s => s.Id == enchantmentId);
                                if (spellItemEnchantment != null && spellItemEnchantment.ItemVisual > 0)
                                {
                                    itemVisual = spellItemEnchantment.ItemVisual;
                                    break;
                                }
                            }
                        }
                    }
                }

                var itemAppearance = await _db2.GetAsync<ItemAppearance>(c => c.Id == itemAppearanceId);
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
                        result.MainHandItemId = itemId;
                        result.MainHandItemAppearanceModifierId = itemAppearanceModifierId;
                        result.MainHandItemVisual = itemVisual;
                        break;

                    case CharacterInventorySlots.OFF_HAND:
                        result.OffHandItemId = itemId;
                        result.OffHandItemAppearanceModifierId = itemAppearanceModifierId;
                        result.OffHandItemVisual = itemVisual;
                        break;

                    case CharacterInventorySlots.RANGED:
                        result.RangedItemId = itemId;
                        result.RangedItemAppearanceModifierId = itemAppearanceModifierId;
                        result.RangedItemVisual = itemVisual;
                        break;
                };
            }
            progressCallback("Done", "Creature returned", 100);
            return result;
        }

        public async Task<CreatureDto> GetCreatureByRaceAndGender(DisplayRaces race, Genders gender, Action<string, string, int>? progressCallback = null)
        {
            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            progressCallback("Creature", "Preparing creature", 50);
            var id = await GetNextCreatureIdAsync();
            progressCallback("Done", "Creature returned", 100);
            return new CreatureDto(id, gender, race);
        }

        public async Task<Dictionary<CustomizationOptionDto, List<CustomizationChoiceDto>>> GetAvailableCustomizations(DisplayRaces race, Genders gender, bool includeDruidForms = false, Action<string, string, int>? progressCallback = null)
        {
            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            var result = new Dictionary<CustomizationOptionDto, List<CustomizationChoiceDto>>();
            var chrModel = new ChrModels().ConvertFromRaceAndGender(race, gender);
            if (chrModel == null)
                return result;

            var options = await _db2.GetManyAsync<ChrCustomizationOption>(c => c.ChrModelId == chrModel);
            foreach (var option in options)
            {
                if (!includeDruidForms && option.IsDruidFormCustomization())
                    continue;

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

        public async Task<List<ItemDto>> GetItemsByItemId(int itemId, Action<string, string, int>? progressCallback = null)
        {
            var result = new List<ItemDto>();

            if (progressCallback == null)
                progressCallback = ConsoleProgressCallback;

            progressCallback("Item", $"Retrieving item", 20);
            var item = await _db2.GetAsync<Item>(c => c.Id == itemId);
            if(item == null)
            {
                progressCallback("Item", $"Item not found", 100);
                return result;
            }
            progressCallback("Item", $"Retrieving item", 40);
            var modifiedAppearances = (await _db2.GetManyAsync<ItemModifiedAppearance>(c => c.ItemId == itemId)).OrderBy(c => c.OrderIndex).ToList();
            if (modifiedAppearances == null || modifiedAppearances.Count == 0)
            {
                progressCallback("Item", $"Modified Appearance not found", 100);
                return result;
            }

            bool isRaidDrop = modifiedAppearances.Count == 4;
            bool isArtifact = modifiedAppearances.Count == 24;

            

            foreach (var (modifiedAppearance, index) in modifiedAppearances.Select((value, idx) => (value, idx)))
            {
                progressCallback("Item", $"Retrieving Item Appearance {index + 1} of {modifiedAppearances.Count}", Math.Min(50 + (50 / modifiedAppearances.Count * index), 99));

                var itemAppearance = await _db2.GetAsync<ItemAppearance>(c => c.Id == modifiedAppearance.ItemAppearanceId);
                if (itemAppearance == null)
                {
                    progressCallback("Item", $"Item Appearance not found", Math.Min(50 + (50 / modifiedAppearances.Count * index), 99));
                    continue;
                }
                   
                result.Add(new ItemDto()
                {
                    ItemId = itemId,
                    ItemDisplayInfoId = itemAppearance.ItemDisplayInfoId,
                    ArmorSlot = item.InventoryType.ToArmorSlot(),
                    WeaponSlot = item.InventoryType.ToWeaponSlot(),
                    ItemModifiedAppearanceId = modifiedAppearance.ItemAppearanceModifierId,
                    ItemSourceType = (ItemSourceTypes)(isRaidDrop ? 1 + index : (isArtifact ? 5 + index : 0))
                });
            }

            progressCallback("Item", $"Items returned", 100);
            return result;
        }

        public void LaunchWorldServer()
        {
            return;
            string path = @"D:\TrinityCore\Fork\out\build\x64-Release\bin";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @$"{path}/worldserver.exe",
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = path,
                    CreateNoWindow = false,

                }
            };

            process.Start();
        }

        void ConsoleProgressCallback(string stepTitle, string stepSubTitle, int progress)
        {
            Console.WriteLine($"{progress} %: {stepTitle} => {stepSubTitle}");
        }


    }
}
