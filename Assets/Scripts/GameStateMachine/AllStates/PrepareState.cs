using Assets.Scripts.FireBase.Analitycs;
using Assets.Scripts.Game.Board;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.GameStateMachine.AllStates;
using Assets.Scripts.Levels;
using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.GameStateMachine.AllStates
{
    internal class PrepareState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private BlankTilesSetup _blankTilesSetup;
        private GameBoard _gameBoard;
        private BackGroundTileSetup _backGroundTileSetup;
        private LevelConfig _levelConfig;
        private IAnalyticsService _analytics;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard,
            BackGroundTileSetup backGroundTileSetup, BlankTilesSetup blankTilesSetup, LevelConfig levelConfig, 
            IAnalyticsService analytics)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
            _backGroundTileSetup = backGroundTileSetup;
            _blankTilesSetup = blankTilesSetup;
            _levelConfig = levelConfig;
            _analytics = analytics;
        }

        public async void Enter()
        {
            await _backGroundTileSetup.SetupBackground(_gameBoard.transform,
                _blankTilesSetup.Blanks, _levelConfig.Width, _levelConfig.Height);
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit()
        {
            _analytics.LogLevelStart(_levelConfig.LevelNumber);
            Debug.Log("Game was Started");
        }
    }
}
