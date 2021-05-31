using UnityEngine;

public class ExplosionAttackState : IState
{
    // Data
    BombAI owner;
    readonly float DAMAGE_RADIUS;

    // Runtime data
    float timeToExplode = 2.0f;

    public ExplosionAttackState(BombAI _owner, float damageRadius)
    {
        owner = _owner;
        DAMAGE_RADIUS = damageRadius;
    }

    public void OnEnter()
    {
        owner.ShowSmoke();
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        timeToExplode -= Time.deltaTime;
        
        if (timeToExplode <= 0)
        {
            owner.Explode();
        }
    }
}
