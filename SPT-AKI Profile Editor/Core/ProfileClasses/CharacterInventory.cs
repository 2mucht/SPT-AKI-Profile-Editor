﻿using Newtonsoft.Json;
using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.ServerClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPT_AKI_Profile_Editor.Core.ProfileClasses
{
    public class CharacterInventory : BindableEntity
    {
        private InventoryItem[] items;
        private string stash;
        private string equipment;

        [JsonProperty("items")]
        public InventoryItem[] Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged("");
            }
        }

        [JsonProperty("stash")]
        public string Stash
        {
            get => stash;
            set
            {
                stash = value;
                OnPropertyChanged("Stash");
            }
        }

        [JsonProperty("equipment")]
        public string Equipment
        {
            get => equipment;
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        [JsonIgnore]
        public string Pockets
        {
            get => Items?
                .Where(x => x.IsPockets)
                .FirstOrDefault()?.Tpl;
            set
            {
                var pocketsSlot = Items?
                .Where(x => x.IsPockets)
                .FirstOrDefault();
                if (pocketsSlot != null)
                {
                    pocketsSlot.Tpl = value;
                    OnPropertyChanged("Pockets");
                }
            }
        }

        [JsonIgnore]
        public string DollarsCount => GetMoneyCountString(AppData.AppSettings.MoneysDollarsTpl);

        [JsonIgnore]
        public string RublesCount => GetMoneyCountString(AppData.AppSettings.MoneysRublesTpl);

        [JsonIgnore]
        public string EurosCount => GetMoneyCountString(AppData.AppSettings.MoneysEurosTpl);

        [JsonIgnore]
        public IEnumerable<InventoryItem> InventoryItems => Items?
            .Where(x => x.ParentId == Stash && x.Location != null);

        [JsonIgnore]
        public bool HasItems => InventoryItems?.Count() > 0;

        [JsonIgnore]
        public bool ContainsModdedItems => InventoryItems.Any(x => x.IsAddedByMods);

        [JsonIgnore]
        public bool InventoryHaveDuplicatedItems => GroupedInventory.Any();

        [JsonIgnore]
        public InventoryItem FirstPrimaryWeapon => GetEquipment(AppData.AppSettings.FirstPrimaryWeaponSlotId);

        [JsonIgnore]
        public InventoryItem Headwear => GetEquipment(AppData.AppSettings.HeadwearSlotId);

        [JsonIgnore]
        public InventoryItem TacticalVest => GetEquipment(AppData.AppSettings.TacticalVestSlotId);

        [JsonIgnore]
        public InventoryItem SecuredContainer => GetEquipment(AppData.AppSettings.SecuredContainerSlotId);

        [JsonIgnore]
        public InventoryItem Backpack => GetEquipment(AppData.AppSettings.BackpackSlotId);

        [JsonIgnore]
        public InventoryItem Earpiece => GetEquipment(AppData.AppSettings.EarpieceSlotId);

        [JsonIgnore]
        public InventoryItem FaceCover => GetEquipment(AppData.AppSettings.FaceCoverSlotId);

        [JsonIgnore]
        public InventoryItem Eyewear => GetEquipment(AppData.AppSettings.EyewearSlotId);

        [JsonIgnore]
        public InventoryItem ArmorVest => GetEquipment(AppData.AppSettings.ArmorVestSlotId);

        [JsonIgnore]
        public InventoryItem SecondPrimaryWeapon => GetEquipment(AppData.AppSettings.SecondPrimaryWeaponSlotId);

        [JsonIgnore]
        public InventoryItem Holster => GetEquipment(AppData.AppSettings.HolsterSlotId);

        [JsonIgnore]
        public InventoryItem Scabbard => GetEquipment(AppData.AppSettings.ScabbardSlotId);

        [JsonIgnore]
        public InventoryItem ArmBand => GetEquipment(AppData.AppSettings.ArmBandSlotId);

        [JsonIgnore]
        public IEnumerable<InventoryItem> PocketsItems => Items?
            .Where(x => x.ParentId == Items?.Where(x => x.IsPockets).FirstOrDefault()?.Id);

        [JsonIgnore]
        public bool PocketsHasItems => PocketsItems?.Count() > 0;

        [JsonIgnore]
        public bool HasEquipment => FirstPrimaryWeapon != null || Headwear != null || TacticalVest != null || SecuredContainer != null
            || Backpack != null || Earpiece != null || FaceCover != null || Eyewear != null || ArmorVest != null
            || SecondPrimaryWeapon != null || Holster != null || Scabbard != null || ArmBand != null
            || PocketsItems?.Count() > 0;

        private IEnumerable<string> GroupedInventory => Items?
            .GroupBy(x => x.Id)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key);

        public void RemoveDuplicatedItems() => FinalRemoveItems(GroupedInventory);

        public void RemoveItems(List<string> itemIds) => FinalRemoveItems(itemIds);

        public void RemoveAllItems() => FinalRemoveItems(InventoryItems.Select(x => x.Id));

        public List<InventoryItem> GetInnerItems(string itemId, List<string> skippedSlots = null)
        {
            List<InventoryItem> items = new();
            foreach (var item in Items?.Where(x => x.ParentId == itemId))
            {
                if (skippedSlots != null && skippedSlots.Count > 0 && skippedSlots.Contains(item.SlotId))
                    continue;
                items.Add(item);
                items.AddRange(GetInnerItems(item.Id, skippedSlots));
            }
            return items;
        }

        public void AddNewItemsToContainer(InventoryItem container, TarkovItem tarkovItem, int count, bool fir, string slotId)
        {
            int[,] Stash = GetSlotsMap(container);
            List<string> iDs = Items.Select(x => x.Id).ToList();
            int stacks = count / tarkovItem.Properties.StackMaxSize;
            if (tarkovItem.Properties.StackMaxSize * stacks < count) stacks++;
            List<ItemLocation> NewItemsLocations = GetItemLocations(tarkovItem, Stash, stacks);
            if (NewItemsLocations == null)
                throw new Exception(AppData.AppLocalization.GetLocalizedString("tab_stash_no_slots"));
            List<InventoryItem> items = Items.ToList();
            for (int i = 0; i < NewItemsLocations.Count; i++)
            {
                if (count <= 0) break;
                string id = ExtMethods.GenerateNewId(iDs);
                iDs.Add(id);
                var newItem = new InventoryItem
                {
                    Id = id,
                    ParentId = container.Id,
                    SlotId = slotId,
                    Tpl = tarkovItem.Id,
                    Location = new ItemLocation { R = NewItemsLocations[i].R, X = NewItemsLocations[i].X, Y = NewItemsLocations[i].Y, IsSearched = true },
                    Upd = new ItemUpd { StackObjectsCount = count > tarkovItem.Properties.StackMaxSize ? tarkovItem.Properties.StackMaxSize : count, SpawnedInSession = fir }
                };
                items.Add(newItem);
                count -= tarkovItem.Properties.StackMaxSize;
            }
            Items = items.ToArray();
        }

        public void AddNewItemsToStash(string tpl, int count, bool fir)
        {
            InventoryItem ProfileStash = Items.Where(x => x.Id == Stash).FirstOrDefault();
            var mItem = AppData.ServerDatabase.ItemsDB[tpl];
            AddNewItemsToContainer(ProfileStash, mItem, count, fir, "hideout");
        }

        public void RemoveAllEquipment()
        {
            FinalRemoveItems(new List<string>
            {
                FirstPrimaryWeapon?.Id,
                Headwear?.Id,
                TacticalVest?.Id,
                SecuredContainer?.Id,
                Backpack?.Id,
                Earpiece?.Id,
                FaceCover?.Id,
                Eyewear?.Id,
                ArmorVest?.Id,
                SecondPrimaryWeapon?.Id,
                Holster?.Id,
                Scabbard?.Id,
                ArmBand?.Id
            });
            FinalRemoveItems(PocketsItems?.Select(x => x.Id));
        }

        public int[,] GetSlotsMap(InventoryItem container)
        {
            int[,] Stash2D = CreateContainerStash2D(container);
            foreach (var item in Items?.Where(x => x.ParentId == container.Id))
            {
                (int itemWidth, int itemHeight) = GetSizeOfInventoryItem(item);
                int rotatedHeight = item.Location.R == ItemRotation.Vertical ? itemWidth : itemHeight;
                int rotatedWidth = item.Location.R == ItemRotation.Vertical ? itemHeight : itemWidth;
                for (int y = 0; y < rotatedHeight; y++)
                {
                    try
                    {
                        for (int z = item.Location.X; z < item.Location.X + rotatedWidth; z++)
                            Stash2D[item.Location.Y + y, z] = 1;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"Failed to insert item with id {item.Id} to Stash2D: {ex.Message}");
                    }
                }
            }
            return Stash2D;
        }

        private static List<ItemLocation> GetItemLocations(TarkovItem tarkovItem, int[,] stash, int stacks)
        {
            List<ItemLocation> freeSlots = GetFreeSlots(stash);
            if (freeSlots.Count < tarkovItem.Properties.Width * tarkovItem.Properties.Height * stacks)
                return null;
            List<ItemLocation> NewItemsLocations = new();
            foreach (var slot in freeSlots)
            {
                if (tarkovItem.Properties.Width == 1 && tarkovItem.Properties.Height == 1)
                    NewItemsLocations.Add(slot);
                else
                {
                    ItemLocation itemLocation = GetItemLocation(tarkovItem.Properties.Height, tarkovItem.Properties.Width, stash, slot);
                    if (itemLocation != null)
                        NewItemsLocations.Add(itemLocation);
                    if (NewItemsLocations.Count == stacks)
                        return NewItemsLocations;
                    itemLocation = GetItemLocation(tarkovItem.Properties.Width, tarkovItem.Properties.Height, stash, slot);
                    if (itemLocation != null)
                    {
                        itemLocation.R = ItemRotation.Vertical;
                        NewItemsLocations.Add(itemLocation);
                    }
                }
                if (NewItemsLocations.Count == stacks)
                    return NewItemsLocations;
            }
            return null;
        }

        private static int[,] CreateContainerStash2D(InventoryItem container)
        {
            Grid stashTPL = AppData.ServerDatabase.ItemsDB[container.Tpl].Properties.Grids.FirstOrDefault();
            return new int[stashTPL?.Props?.CellsV ?? 0, stashTPL?.Props?.CellsH ?? 0];
        }

        private static List<ItemLocation> GetFreeSlots(int[,] Stash)
        {
            List<ItemLocation> locations = new();
            for (int y = 0; y < Stash.GetLength(0); y++)
                for (int x = 0; x < Stash.GetLength(1); x++)
                    if (Stash[y, x] == 0)
                        locations.Add(new ItemLocation { X = x, Y = y, R = ItemRotation.Horizontal });
            return locations;
        }

        private static ItemLocation GetItemLocation(int Width, int Height, int[,] Stash, ItemLocation slot)
        {
            int size = 0;
            for (int y = 0; y < Width; y++)
            {
                if (slot.X + Height < Stash.GetLength(1) && slot.Y + y < Stash.GetLength(0))
                    for (int z = slot.X; z < slot.X + Height; z++)
                        if (Stash[slot.Y + y, z] == 0) size++;
            }
            if (size == Width * Height)
            {
                for (int y = 0; y < Width; y++)
                {
                    for (int z = slot.X; z < slot.X + Height; z++)
                        Stash[slot.Y + y, z] = 1;
                }
                return new ItemLocation { X = slot.X, Y = slot.Y };
            }
            return null;
        }

        private InventoryItem GetEquipment(string slotId)
        {
            return Items?.Where(x => x.ParentId == Equipment && x.SlotId == slotId)?.FirstOrDefault();
        }

        private List<string> GetCompleteItemsList(IEnumerable<string> items)
        {
            List<string> itemIds = new();
            foreach (var TargetItem in items)
            {
                if (string.IsNullOrEmpty(TargetItem))
                    continue;
                List<string> toDo = new() { TargetItem };
                while (toDo.Count > 0)
                {
                    foreach (var item in Items.Where(x => x.ParentId == toDo.ElementAt(0)))
                        toDo.Add(item.Id);
                    itemIds.Add(toDo.ElementAt(0));
                    toDo.Remove(toDo.ElementAt(0));
                }
            }
            return itemIds;
        }

        private void FinalRemoveItems(IEnumerable<string> itemIds)
        {
            var completedList = GetCompleteItemsList(itemIds);
            App.CloseItemViewWindows(completedList);
            List<InventoryItem> ItemsList = Items.ToList();
            while (completedList.Count > 0)
            {
                var item = ItemsList.Where(x => x.Id == completedList[0]).FirstOrDefault();
                if (item != null)
                    ItemsList.Remove(item);
                else
                    completedList.RemoveAt(0);
            }
            Items = ItemsList.ToArray();
        }

        private (int iW, int iH) GetSizeOfInventoryItem(InventoryItem inventoryItem)
        {
            List<InventoryItem> toDo = new() { inventoryItem };
            TarkovItem tmpItem = AppData.ServerDatabase.ItemsDB[inventoryItem.Tpl];
            InventoryItem rootItem = Items.Where(x => x.ParentId == inventoryItem.Id).FirstOrDefault();
            bool FoldableWeapon = tmpItem.Properties.Foldable;
            string FoldedSlot = tmpItem.Properties.FoldedSlot;

            int SizeUp = 0;
            int SizeDown = 0;
            int SizeLeft = 0;
            int SizeRight = 0;

            int ForcedUp = 0;
            int ForcedDown = 0;
            int ForcedLeft = 0;
            int ForcedRight = 0;
            int outX = tmpItem.Properties.Width;
            int outY = tmpItem.Properties.Height;
            if (rootItem != null)
            {
                List<string> skipThisItems = new() { "5448e53e4bdc2d60728b4567", "566168634bdc2d144c8b456c", "5795f317245977243854e041" };
                bool rootFolded = rootItem.Upd != null && rootItem.Upd.Foldable != null && rootItem.Upd.Foldable.Folded;

                if (FoldableWeapon && string.IsNullOrEmpty(FoldedSlot) && rootFolded)
                    outX -= tmpItem.Properties.SizeReduceRight;

                if (!skipThisItems.Contains(tmpItem.Parent))
                {
                    while (toDo.Count > 0)
                    {
                        if (toDo.ElementAt(0) != null)
                        {
                            foreach (var item in Items.Where(x => x.ParentId == toDo.ElementAt(0).Id))
                            {
                                if (!item.SlotId.Contains("mod_"))
                                    continue;
                                toDo.Add(item);
                                TarkovItem itm = AppData.ServerDatabase.ItemsDB[item.Tpl];
                                bool childFoldable = itm.Properties.Foldable;
                                bool childFolded = item.Upd != null && item.Upd.Foldable != null && item.Upd.Foldable.Folded;
                                if (FoldableWeapon && FoldedSlot == item.SlotId && (rootFolded || childFolded))
                                    continue;
                                else if (childFoldable && rootFolded && childFolded)
                                    continue;
                                if (itm.Properties.ExtraSizeForceAdd)
                                {
                                    ForcedUp += itm.Properties.ExtraSizeUp;
                                    ForcedDown += itm.Properties.ExtraSizeDown;
                                    ForcedLeft += itm.Properties.ExtraSizeLeft;
                                    ForcedRight += itm.Properties.ExtraSizeRight;
                                }
                                else
                                {
                                    SizeUp = (SizeUp < itm.Properties.ExtraSizeUp) ? itm.Properties.ExtraSizeUp : SizeUp;
                                    SizeDown = (SizeDown < itm.Properties.ExtraSizeDown) ? itm.Properties.ExtraSizeDown : SizeDown;
                                    SizeLeft = (SizeLeft < itm.Properties.ExtraSizeLeft) ? itm.Properties.ExtraSizeLeft : SizeLeft;
                                    SizeRight = (SizeRight < itm.Properties.ExtraSizeRight) ? itm.Properties.ExtraSizeRight : SizeRight;
                                }
                            }
                        }
                        toDo.Remove(toDo.ElementAt(0));
                    }
                }
            }

            return (outX + SizeLeft + SizeRight + ForcedLeft + ForcedRight, outY + SizeUp + SizeDown + ForcedUp + ForcedDown);
        }

        private string GetMoneyCountString(string moneys) => (Items?
            .Where(x => x.Tpl == moneys)
            .Sum(x => x.Upd.StackObjectsCount ?? 0) ?? 0)
            .ToString("N0");
    }
}