using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IDamageable
{
    public int Damage { get => 10; }
    [SerializeField] int defaultHealth = 100;
    Statistic health = new Statistic();
    [SerializeField] Vector2Int rewardRange;

    [SerializeField] AudioClip deathSound;

    NavMeshAgent agent;
    StateMachine stateMachine;

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Damage taken: " + damageAmount);
        health.ChangeByAmount(-damageAmount);
        NotificationManager.instance.SpawnDamageInfo(transform.position + new Vector3(0, 2, 0), damageAmount);

        if (!health.IsGreaterThanMinimum())
        {
            Die();
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        gameObject.SetActive(false);
        SpawnManager.RegisterKill(this);
        ParticleManager.instance.SpawnBlowUpParticle(transform.position, transform.rotation);
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = new Statistic(0, defaultHealth, defaultHealth);

        Player player = GameObject.FindObjectOfType<Player>();
        ChaseState chaseState = new ChaseState(agent, player);
        AttackState attackState = new AttackState(this, player);
        MissedAttackState missedAttackState = new MissedAttackState(0.8f);
        stateMachine = new StateMachine(this);
        //Attack player
        stateMachine.AddTransition(chaseState, attackState,
        () =>
        {
            float distSq = (transform.position - player.transform.position).sqrMagnitude;
            return distSq < 12;
        });
        //Go back to chase when missed attack
        stateMachine.AddTransition(attackState, missedAttackState,
        () =>
        {
            return attackState.MissedAttack;
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

    public int GetReward()
    {
        //Adding 1 because random for int is inclusive for max
        return Random.Range(rewardRange.x, rewardRange.y + 1);
    }
}
