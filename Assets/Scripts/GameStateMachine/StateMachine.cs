using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GameStateMachine.AllStates;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.GameStateMachine.AllStates;
using Assets.Scripts.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Game.GameStateMachine
{
    public class StateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;
        private Grid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;

        public StateMachine(GameBoard gameBoard, Grid grid, IAnimation animation, MatchFinder matchFinder, TilePool tilePool)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;

            _states = new List<IState>()
            {
               new PrepareState (this, _gameBoard),
               new PlayerTurnState(_grid, this, _animation),
               new SwapTilesState(_grid, this, _animation, _matchFinder),
               new RemoveTilesState(_grid, this, _animation, _matchFinder),
               new RefillGridState(_grid, this, _animation, _matchFinder, _tilePool, _gameBoard.transform)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
