using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    internal class PlayerTurnState : IState, IDisposable
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private readonly InputReader _reader;
        private readonly Grid _grid;
        private readonly IStateSwitcher _switcher;
        private readonly Camera _camera;
        private IAnimation _animation;
        private AudioManager _audioManager;

        public PlayerTurnState(Grid grid, IStateSwitcher switcher, IAnimation animation, AudioManager audioManager)
        {
            _grid = grid;
            _switcher = switcher;
            _animation = animation;
            _reader = new InputReader();
            _camera = Camera.main;
            _audioManager = audioManager;

            _reader.Click += OnTileClick;
        }

        private void OnTileClick()
        {
            var clickPosition = _grid.WorldToGrid(_camera.ScreenToWorldPoint(_reader.Position()));

            if (IsValidPosition(clickPosition) == false || IsBlankBosition(clickPosition))
                return;

            if (_grid.CurrentPosition == _emptyPosition)
            {
                _audioManager.PlayClick();
                _grid.SetCurrentPosition(clickPosition);
                _animation.AnimateTile(_grid.Getvalue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }

            else if (_grid.CurrentPosition == clickPosition)
            {
                _audioManager.PlayDeselect();
                DeselectTile();
            }
            else if (_grid.CurrentPosition != clickPosition && IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                _animation.AnimateTile(_grid.Getvalue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
                _switcher.SwitchState<SwapTilesState>();
            }
        }

        private bool IsSwappable(Vector2Int currentPos, Vector2Int targetTilePos)
        {
            return Mathf.Abs(currentPos.x - targetTilePos.x) + Mathf.Abs(currentPos.y - targetTilePos.y) == 1;
        }

        private void DeselectTile()
        {
            _animation.AnimateTile(_grid.Getvalue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }

        private bool IsBlankBosition(Vector2Int gridPosition) =>
             _grid.Getvalue(gridPosition.x, gridPosition.y).TileConfig.TileKind == Game.Tiles.TileKind.Blank;

        private bool IsValidPosition(Vector2Int gridPosition) =>
            gridPosition.x >= 0 && gridPosition.x < _grid.Wigth &&
            gridPosition.y >= 0 && gridPosition.y < _grid.Height;

        public void Enter()
        {
            _reader.EnableInputs(true);
            DeselectTile();
        }

        public void Exit()
        {
            _reader.EnableInputs(false);
        }

        public void Dispose()
        {
            _reader.Click -= OnTileClick;
        }
    }
}
