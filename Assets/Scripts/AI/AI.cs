using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IDamageable
{
    [SerializeField] Statistic health = new Statistic();
    Transform target;

    NavMeshAgent agent;

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
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
