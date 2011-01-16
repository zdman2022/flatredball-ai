using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatRedBallAI.FiniteStateMachine
{
    public interface IState<T>
    {
        void Enter(T pInput);
        void Execute(T pInput);
        void Exit(T pInput);
    }
}
