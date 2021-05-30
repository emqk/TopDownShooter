using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AI : MonoBehaviour, IDamageable
{
    [SerializeField] int defaultHealth = 100;
    [SerializeField] Vector2Int damageRange;
    [SerializeField] Vector2Int rewardRange;

    [SerializeField] AudioClip deathSound;

    Statistic health = new Statistic();

    protected NavMeshAgent agent;
    protected StateMachine stateMachine;
    protected Player player;
    bool deathFromAttack = false;
    float characterRadius;

    /////////////// Implement interfaces - BEGIN ///////////////

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

    public bool IsInDamageRadius(float distance, float radius)
    {
        return distance <= radius + characterRadius;
    }

    /////////////// Implement interfaces - END ///////////////


    /////////////// To implement - START ///////////////
    
    protected abstract void InitBehaviour();

    /////////////// To implement - END ///////////////


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
        player = PlayerController.instance.GetControlledPlayer();
        agent = GetComponent<NavMeshAgent>();

        health = new Statistic(0, defaultHealth, defaultHealth);
        stateMachine = new StateMachine(this);

        characterRadius = agent.radius;

        InitBehaviour();
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
