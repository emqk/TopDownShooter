using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    Player targetPlayer;
    NavMeshAgent ownerAgent;

    public AttackState(NavMeshAgent _ownerAgent, Player playerToChase)
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
        Debug.Log("Attack");
    }
}