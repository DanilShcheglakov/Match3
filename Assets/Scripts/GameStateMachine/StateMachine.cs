using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GameStateMachine.AllStates;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.UI;
using Assets.Scripts.Game.Utils;
using Assets.Scripts.GameStateMachine.AllStates;
using Assets.Scripts.Input;
using Assets.Scripts.Levels;
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
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private EndGamePanelView _endGamePanelView;
        private LevelConfig _levelConfig;
        private BackGroundTileSetup _backGroundTileSetup;
        private BlankTilesSetup _blankTilesSetup;
        private FXPool _fxPool;

        public StateMachine(GameBoard gameBoard, Grid grid, IAnimation animation, MatchFinder matchFinder,
            TilePool tilePool, GameProgress gameProgress, ScoreCalculator scoreCalculator,
            AudioManager audioManager, EndGamePanelView endGame,
            LevelConfig levelConfig, BackGroundTileSetup backGroundTileSetup, BlankTilesSetup blankTilesSetup, FXPool fXPool)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
            _audioManager = audioManager;
            _endGamePanelView = endGame;
            _levelConfig = levelConfig;
            _backGroundTileSetup = backGroundTileSetup;
            _blankTilesSetup = blankTilesSetup;
            _fxPool = fXPool;

            _states = new List<IState>()
            {
               new PrepareState (this, _gameBoard, _backGroundTileSetup,_blankTilesSetup,_levelConfig),
               new PlayerTurnState(_grid, this, _animation, _audioManager),
               new SwapTilesState(_grid, this, _animation, _matchFinder, _gameProgress,_audioManager),
               new RemoveTilesState(_grid, this, _animation, _matchFinder, _scoreCalculator, _audioManager,_fxPool,_gameBoard),
               new RefillGridState(_grid, this, _animation, _matchFinder, _tilePool, _gameBoard.transform, _gameProgress,_audioManager),
               new WinState(_endGamePanelView),
               new LooseState(_endGamePanelView)
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
