using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableObject attackStrategyObject;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    private IAttackStrategy currentStrategy;
    private IDamageable currentTarget;
    private Transform targetTransform;
    private float lastAttackTime;

    private void Awake()
    {
        currentStrategy = attackStrategyObject as IAttackStrategy;
        
        if (currentStrategy == null)
        {
            Debug.LogError($"{gameObject.name} there is not any IAttackStrategy!");
        }
    }

    public void SetTarget(Transform targetT, IDamageable targetD)
    {
        targetTransform = targetT;
        currentTarget = targetD;
    }

    private void Update()
    {
        if (targetTransform == null || currentTarget == null) return;

        float distance = Vector3.Distance(transform.position, targetTransform.position);

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
            transform.LookAt(targetTransform);
        }
        else
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                PerformAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    public void PerformAttack()
    {
        if (currentStrategy != null && currentTarget != null)
        {
            currentStrategy.ExecuteAttack(transform, currentTarget);
        }
    }
}