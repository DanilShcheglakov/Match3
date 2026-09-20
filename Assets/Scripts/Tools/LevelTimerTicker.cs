using System;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Tools
{
    public class LevelTimerTicker : ITickable,IDisposable
    {
        private readonly ITimer _timer;
        private float _logTimer;

        public LevelTimerTicker(ITimer timer)
        {
            _timer = timer;
            Application.focusChanged += OnFocusChanged;
        }

        public void Tick()
        {
            _timer.Tick(Time.unscaledDeltaTime);            
        }

        public void Dispose()
        {
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            _timer.SetFocused(hasFocus);
        }
    }
}
