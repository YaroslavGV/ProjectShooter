using System;
using System.Linq;
using System.Collections.Generic;

namespace InventorySystem
{
    // performance for json

    [Serializable]
    public struct SlotItemID
    {
        public string slot;
        public int itemID;

        public SlotItemID (string point, int itemID)
        {
            this.slot = point;
            this.itemID = itemID;
        }

        public override string ToString () => string.Format("{0}: {1}", slot, itemID);
    }

    [Serializable]
    public struct EquipmentID
    {
        public SlotItemID[] items;

        public EquipmentID (IEnumerable<SlotItemID> items) => this.items = items.ToArray();

        public override string ToString () => string.Join(Environment.NewLine, items);
    }

    // performance for game

    [Serializable]
    public struct SlotItem
    {
        public string slot;
        public Item item;

        public SlotItem (string point, Item item)
        {
            this.slot = point;
            this.item = item;
        }

        public override string ToString () => string.Format("{0}: {1} {2}", slot, item.ID, item.Name);

        public SlotItemID PointItemID => new SlotItemID(slot, item.ID);
    }

    [Serializable]
    public struct Equipment
    {
        public SlotItem[] items;

        public Equipment (IEnumerable<SlotItem> items) => this.items = items.ToArray();

        public override string ToString () => string.Join(Environment.NewLine, items);

        public EquipmentID EquipmentID => new EquipmentID(items.Select(i => i.PointItemID));
    }

    // performance for editor

    [Serializable]
    public struct SlotKeyItem
    {
        public Key slot;
        public Item item;

        public SlotKeyItem (Key point, Item item)
        {
            this.slot = point;
            this.item = item;
        }

        public override string ToString () => string.Format("{0}: {1} {2}", slot.Name, item.ID, item.Name);

        public SlotItem PointItem => new SlotItem(slot.Name, item);
    }

    [Serializable]
    public struct EquipmentKey
    {
        public SlotKeyItem[] items;

        public EquipmentKey (IEnumerable<SlotKeyItem> items) => this.items = items.ToArray();

        public override string ToString () => string.Join(Environment.NewLine, items);

        public Equipment Equipment => new Equipment(items.Select(i => i.PointItem));
    }
}