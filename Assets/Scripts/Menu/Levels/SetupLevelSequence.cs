using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Menu.Levels
{
    public class SetupLevelSequence
    {
        public LevelSequnce CurrentlevelSequence { get; private set; }

        public async UniTask Setup(int currentLevel)
        {
            if (currentLevel <= 5)
            {
                await LoadLevels("Levels 1-5");
                Debug.Log("loaded 'Levels 1-5'");
            }
            else
            {
                await LoadLevels("Levels 6-10");
                Debug.Log("loaded 'Levels 6-10'");
            }
        }

        private async UniTask LoadLevels(string key)
        {
            AsyncOperationHandle<LevelSequnce> levels =
                  Addressables.LoadAssetAsync<LevelSequnce>(key);

            await levels.ToUniTask();

            if (levels.Status==AsyncOperationStatus.Succeeded)
            {
                CurrentlevelSequence = levels.Result;
                Addressables.Release(levels);
            }          

            
        }
    }
}
