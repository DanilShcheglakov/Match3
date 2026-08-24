using Assets.Scripts.Animations;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Levels;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Game.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private LevelConfig _levelConfig;
        private StateMachine _stateMachine;
        private Grid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;

        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _tilePool, _gameProgress, _scoreCalculator);
            _gameProgress.LoadLevelConfig(_gameBoard.LevelConfig.GoalScore, _gameBoard.LevelConfig.Moves);
        }

        [Inject]
        private void Construct(Grid grid, IAnimation animation, MatchFinder matchFinder, TilePool tilePool,
            GameProgress gameProgress, ScoreCalculator scoreCalculator)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _gameProgress = gameProgress;
            _scoreCalculator = scoreCalculator;
        }
    }
}