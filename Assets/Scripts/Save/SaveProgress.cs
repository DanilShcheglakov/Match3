using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Save
{
    public class SaveProgress
    {
        private const string CurrentLevel = "Level";
        private const string Sound = "Sound";
        private GameData _gameData;

        public SaveProgress(GameData gameData) => _gameData = gameData;

        public void SaveData()
        {
            PlayerPrefs.SetInt(CurrentLevel, _gameData.CurrentLevelIndex);

            if (_gameData.IsEnabledSound)
                PlayerPrefs.SetInt(Sound, 1);
            else
                PlayerPrefs.SetInt(Sound, 0);
        }

        public void LoadData()
        {
            if (PlayerPrefs.GetInt(CurrentLevel) >= 1)
            {
                _gameData.SetCurrentLevelIndex(PlayerPrefs.GetInt(CurrentLevel));
            }
            else
            {
                _gameData.SetCurrentLevelIndex(1);
            }

            if (PlayerPrefs.GetInt(Sound) == 0)
            {
                _gameData.SetEnabledSound(false);
            }
            else
            {
                _gameData.SetEnabledSound(true);
            }

        }
    }
}
