using Assets.Scripts.Game.GameStateMachine;
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
        public void Enter()
        {
            Debug.Log("Loose(((");
        }

        public void Exit()
        {
        }
    }
}
