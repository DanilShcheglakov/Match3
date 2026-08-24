using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SceneLoading
{
    public  interface IAsyncSceneLoading
    {
        UniTask LoadAsync(string sceneName);
        UniTask UnloadAsunc(string sceneName);
        void LoadingIsDone(bool value);
    }
}
