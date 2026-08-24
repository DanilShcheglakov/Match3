using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.Utils;
using Assets.Scripts.ResourcesLoading;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Animations;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Score;

namespace Assets.Scripts.DI
{
    internal class LifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameResourcesLoader _loader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_loader);
            builder.Register<Grid>(Lifetime.Singleton);            
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Singleton);
            builder.Register<BlankTilesSetup>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameProgress>(Lifetime.Singleton);
            builder.Register<ScoreCalculator>(Lifetime.Singleton);
        }
    }
}
