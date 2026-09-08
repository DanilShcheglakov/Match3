using Assets.Scripts.Audio;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.GameStateMachine.AllStates
{
    public class WinState : IState
    {
        private EndGamePanelView _endGame;

        public WinState(EndGamePanelView endGame)
        {
            _endGame = endGame;
        }

        public void Enter()
        {
            _endGame.ShowEndGamePanel(true);
        }

        public void Exit()
        {
        }
    }
}
