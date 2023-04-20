using UnityEngine;
using Zenject;

namespace Unit
{
    public class SceneUnitsInstaller : MonoInstaller
    {
        [SerializeField] private SceneUnits _units;

        public override void InstallBindings ()
        {
            Container.Bind<SceneUnits>().FromInstance(_units).AsSingle();
        }
    }
}