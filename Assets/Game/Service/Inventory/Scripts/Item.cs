using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id = 0;
        [SerializeField] private Sprite _icon;

        public int ID => _id;
        public virtual string Name => name;
        public virtual Sprite Icon => _icon;


        public Item GetCopy () => Instantiate(this);
    }
}