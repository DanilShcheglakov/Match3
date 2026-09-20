using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.FireBase.Analitycs;
using Assets.Scripts.Game.Score;
using Assets.Scripts.ResourcesLoading;
using Assets.Scripts.Tools;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.Game.UI
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _panelRectTransform;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _title;

        private IAnimation _animation;
        private IAnalyticsService _analytics;
        private ITimer _timer;
        private AudioManager _audioManager;
        private EndGame _endGame;
        private CancellationTokenSource _cts;
        private bool _isWinCondition;
        private GameData _gameData;

        private readonly string _win = "You have won!";
        private readonly string _loose = "You have loose!";

        private void OnEnable() => _closeButton.onClick.AddListener(ExitGame);

        private void OnDisable() => _closeButton.onClick.RemoveListener(ExitGame);

        [Inject]
        private void Construct(IAnimation animation, AudioManager audioManager, EndGame endGame, 
            IAnalyticsService analytics, ITimer timer, GameData gameData)
        {
            _animation = animation;
            _audioManager = audioManager;
            _endGame = endGame;
            _analytics = analytics;
            _timer = timer;
            _gameData = gameData;

            _timer.Start();
        }

        public async void ShowEndGamePanel(bool isWinCondition)
        {
            _timer.Stop();
            var time = _timer.ElapsedSeconds;
            _analytics.LogLevelEnd(_gameData.CurrenLevel.LevelNumber, isWinCondition, _timer.ElapsedSeconds);

            _isWinCondition = isWinCondition;
            _title.text = isWinCondition ? _win : _loose;
            await StartAnimation();

            _closeButton.interactable = true;
        }

        private async UniTask StartAnimation()
        {
            _cts = new CancellationTokenSource();
            _audioManager.PlayWoosh();
            _panel.SetActive(true);
            _animation.MoveUI(_panelRectTransform, new Vector3(0f, -240f, 0f), 0.5f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
            _audioManager.StopMusic();

            if (_isWinCondition)
                _audioManager.PlayWin();
            else _audioManager.PlayLoose();
        }

        private void ExitGame()
        {
            _endGame.End(_isWinCondition);
        }
    }
}
