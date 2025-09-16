using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_StateMachine
{
    public Enemies_State currentState { get; private set; }

    public void Initialize(Enemies_State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(Enemies_State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
