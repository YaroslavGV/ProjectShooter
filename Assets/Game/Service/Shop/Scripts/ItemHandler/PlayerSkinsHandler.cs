using System;
using Unit.Skin;

namespace Shop
{
    public class PlayerSkinsHandler : ShopItemHandler
    {
        public readonly PlayerSkins _playSkin;
                
        public PlayerSkinsHandler (PlayerSkins playSkin)
        {
            _playSkin = playSkin;
        }

        public override Type ProcessedType => typeof(ShopSkinItem);

        public override bool CanBuy (ShopItem item)
        {
            if (IsValid(item) == false)
                return false;
            ShopSkinItem sItem = item as ShopSkinItem;
            bool available = _playSkin.IsAvailable(sItem.Skin);
            return available == false;
        }

        public override void Buy (ShopItem item)
        {
            if (IsValid(item) == false)
                return;
            ShopSkinItem sItem = item as ShopSkinItem;
            _playSkin.SetAvailable(sItem.Skin);
        }
    }
}