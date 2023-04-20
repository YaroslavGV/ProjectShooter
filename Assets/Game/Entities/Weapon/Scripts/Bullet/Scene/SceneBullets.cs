using UnityEngine;
using System.Collections.Generic;

namespace Weapon
{
    public class SceneBullets : MonoBehaviour
    {
        private List<UsageBulletAssetHandler> _assetHandlers = new List<UsageBulletAssetHandler>();


        private void OnDestroy ()
        {
            foreach (UsageBulletAssetHandler assetHandler in _assetHandlers)
                assetHandler.OnUnused -= OnUnusedHandler;
        }

        public BulletHandler AddUser (IBulletAssetUser user)
        {
            UsageBulletAssetHandler assetHandler = GetAsset(user.Asset);
            if (assetHandler == null)
            {
                assetHandler = CreateAsset(user.Asset);
                assetHandler.OnUnused += OnUnusedHandler;
                _assetHandlers.Add(assetHandler);
            }
            assetHandler.AddUser(user);
            return assetHandler.BulletHandler;
        }

        public void RemoveUser (IBulletAssetUser user)
        {
            UsageBulletAssetHandler assetHandler = GetAsset(user.Asset);
            if (assetHandler != null)
                assetHandler.RemoveUser(user);
        }

        private UsageBulletAssetHandler CreateAsset (BulletTemplateAsset templateAsset)
        {
            GameObject handlerObject = new GameObject();
            handlerObject.transform.SetParent(transform);
            handlerObject.name = templateAsset.Bullet.name;

            UsageBulletAssetHandler handler = handlerObject.AddComponent<UsageBulletAssetHandler>();
            handler.Initialize(templateAsset);
            return handler;
        }

        private UsageBulletAssetHandler GetAsset (BulletTemplateAsset asset)
        {
            foreach (UsageBulletAssetHandler assetHandler in _assetHandlers)
                if (assetHandler.Asset == asset)
                    return assetHandler;
            return null;
        }

        private void OnUnusedHandler (UsageBulletAssetHandler handler)
        {
            _assetHandlers.Remove(handler);
            Destroy(handler.gameObject);
        }
    }
}
