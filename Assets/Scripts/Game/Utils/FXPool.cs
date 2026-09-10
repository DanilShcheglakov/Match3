using Assets.Scripts.ResourcesLoading;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Game.Utils
{
    public class FXPool
    {
        private readonly List<GameObject> _FXPool = new List<GameObject>();
        private readonly GameResourcesLoader _resourcesLoader;
        private GameObject _prefabFX;
        private IObjectResolver _objectResolver;

        public FXPool(GameResourcesLoader resourcesLoader, IObjectResolver objectResolver)
        {
            _resourcesLoader = resourcesLoader;
            _objectResolver = objectResolver;
        }

        public GameObject GetFXFromPool(Vector3 position, Transform transform)
        {
            for (int i = 0; i < _FXPool.Count; i++)
            {
                if (_FXPool[i].activeInHierarchy)
                    continue;

                _FXPool[i].gameObject.transform.position = position;
                _FXPool[i].SetActive(true);
                return _FXPool[i];
            }

            var FX = CreateFX(position, transform);
            FX.SetActive(true);
            return FX;
        }

        private GameObject CreateFX(Vector3 position, Transform transform)
        {
            var FX = _objectResolver.Instantiate(_resourcesLoader.FXPrefab,
                position + Vector3.forward, Quaternion.identity, transform);

            _FXPool.Add(FX);
            return FX;
        }
    }
}
