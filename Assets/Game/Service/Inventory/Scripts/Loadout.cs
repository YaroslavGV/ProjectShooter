using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Loadout
    {
        public Action OnChange;
        private readonly List<SlotItem> _items = new List<SlotItem>();

        public override string ToString () => string.Join(Environment.NewLine, _items);

        public IEnumerable<SlotItem> Items => _items;
        public Equipment Equipment => new Equipment(_items);

        /// <returns> Removed item </returns>
        public Item Equipe (string pointName, Item item)
        {
            Item current = null;
            for (int i = 0; i < _items.Count; i++)
                if (_items[i].slot == pointName)
                {
                    current = _items[i].item;
                    _items[i] = new SlotItem(pointName, item);
                    break;
                }
            if (current == null)
                _items.Add(new SlotItem(pointName, item));
            OnChange?.Invoke();
            return current;
        }

        public Item Equipe (SlotItem pointItem) => Equipe(pointItem.slot, pointItem.item);

        public Item GetItem (string pointName)
        {
            for (int i = 0; i < _items.Count; i++)
                if (_items[i].slot == pointName)
                    return _items[i].item;
            return null;
        }
    }
}