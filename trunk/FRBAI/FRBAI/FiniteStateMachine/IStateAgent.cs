using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatRedBallAI.FiniteStateMachine
{
    public interface IStateAgent<T>
    {
        void Update();
        void ChangeState(IState<T> pNewState);
    }
}
