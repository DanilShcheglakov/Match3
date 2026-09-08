using Assets.Scripts.Audio;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    public class LooseState : IState
    {
        private EndGamePanelView _endGame;

        public LooseState(EndGamePanelView endGame)
        {
            _endGame = endGame;
        }

        public void Enter()
        {
            _endGame.ShowEndGamePanel(false);
        }

        public void Exit()
        {
        }
    }
}
