using UnityEngine;

public class BombAI : AI
{
    [SerializeField] float blowRadius = 3;
    [SerializeField] ParticleSystem smokeParticle;

    protected override void InitBehaviour()
    {
        ChaseState chaseState = new ChaseState(agent, player);
        ExplosionAttackState explosionAttackState = new ExplosionAttackState(this);

        //Explode
        stateMachine.AddTransition(chaseState, explosionAttackState,
        () =>
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            return player.IsInDamageRadius(dist, blowRadius);
        });

        stateMachine.SetState(chaseState);
    }

    public void ShowSmoke()
    {
        smokeParticle.gameObject.SetActive(true);
    }

    public void Explode()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (player.IsInDamageRadius(distToPlayer, blowRadius))
        {
            DamageManager.instance.ApplyRadialDamageToPlayer(GetDamage(), transform.position, blowRadius);
        }

        Suicide();
    }
}
