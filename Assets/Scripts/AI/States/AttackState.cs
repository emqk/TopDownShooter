using UnityEngine;

public class AttackState : IState
{
    Player targetPlayer;
    AI owner;
    Vector3 targetLocation;
    const float distToEndAttackSq = 0.4f;
    const float distToDieSq = 2;
    public bool MissedAttack { get => missedAttack; }
    bool missedAttack = false;  

    public AttackState(AI _owner, Player playerToChase)
    {
        owner = _owner;
        targetPlayer = playerToChase;
    }

    public void OnEnter()
    {
        targetLocation = targetPlayer.transform.position;
        missedAttack = false;
        owner.SetCollisionsActive(false);
    }

    public void OnExit()
    {
        owner.SetCollisionsActive(true);
    }

    public void OnUpdate()
    {
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, targetLocation, 8 * Time.deltaTime);

        float distToTargetSq = (owner.transform.position - targetLocation).sqrMagnitude;
        if (distToTargetSq <= distToEndAttackSq)
        {
            float distToPlayerSq = (owner.transform.position - targetPlayer.transform.position).sqrMagnitude;
            if (distToPlayerSq <= distToDieSq)
            {
                targetPlayer.TakeDamage(owner.GetDamage());
                owner.Suicide();
            }
            else
            {
                missedAttack = true;
            }
        }
    }
}