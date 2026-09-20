using Assets.Scripts.FireBase;
using Assets.Scripts.FireBase.Analitycs;
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
        private IAnalyticsService _analyticsService;
        private SaveProgress _saveProgress;
        FirebaseInitializer _firebase;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading, SaveProgress saveProgress,
            FirebaseInitializer firebase, IAnalyticsService analyticsService)
        {
            _sceneLoading = sceneLoading;
            _saveProgress = saveProgress;
            _firebase = firebase;
            _analyticsService = analyticsService;
        }

        public async void Initialize()
        {
            try
            {
                await _firebase.InitializeAsync();
                _analyticsService.Initialize();

            }
            catch (OperationCanceledException) { return; }
            catch (Exception e)
            {
                Debug.LogError($"[Boot] Firebase failed, continuing offline: {e}");
            }


            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            DOTween.SetTweensCapacity(5000, 100);
            _saveProgress.LoadData();

            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}
