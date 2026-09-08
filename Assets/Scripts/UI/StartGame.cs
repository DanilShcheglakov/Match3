using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.Levels;
using Assets.Scripts.SceneLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.SearchService;

namespace Assets.Scripts.UI
{
    internal class StartGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;
        private CancellationTokenSource _cts;

        public StartGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading sceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _sceneLoading = sceneLoading;
        }

        public async void Start(LevelConfig level)
        {
            _cts = new CancellationTokenSource();

            _gameData.SetCurrentLevel(level);
            _audioManager.StopMusic();
            _audioManager.PlayStopMusic();

            await _sceneLoading.UnloadAsunc(Scenes.MENU);
            await _sceneLoading.LoadAsync(Scenes.GAME);

            _audioManager.PlayGameMusic();
            _cts.Cancel();
        }
    }
}
