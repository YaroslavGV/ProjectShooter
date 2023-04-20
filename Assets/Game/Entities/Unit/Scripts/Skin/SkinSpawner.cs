using UnityEngine;

namespace Unit.Skin
{
    public class SkinSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        
        public UnitSkin Skin { get; private set; }
        
        public void SpawnSkin (UnitSkin skin)
        {
            if (Skin != skin)
            {
                if (Skin != null)
                    Destroy(Skin.gameObject);
                Skin = Instantiate(skin, _parent);
            }
        }
    }
}