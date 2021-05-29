using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IDamageable
{
    [SerializeField] int defaultHealth = 100;
    [SerializeField] Vector2Int damageRange;
    [SerializeField] Vector2Int rewardRange;

    [SerializeField] AudioClip deathSound;

    Statistic health = new Statistic();

    NavMeshAgent agent;
    StateMachine stateMachine;
    bool deathFromAttack = false;


    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        NotificationManager.instance.SpawnDamageInfo(transform.position + new Vector3(0, 2, 0), damageAmount);

        if (!health.IsGreaterThanMinimum())
        {
            Die();
        }
    }

    public void Die()
    {
        AudioManager.PlayClip2D(deathSound);
        gameObject.SetActive(false);
        SpawnManager.RegisterKill(this);
        ParticleManager.instance.SpawnBlowUpParticle(transform.position, transform.rotation);
    }

    /// <summary>
    /// Die but without kill reward
    /// </summary>
    public void Suicide()
    {
        deathFromAttack = true;
        Die();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = new Statistic(0, defaultHealth, defaultHealth);

        Player player = PlayerController.instance.GetControlledPlayer();
        ChaseState chaseState = new ChaseState(agent, player);
        AttackState attackState = new AttackState(this, player);
        JumpAttackState jumpAttackState = new JumpAttackState(this, player);
        MissedAttackState missedAttackState = new MissedAttackState(0.8f);
        stateMachine = new StateMachine(this);

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
            float distSq = (transform.position - player.transform.position).sqrMagnitude;
            return distSq < 12;
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

    void Update()
    {
        stateMachine.Update();
    }

    public void SetCollisionsActive(bool active)
    {
        foreach (Collider coll in GetComponents<Collider>())
        {
            coll.enabled = active;
        }
    }

    public void SetNavMeshAgentActive(bool active)
    {
        agent.enabled = active;
    }

    public int GetDamage()
    {
        //Adding 1 because random for int is inclusive for max
        return Random.Range(damageRange.x, damageRange.y + 1);
    }

    public int GetReward()
    {
        if (deathFromAttack)
        {
            //If died from attack don't give reward
            return 0;
        }
        else
        {
            //Adding 1 because random for int is inclusive for max
            return Random.Range(rewardRange.x, rewardRange.y + 1);
        }
    }
}
