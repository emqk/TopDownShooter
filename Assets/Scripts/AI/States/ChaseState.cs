﻿using UnityEngine.AI;

public class ChaseState : IState
{
    Player targetPlayer;
    NavMeshAgent ownerAgent;

    public ChaseState(NavMeshAgent _ownerAgent, Player playerToChase)
    {
        ownerAgent = _ownerAgent;
        targetPlayer = playerToChase;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        ownerAgent.SetDestination(targetPlayer.transform.position);
    }
}