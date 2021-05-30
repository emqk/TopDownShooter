using UnityEngine;

public class LandAI : AI
{
    [SerializeField] float damageRadius = 3;

    protected override void InitBehaviour()
    {
        ChaseState chaseState = new ChaseState(agent, player);
        AttackState attackState = new AttackState(this, player);
        JumpAttackState jumpAttackState = new JumpAttackState(this, player, damageRadius);
        MissedAttackState missedAttackState = new MissedAttackState(0.8f);

        //Attack player
        //stateMachine.AddTransition(chaseState, attackState,
        //() =>
        //{
        //    float distSq = (transform.position - player.transform.position).sqrMagnitude;
        //    return distSq < 12;
        //});

        //Attack player
        stateMachine.AddTransition(chaseState, jumpAttackState,
        () =>
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            return player.IsInDamageRadius(dist, damageRadius);
        });


        //Go back to chase when missed attack
        stateMachine.AddTransition(jumpAttackState, missedAttackState,
        () =>
        {
            return jumpAttackState.AttackPerformed;
        });

        //Wait after missed attack
        stateMachine.AddTransition(missedAttackState, chaseState,
        () =>
        {
            return missedAttackState.WaitingFinished();
        });

        stateMachine.SetState(chaseState);
    }
}
