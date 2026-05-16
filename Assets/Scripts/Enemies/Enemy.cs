using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableObject attackStrategyObject;
    private IAttackStrategy currentStrategy;
    
    private IDamageable currentTarget;

    private void Awake()
    {
        currentStrategy = attackStrategyObject as IAttackStrategy;
        
        if (currentStrategy == null)
        {
            Debug.LogError($"{gameObject.name} there is not any valid attackStrategy on gameObject!");
        }
    }

    public void SetTarget(IDamageable target)
    {
        currentTarget = target;
    }

    public void PerformAttack()
    {
        if (currentStrategy != null && currentTarget != null)
        {
            currentStrategy.ExecuteAttack(transform, currentTarget);
        }
    }

    public void ChangeStrategy(IAttackStrategy newStrategy)
    {
        currentStrategy = newStrategy;
    }
}
