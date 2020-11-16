using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IDamageable
{
    [SerializeField] Transform target;
    [SerializeField] Statistic health = new Statistic();

    NavMeshAgent agent;

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        if (!health.IsGreaterThanZero())
        {
            Destroy(gameObject);
        }
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
