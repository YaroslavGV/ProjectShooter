using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Loadout
    {
        public Action OnChange;
        protected readonly List<SlotItem> items = new List<SlotItem>();

        public override string ToString () => string.Join(Environment.NewLine, items);

        public IEnumerable<SlotItem> Items => items;
        public Equipment Equipment => new Equipment(items);

        /// <returns> Removed item </returns>
        public Item Equipe (string pointName, Item item)
        {
            Item current = null;
            for (int i = 0; i < items.Count; i++)
                if (items[i].slot == pointName)
                {
                    current = items[i].item;
                    items[i] = new SlotItem(pointName, item);
                    break;
                }
            if (current == null)
                items.Add(new SlotItem(pointName, item));
            OnChange?.Invoke();
            return current;
        }

        public Item Equipe (SlotItem pointItem) => Equipe(pointItem.slot, pointItem.item);

        public Item GetItem (string pointName)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].slot == pointName)
                    return items[i].item;
            return null;
        }
    }
}