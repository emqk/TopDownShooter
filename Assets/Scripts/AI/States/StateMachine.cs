using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

class Transition
{
    public Func<bool> Condition { get; }

    public IState to { get; }

    public Transition(IState _to, Func<bool> _condition)
    {
        to = _to;
        Condition = _condition;
    }
}

public class StateMachine
{
    Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    List<Transition> currentTransitions = new List<Transition>();
    List<Transition> emptyList = new List<Transition>();
    IState currentState;
    AI owner;

    public StateMachine(AI _owner)
    {
        owner = _owner;
    }

    public void Update()
    {
        currentState?.OnUpdate();

        Transition transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.to);
        }
    }

    public void SetState(IState newState)
    {
        if (currentState != null)
            currentState.OnExit();

        currentState = newState;
        currentState.OnEnter();

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
        {
            currentTransitions = emptyList;
        }
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        List<Transition> trans;
        if (!transitions.TryGetValue(from.GetType(), out trans))
        {
            trans = new List<Transition>();
            transitions[from.GetType()] = trans;
        }

        trans.Add(new Transition(to, condition));
    }

    Transition GetTransition()
    {
        foreach (var transition in currentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }

    public void End()
    {
        currentState?.OnExit();
        currentState = null;
    }
}
