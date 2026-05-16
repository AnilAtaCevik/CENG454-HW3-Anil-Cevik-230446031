using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable 
{
    [SerializeField] private ScriptableObject attackStrategyObject;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    public float MaxHealth { get; private set; } = 30f;
    public float CurrentHealth { get; private set; }

    private IAttackStrategy currentStrategy;
    private IDamageable currentTarget;
    private Transform targetTransform;
    private float lastAttackTime;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        currentStrategy = attackStrategyObject as IAttackStrategy;
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

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} Killed");
        Destroy(gameObject); 
    }
}