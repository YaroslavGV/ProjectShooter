using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "BulletTemplateAsset", menuName = "Weapon/Bullet/TemplateAsset")]
    public class BulletTemplateAsset : ScriptableObject
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private PoolSettings _settings = new PoolSettings(16, 8);
        [SerializeField] private BulletEffectData[] _effects;

        public Bullet Bullet => _bullet;
        public PoolSettings Settings => _settings;
        public int EffectsCount => _effects.Length;
        public IEnumerable<BulletEffectData> Effects => _effects;
    }
}