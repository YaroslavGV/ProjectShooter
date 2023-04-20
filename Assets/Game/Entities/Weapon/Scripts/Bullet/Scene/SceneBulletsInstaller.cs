using UnityEngine;
using Zenject;

namespace Weapon
{
    public class SceneBulletsInstaller : MonoInstaller
    {
        [SerializeField] private SceneBullets _bullets;

        public override void InstallBindings ()
        {
            Container.Bind<SceneBullets>().FromInstance(_bullets).AsSingle();
        }
    }
}
