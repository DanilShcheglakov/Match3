using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.SceneLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Score
{
    public class EndGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;

        public EndGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading sceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _sceneLoading = sceneLoading;
        }

        public async void End(bool success)
        {
            if (success && _gameData.CurrenLevel.LevelNumber == _gameData.CurrentLevelIndex)            
                _gameData.OpenNextLevel();            

            _audioManager.StopMusic();
            await _sceneLoading.UnloadAsunc(Scenes.GAME);
            await _sceneLoading.LoadAsync(Scenes.MENU);
            _audioManager.PlayMenuMusic();
        }
    }
}
