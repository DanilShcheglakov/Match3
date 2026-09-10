using Assets.Scripts.Data;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Levels;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.ResourcesLoading
{
    public class GameResourcesLoader : IDisposable
    {
        private GameData _gameData;
        private CancellationTokenSource _cts;

        public GameResourcesLoader(GameData gameData) => _gameData = gameData;

        public GameObject TilePrefab { get; private set; }
        public GameObject BackgroundTilePrefab { get; private set; }
        public TileConfig BlankConfig { get; private set; }
        public GameObject FXPrefab { get; private set; }
        public Sprite DarkTile { get; private set; }
        public Sprite LightTile { get; private set; }
        public List<TileConfig> CurrentTileSet { get; private set; }

        public async UniTask Load()
        {
            _cts = new CancellationTokenSource();
            CurrentTileSet = new List<TileConfig>();

            await LoadSet();
            await LoadTilePrefabs();
            await LoadBackgroundSprites();
            BlankConfig = await Loader<TileConfig>("Blank");
            _cts.Cancel();
        }

        private async UniTask<T> Loader<T>(string key)
        {
            var assetHandler = Addressables.LoadAssetAsync<T>(key);
            var asset = await assetHandler.ToUniTask();

            return assetHandler.Status == AsyncOperationStatus.Succeeded ? asset : default;
        }

        private async UniTask LoadSet()
        {
            _cts = new CancellationTokenSource();
            switch (_gameData.CurrenLevel.TileSets)
            {
                case TilesSets.Fruits:
                    var tileSets = await Loader<TileSetConfig>("Fruits");
                    CurrentTileSet = tileSets.Set;
                    break;

                case TilesSets.Gem:
                    tileSets = await Loader<TileSetConfig>("Gem");
                    CurrentTileSet = tileSets.Set;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            _cts.Cancel();
        }

        private async UniTask LoadTilePrefabs()
        {
            _cts = new CancellationTokenSource();
            TilePrefab = await Loader<GameObject>("TilePrefab");
            BackgroundTilePrefab = await Loader<GameObject>("BackGroundTile");
            FXPrefab = await Loader<GameObject>("FXPrefab");
            _cts.Cancel();
        }

        private async UniTask LoadBackgroundSprites()
        {
            _cts = new CancellationTokenSource();
            var bgBoard = await Loader<IList<Sprite>>("bg_Board");

            DarkTile = bgBoard[0];
            LightTile = bgBoard[1];
            _cts.Cancel();
        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}