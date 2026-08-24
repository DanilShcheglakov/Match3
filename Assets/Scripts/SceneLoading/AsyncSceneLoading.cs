using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneLoading
{
    internal class AsyncSceneLoading :IAsyncSceneLoading
    {
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new Dictionary<string, SceneInstance>();
        private LoadingView _loadingView;
        private CancellationTokenSource _cts;

        public AsyncSceneLoading(LoadingView loadingView) =>
            _loadingView = loadingView;

        public async UniTask LoadAsync(string sceneName)
        {
            _cts = new CancellationTokenSource();

            LoadingIsDone(false);

            await UniTask.Delay(TimeSpan.FromSeconds(2), _cts.IsCancellationRequested);

            var loadingScene = await Addressables.LoadSceneAsync(sceneName,
                LoadSceneMode.Additive).WithCancellation(_cts.Token);

            SceneManager.SetActiveScene(loadingScene.Scene);

            if (_loadedScenes.ContainsKey(sceneName) == false)
                _loadedScenes.Add(sceneName, loadingScene);

            _cts.Cancel();
        }

        public async UniTask UnloadAsunc(string sceneName)
        {
            _cts = new CancellationTokenSource();
            var sceneInstance = _loadedScenes[sceneName];

            await Addressables.UnloadSceneAsync(sceneInstance).WithCancellation(_cts.Token).AsUniTask();

            _loadedScenes.Remove(sceneName);
            _cts.Cancel();
        }

        public void LoadingIsDone(bool value) => _loadingView.SetActiveScreen(value != true);
    }
}
