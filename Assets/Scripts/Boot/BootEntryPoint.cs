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

        public BootEntryPoint(IAsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            DOTween.SetTweensCapacity(5000, 100);

            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}
