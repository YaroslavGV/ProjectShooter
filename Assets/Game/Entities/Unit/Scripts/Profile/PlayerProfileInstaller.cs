using UnityEngine;
using Zenject;

namespace Unit
{
    public class PlayerProfileInstaller : MonoInstaller
    {
        [SerializeField] private PlayerProfileProvider _profile;

        public override void InstallBindings ()
        {
            Container.Bind<PlayerProfileProvider>().FromScriptableObject(_profile).AsSingle();
            Container.QueueForInject(_profile);
        }
    }
}