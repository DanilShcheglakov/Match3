using Assets.Scripts.Game.GameStateMachine;
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
        public void Enter()
        {
            Debug.Log("WINNNNNN");
        }

        public void Exit()
        {
        }
    }
}
