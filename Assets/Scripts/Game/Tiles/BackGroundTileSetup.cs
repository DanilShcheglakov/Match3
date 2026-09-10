using Assets.Scripts.Animations;
using Assets.Scripts.ResourcesLoading;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Game.Tiles
{
    public class BackGroundTileSetup : IDisposable
    {
        private readonly GameResourcesLoader _resourcesLoader;
        private CancellationTokenSource _cts;
        private IAnimation _animation;
        private IObjectResolver _objectResolver;

        public BackGroundTileSetup(GameResourcesLoader resourcesLoader, IObjectResolver objectResolver,
            IAnimation animation)
        {
            _resourcesLoader = resourcesLoader;
            _objectResolver = objectResolver;
            _animation = animation;
        }

        public void Dispose()
        {
            _cts?.Dispose();
            _objectResolver?.Dispose();
        }

        public async UniTask SetupBackground(Transform parent, bool[,] blanks,
            int width, int height)
        {
            _cts = new CancellationTokenSource();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (blanks[x, y])
                        continue;

                    GameObject backgroundTile = CreateBackgroundTile(new Vector3(x, y, 0.1f), parent);

                    if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0)
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.DarkTile;
                    else
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.LightTile;

                    var duration = UnityEngine.Random.Range(0.8f, 1.5f);
                    _animation.Reveal(backgroundTile, duration);
                }
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }

        private GameObject CreateBackgroundTile(Vector3 position, Transform parent) =>
           _objectResolver.Instantiate(_resourcesLoader.BackgroundTilePrefab,
               position, Quaternion.identity, parent);
    }
}