using Assets.Scripts.Animations;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    public class RefillGridState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _switcher;
        IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;

        private readonly Transform _parent;

        private List<Vector2Int> _tilesToRefill = new List<Vector2Int>();

        public RefillGridState(Grid grid, IStateSwitcher switcher, IAnimation animation,
            MatchFinder matchFinder, TilePool tilePool, Transform parent, GameProgress gameProgress)
        {
            _grid = grid;
            _switcher = switcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _parent = parent;
            _gameProgress = gameProgress;
        }

        public void Dispose()
        {
            _cts?.Cancel();
        }

        public async void Enter()
        {
            await FallTiles();
            await Refill();
            if (_matchFinder.CheckBoardForMatches(_grid))
            {
                _switcher.SwitchState<RemoveTilesState>();
                //playSound
            }
            else
            {
                //playSound
                CheckEndGame();
            }
        }

        private void CheckEndGame()
        {
            if (_gameProgress.CheckGoalScore())
                _switcher.SwitchState<WinState>();
            else if (_gameProgress.Moves <= 0)
                _switcher.SwitchState<LooseState>();
            else
                _switcher.SwitchState<PlayerTurnState>();
        }

        public void Exit()
        {
            _cts?.Cancel();
        }

        private async UniTask FallTiles()
        {
            _cts = new CancellationTokenSource();

            for (int x = 0; x < _grid.Wigth; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.Getvalue(x, y)) continue;

                    for (int i = y + 1; i < _grid.Height; i++)
                    {
                        if (_grid.Getvalue(x, i) == null) continue;
                        if (_grid.Getvalue(x, i).IsInteractable == false) continue;

                        var tile = _grid.Getvalue(x, i);
                        _grid.Setvalue(x, y, tile);
                        _animation.MoveTile(tile, _grid.GridToWorld(x, y), DG.Tweening.Ease.InBack);
                        _grid.Setvalue(x, i, null);
                        _tilesToRefill.Add(new Vector2Int(x, i));
                        break;
                    }
                }
            }
            //playSound
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }

        private async UniTask Refill()
        {
            _cts = new CancellationTokenSource();
            for (int x = 0; x < _grid.Wigth; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.Getvalue(x, y) != null) continue;
                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), _parent);
                    tile.gameObject.SetActive(true);
                    _grid.Setvalue(x, y, tile);
                    _animation.Reveal(tile.gameObject, 0.2f);
                    //playSound
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f), _cts.IsCancellationRequested);
                }
            }
            _cts.Cancel();
        }
    }
}
