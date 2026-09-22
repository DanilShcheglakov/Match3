using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Assets.Scripts.FireBase.RemoteConfig
{
    internal class RemoteLoader : IRemoteLoader, IRemoteConfigService, IDisposable
    {
        private FirebaseRemoteConfig _remoteConfig;
        private bool _isSubscribedToUpdates;
        private bool _isConfigOperationInProgress;

        public async Task Initialize()
        {
            var defaults = new Dictionary<string, object>
             {
                 { "tile_set", "Fruits" }
            };

            _remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            await _remoteConfig.SetDefaultsAsync(defaults);

            await FetchConfigAsync("initial");
            SubscribeToUpdates();
        }

        public string GetString(string key, string fallback = "")
        {
            var value = _remoteConfig.GetValue(key);
            return value.Source == ValueSource.StaticValue ? fallback : value.StringValue;
        }

        public int GetInt(string key, int fallback = 0)
        {
            var value = _remoteConfig.GetValue(key);
            return value.Source == ValueSource.StaticValue ? fallback : (int)value.LongValue;
        }

        public float GetFloat(string key, float fallback = 0f)
        {
            var value = _remoteConfig.GetValue(key);
            return value.Source == ValueSource.StaticValue ? fallback : (float)value.DoubleValue;
        }

        public bool GetBool(string key, bool fallback = false)
        {
            var value = _remoteConfig.GetValue(key);
            return value.Source == ValueSource.StaticValue ? fallback : value.BooleanValue;
        }

        private async Task FetchConfigAsync(string requestSource)
        {
            if (_isConfigOperationInProgress)
            {
                Debug.LogWarning("A Remote Config operation is already in progress.");
                return;
            }

            _isConfigOperationInProgress = true;

            try
            {
                Debug.Log($"Remote Config {requestSource} fetch started.");
                await _remoteConfig.FetchAsync(TimeSpan.Zero);

                var info = _remoteConfig.Info;
                Debug.Log(
                    $"Remote Config {requestSource} fetch finished: " +
                    $"status={info.LastFetchStatus}, fetchTime={info.FetchTime:O}");

                if (info.LastFetchStatus != LastFetchStatus.Success)
                {
                    Debug.LogError(
                        $"Remote Config fetch failed: {info.LastFetchStatus}, " +
                        $"reason={info.LastFetchFailureReason}, throttledUntil={info.ThrottledEndTime:O}");
                    return;
                }

                var activated = await _remoteConfig.ActivateAsync();
                Debug.Log($"Remote Config {requestSource} activation: changed={activated}");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                _isConfigOperationInProgress = false;
            }
        }

        private async void OnConfigUpdated(object sender, ConfigUpdateEventArgs args)
        {
            if (args.Error != RemoteConfigError.None)
            {
                Debug.LogError($"Real-time Remote Config error: {args.Error}");
                return;
            }

            if (_isConfigOperationInProgress)
            {
                Debug.LogWarning("A Remote Config operation is already in progress.");
                return;
            }

            _isConfigOperationInProgress = true;

            try
            {
                Debug.Log($"Real-time Remote Config updated keys: {string.Join(", ", args.UpdatedKeys)}");

                var activated = await _remoteConfig.ActivateAsync();
                Debug.Log($"Real-time Remote Config activation: changed={activated}");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                _isConfigOperationInProgress = false;
            }
        }

        private void SubscribeToUpdates()
        {
            if (_remoteConfig == null || _isSubscribedToUpdates)
            {
                return;
            }

            if (Application.platform != RuntimePlatform.Android &&
                Application.platform != RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("Real-time Remote Config is unavailable in Unity Editor; use manual fetch.");
                return;
            }

            _remoteConfig.OnConfigUpdateListener += OnConfigUpdated;
            _isSubscribedToUpdates = true;
            Debug.Log("Real-time Remote Config listener connected.");
        }

        private void UnSubscribeToUpdates()
        {
            if (_remoteConfig == null || !_isSubscribedToUpdates)            
                return;            

            _remoteConfig.OnConfigUpdateListener -= OnConfigUpdated;
            _isSubscribedToUpdates = false;
        }

        public void Dispose()
        {
            UnSubscribeToUpdates();
        }
    }
}
