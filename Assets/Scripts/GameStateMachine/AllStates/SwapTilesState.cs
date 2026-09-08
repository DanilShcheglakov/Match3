using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using IState = Assets.Scripts.Game.GameStateMachine.IState;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    public class SwapTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _switcher;
        IAnimation _animation;
        private MatchFinder _matchFinder;
        private GameProgress _gameProgress;
        private AudioManager _audioManager;

        public SwapTilesState(Grid grid, IStateSwitcher switcher, IAnimation animation, 
            MatchFinder matchFinder, GameProgress gameProgress, AudioManager audioManager)
        {
            _grid = grid;
            _switcher = switcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _gameProgress = gameProgress;
            _audioManager = audioManager;   
        }

        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            _audioManager.PlayWoosh();
            await SwapTiles(_grid.CurrentPosition, _grid.TargetPosition);

            if (_matchFinder.CheckBoardForMatches(_grid) == false)
            {
                _audioManager.PlayNoMatch();
                await SwapTiles(_grid.TargetPosition, _grid.CurrentPosition);
                _switcher.SwitchState<PlayerTurnState>();
            }
            else 
            {
                _audioManager.PlayMatch();
                _gameProgress.SpendMoves(); 
                _switcher.SwitchState<RemoveTilesState>();
            }
        }

        public void Exit()
        {
            _cts?.Cancel();
        }

        private async UniTask SwapTiles(Vector2Int current, Vector2Int target)
        {
            var currentTile = _grid.Getvalue(current.x, current.y);
            var targetTile = _grid.Getvalue(target.x, target.y);

            MoveAnimation(currentTile, target);
            MoveAnimation(targetTile, current);

            _grid.Setvalue(current.x, current.y, targetTile);
            _grid.Setvalue(target.x, target.y, currentTile);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
        }

        private void MoveAnimation(Tile tileToMove, Vector2Int target) =>
            _animation.MoveTile(tileToMove, _grid.GridToWorld(target.x, target.y), DG.Tweening.Ease.OutCubic);


        public void Dispose()
        {
            _cts?.Cancel();
        }
    }
}