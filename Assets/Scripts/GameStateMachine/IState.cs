using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.GameStateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
