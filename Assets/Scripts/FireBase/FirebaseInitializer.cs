using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Crashlytics;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.FireBase
{
    internal class FirebaseInitializer
    {
        public FirebaseApp App { get; private set; }
        public bool IsReady => App != null;

        public async UniTask InitializeAsync(CancellationToken cancellation = default)
        {
            try
            {
                var status = await FirebaseApp
              .CheckAndFixDependenciesAsync()
              .AsUniTask()
              .AttachExternalCancellation(cancellation);

                if (status != DependencyStatus.Available)
                    throw new Exception($"[Firebase] Dependencies not available: {status}");

                App = FirebaseApp.DefaultInstance;
                Crashlytics.ReportUncaughtExceptionsAsFatal = true;                             

                Debug.Log("[FirebaseInitializer] Firebase initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                throw;
            }
        }
    }
}
