using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.Utils;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    internal class RemoveTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _switcher;
        IAnimation _animation;
        private MatchFinder _matchFinder;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private FXPool _fxPool;
        private GameBoard _gameBoard;

        public RemoveTilesState(Grid grid, IStateSwitcher switcher, IAnimation animation,
            MatchFinder matchFinder, ScoreCalculator scoreCalculator, AudioManager audioManager,
            FXPool fXPool, GameBoard gameBoard)
        {
            _grid = grid;
            _switcher = switcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _scoreCalculator = scoreCalculator;
            _audioManager = audioManager;
            _fxPool = fXPool;
            _gameBoard = gameBoard;
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();

            _scoreCalculator.CalculateScoreToAdd(_matchFinder.CurrentMetchResult.MatchDirection);

            await RemoveTiles(_matchFinder.TilesToRemove, _grid);
            _switcher.SwitchState<RefillGridState>();
        }

        public void Exit()
        {
            _matchFinder.ClearTilesToRemove();
            _cts?.Cancel();
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }

        private async UniTask RemoveTiles(List<Tile> tilesToRemove, Grid grid)
        {
            foreach (var tile in tilesToRemove)
            {
                _audioManager.PlayRemove();
                var pos = grid.WorldToGrid(tile.transform.position);
                grid.Setvalue(pos.x, pos.y, null);
                await _animation.HideTile(tile.gameObject);
                _fxPool.GetFXFromPool(tile.transform.position, _gameBoard.transform);
            }
            _cts.Cancel();
        }
    }
}
