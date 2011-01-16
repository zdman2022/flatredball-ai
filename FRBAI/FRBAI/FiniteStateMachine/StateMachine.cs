using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatRedBallAI.FiniteStateMachine
{
    public class StateMachine<T>
    {
        private T mOwner;
        private IState<T> mCurrentState = null;
        private IState<T> mPreviousState = null;
        private IState<T> mGlobalState = null;

        public IState<T> CurrentState { get { return mCurrentState; } }
        public IState<T> PreviousState { get { return mPreviousState; } }
        public IState<T> GlobalState { get { return mGlobalState; } }

        public StateMachine(T pOwner)
        {
            mOwner = pOwner;
        }

        /// <summary>
        /// Initializes Current State
        /// </summary>
        /// <param name="pState">State to initialize to</param>
        public void SetCurrentState(IState<T> pState) { mCurrentState = pState; }
        /// <summary>
        /// Initializes Global State
        /// </summary>
        /// <param name="pState">State to initialize to</param>
        public void SetGlobalState(IState<T> pState) { mGlobalState = pState; }
        /// <summary>
        /// Initializes Previous State
        /// </summary>
        /// <param name="pState">State to initialize to</param>
        public void SetPreviousState(IState<T> pState) { mPreviousState = pState; }

        /// <summary>
        /// Runs through current states
        /// </summary>
        public void Update()
        {
            //Call Global State
            if (mGlobalState != null) { mGlobalState.Execute(mOwner); }

            //Call Current State
            if (mCurrentState != null) { mCurrentState.Execute(mOwner); }
        }

        /// <summary>
        /// Changes to a different state.
        /// </summary>
        /// <param name="pNewState">State to change to.</param>
        public void ChangeState(IState<T> pNewState)
        {
            if (pNewState == null)
                throw new Exception("Can not change state to null");

            //Store the previous state
            mPreviousState = mCurrentState;

            //Call exit method of state that is being left
            mCurrentState.Exit(mOwner);

            //Change to the new state
            mCurrentState = pNewState;

            //Run the entry method of state that is being moved to
            mCurrentState.Enter(mOwner);
        }

        /// <summary>
        /// Change back to the previous state.  Usefull when change a state temporarily.
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(mPreviousState);
        }    
    }
}
