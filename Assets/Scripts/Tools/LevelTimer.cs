using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class LevelTimer : ITimer
    {
        private float _elapsed;
        private bool _isRunning;
        private bool _isFocused = true;

        public float ElapsedSeconds => _elapsed;

        public void Start()
        {
            _elapsed = 0f;
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Tick(float unscaledDeltaTime)
        {
            if (_isRunning && _isFocused)
                _elapsed += unscaledDeltaTime;
        }

        public void SetFocused(bool isFocused)
        {
            _isFocused = isFocused;
        }
    }
}
