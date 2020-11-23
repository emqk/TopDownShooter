using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IDamageable
{
    [SerializeField] Statistic health = new Statistic();
    Transform target;

    NavMeshAgent agent;
    StateMachine stateMachine;

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Damage taken: " + damageAmount);
        health.ChangeByAmount(-damageAmount);
        NotificationManager.instance.SpawnDamageInfo(transform.position + new Vector3(0, 2, 0), damageAmount);
        if (!health.IsGreaterThanMinimum())
        {
            Destroy(gameObject);
        }
    }


    public void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChaseState chaseState = new ChaseState(agent, GameObject.FindObjectOfType<Player>());
        AttackState attackState = new AttackState(agent, GameObject.FindObjectOfType<Player>());
        stateMachine = new StateMachine(this);
        stateMachine.AddTransition(chaseState, attackState, () => true);
        stateMachine.SetState(chaseState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
