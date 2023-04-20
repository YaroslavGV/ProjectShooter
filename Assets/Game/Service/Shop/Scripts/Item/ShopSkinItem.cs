using UnityEngine;
using Unit.Skin;

namespace Shop
{
    [CreateAssetMenu(fileName = "SkinItem", menuName = "Shop/Item/Skin")]
    public class ShopSkinItem : ShopItem
    {
        [SerializeField] private UnitSkin _skin;

        public override Sprite Icon => _skin.Icon;
        public UnitSkin Skin => _skin;
    }
}