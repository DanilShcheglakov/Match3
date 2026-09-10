using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.EntryPoint;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.UI;
using Assets.Scripts.Game.Utils;
using Assets.Scripts.ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    internal class LifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private EndGamePanelView _endGame;
        [SerializeField] private GameProgressView _progress;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EntryPoint>();
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_endGame);
            builder.RegisterInstance(_progress);
            builder.Register<GameResourcesLoader>(Lifetime.Singleton);
            builder.Register<FXPool>(Lifetime.Singleton);
            builder.Register<Grid>(Lifetime.Singleton);            
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
            builder.Register<BlankTilesSetup>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameProgress>(Lifetime.Singleton);
            builder.Register<ScoreCalculator>(Lifetime.Singleton);
            builder.Register<EndGame>(Lifetime.Singleton);
            builder.Register <BackGroundTileSetup>(Lifetime.Singleton);
        }
    }
}
