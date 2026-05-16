using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeStrategy", menuName = "Strategies/Melee Attack")]
public class MeleeAttackStrategy : ScriptableObject, IAttackStrategy
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float knockbackForce = 5f; 

    public void ExecuteAttack(Transform attacker, IDamageable target)
    {
        Debug.Log($"{attacker.name} used melee attack! Damage: {damage}, Knockback Force: {knockbackForce}");
        target.TakeDamage(damage);
    }
}