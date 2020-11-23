using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    Player targetPlayer;
    NavMeshAgent ownerAgent;
    readonly float distToAttack = 5;

    public ChaseState(NavMeshAgent _ownerAgent, Player playerToChase)
    {
        ownerAgent = _ownerAgent;
        targetPlayer = playerToChase;
        distToAttack *= distToAttack;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        Vector3 playerPos = targetPlayer.transform.position;
        ownerAgent.SetDestination(playerPos);
        Debug.Log("Chase");
    }
}