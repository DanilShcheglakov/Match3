using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.FireBase.Analitycs;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.UI;
using Assets.Scripts.Game.Utils;
using Assets.Scripts.Levels;
using Assets.Scripts.ResourcesLoading;
using Assets.Scripts.SceneLoading;
using Assets.Scripts.Tools;
using VContainer.Unity;

namespace Assets.Scripts.Game.EntryPoint
{
    public class EntryPoint : IInitializable
    {
        private LevelConfig _levelConfig;
        private ScoreCalculator _scoreCalculator;
        private BlankTilesSetup _blankTilesSetup;
        private StateMachine _stateMachine;
        private GameProgress _gameProgress;
        private MatchFinder _matchFinder;
        private Grid _grid;
        private GameBoard _gameBoard;
        private GameDebug _debug;
        private TilePool _tilePool;
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAnimation _animation;
        private GameResourcesLoader _resourcesLoader;
        private SetupCamera _setupCamera;
        private IAsyncSceneLoading _sceneLoading;
        private EndGamePanelView _endGame;
        private BackGroundTileSetup _backGroundTileSetup;
        private FXPool _fxPool;
        private IAnalyticsService _analytics;

        private bool _isDebugging;

        public EntryPoint( ScoreCalculator scoreCalculator, BlankTilesSetup blankTilesSetup,
            GameProgress gameProgress, MatchFinder matchFinder, Grid grid,
            GameBoard gameBoard, GameDebug debug, TilePool tilePool, GameData gameData, AudioManager audioManager,
            IAnimation animation, GameResourcesLoader resourcesLoader, SetupCamera setupCamera, IAsyncSceneLoading sceneLoading,
            EndGamePanelView endGame, BackGroundTileSetup backGroundTileSetup, FXPool fxPool, IAnalyticsService analytics)
        {
            _scoreCalculator = scoreCalculator;
            _blankTilesSetup = blankTilesSetup;
            _gameProgress = gameProgress;
            _matchFinder = matchFinder;
            _grid = grid;
            _gameBoard = gameBoard;
            _debug = debug;
            _tilePool = tilePool;
            _gameData = gameData;
            _audioManager = audioManager;
            _animation = animation;
            _resourcesLoader = resourcesLoader;
            _setupCamera = setupCamera;
            _sceneLoading = sceneLoading;
            _endGame = endGame;
            _backGroundTileSetup = backGroundTileSetup;
            _fxPool = fxPool;
            _analytics = analytics;
        }

        public async void Initialize()
        {
            _levelConfig = _gameData.CurrenLevel;

            if (_isDebugging)
                _debug.ShowDebug(_gameBoard.transform);

            _grid.SetUpGrid(_levelConfig.Width, _levelConfig.Height);
            _gameProgress.LoadLevelConfig(_levelConfig.GoalScore, _levelConfig.Moves);

            await _resourcesLoader.Load();

            _setupCamera.SetCameta(_grid.Wigth, _grid.Height, false);
            _blankTilesSetup.SetupBlanks(_levelConfig);

            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _tilePool,
                _gameProgress, _scoreCalculator, _audioManager, _endGame, _levelConfig, _backGroundTileSetup, 
                _blankTilesSetup, _fxPool, _analytics);

            _sceneLoading.LoadingIsDone(true);
        }
    }
}