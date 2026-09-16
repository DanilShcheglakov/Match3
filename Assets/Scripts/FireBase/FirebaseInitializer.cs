using Cysharp.Threading.Tasks;
using Firebase;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.FireBase
{
    internal class FirebaseInitializer 
    {
        public async UniTask InitializeAsync(CancellationToken cancellation = default)
        {
            try
            {
                var status = await FirebaseApp
                    .CheckAndFixDependenciesAsync()
                    .AsUniTask()
                    .AttachExternalCancellation(cancellation);

                if (cancellation.IsCancellationRequested)
                    return;

                if (status != DependencyStatus.Available)
                    throw new Exception($"Firebase dependencies not available: {status}");

                if (status == DependencyStatus.Available)
                {
                    Debug.Log("Firebase Initializable");
                }

                // Здесь — Инициализирую другие компоненты в будущем. Сейчас пробую инициализацию самого Firebase
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError($"[FirebaseInitializer] Initialization failed: {e}");
            }
        }
    }
}
