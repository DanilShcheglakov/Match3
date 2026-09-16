using Assets.Scripts.FireBase;
using Assets.Scripts.Save;
using Assets.Scripts.SceneLoading;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Boot
{
    internal class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SaveProgress _saveProgress;
        FirebaseInitializer _firebase;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading, SaveProgress saveProgress,FirebaseInitializer firebase)
        {
            _sceneLoading = sceneLoading;
            _saveProgress = saveProgress;
            _firebase = firebase;
        }

        public async void Initialize()
        {
            await _firebase.InitializeAsync();

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            DOTween.SetTweensCapacity(5000, 100);
            _saveProgress.LoadData();
            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}
