using UnityEngine;
using Currency;

namespace Shop
{
    public abstract class ShopItem : ScriptableObject
    {
        [SerializeField] private CurrecnyValue _price;

        public CurrecnyValue Price => _price;
        public abstract Sprite Icon { get; }
    }
}