using Assets.Scripts.Game.Board;
using Assets.Scripts.GameStateMachine.AllStates;
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
        private GameBoard _gameBoard;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard)
        {
            _stateSwitcher = stateSwitcher;
            _gameBoard = gameBoard;
        }

        public void Enter()
        {
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit()
        {
            Debug.Log("Game was Started");
        }
    }
}
