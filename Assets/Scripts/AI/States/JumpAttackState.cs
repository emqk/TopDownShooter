using UnityEngine;

public class JumpAttackState : IState
{
    Player targetPlayer;
    AI owner;
    Vector3 targetLocation;
    const float FALL_SPEED = 80;
    const float JUMP_SPEED = 8;
    const float JUMP_HEIGHT = 10;
    readonly float DAMAGE_RADIUS;

    Vector3 jumpLocation = new Vector3();
    bool jumpReached = false;
    bool floorReached = false;

    public bool AttackPerformed { get => attackPerformed; }
    bool attackPerformed = false; // state transitions observes this value

    public JumpAttackState(AI _owner, Player playerToChase, float damageRadius)
    {
        owner = _owner;
        targetPlayer = playerToChase;
        DAMAGE_RADIUS = damageRadius;
    }

    public void OnEnter()
    {
        targetLocation = owner.transform.position;
        owner.SetCollisionsActive(false);
        owner.SetNavMeshAgentActive(false);
        jumpLocation = owner.transform.position + new Vector3(0, JUMP_HEIGHT, 0);

        // Clean old values
        jumpReached = false;
        floorReached = false;
        attackPerformed = false;
    }

    public void OnExit()
    {
        owner.SetCollisionsActive(true);
        owner.SetNavMeshAgentActive(true);
    }

    public void OnUpdate()
    {
        if (jumpReached)
        {
            // If floor is reached - Deal damage to the player (if in damage range) and end this state
            if (floorReached)
            {
                float distToPlayer = Vector3.Distance(owner.transform.position, targetPlayer.transform.position);

                if (targetPlayer.IsInDamageRadius(distToPlayer, DAMAGE_RADIUS))
                {
                    DamageManager.instance.ApplyRadialDamageToPlayer(owner.GetDamage(), targetLocation, DAMAGE_RADIUS);
                }

                attackPerformed = true;
            }
            // Fall on the ground
            else
            {
                owner.transform.position = Vector3.MoveTowards(owner.transform.position, targetLocation, FALL_SPEED * Time.deltaTime);

                float distToTargetLocation = Vector3.Distance(owner.transform.position, targetLocation);
                if (distToTargetLocation <= 0.6f /* this value represents distance to be reached to 'detect' floor */)
                {
                    floorReached = true;
                    owner.transform.position = targetLocation;
                }
            }
        }
        else
        {
            //Go to jump location
            owner.transform.position = Vector3.Lerp(owner.transform.position, jumpLocation, JUMP_SPEED * Time.deltaTime);

            float distToJumpLocation = Vector3.Distance(owner.transform.position, jumpLocation);
            if (distToJumpLocation <= 0.25f /* this value represents distance to be reached to 'detect' jump target location */)
            {
                jumpReached = true;
            }
        }
    }
}