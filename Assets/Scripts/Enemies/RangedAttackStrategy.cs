using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedStrategy", menuName = "Strategies/Ranged Attack")]
public class RangedAttackStrategy : ScriptableObject, IAttackStrategy
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject projectilePrefab; 

    public void ExecuteAttack(Transform attacker, IDamageable target)
    {
        Debug.Log($"{attacker.name} used ranged attack! Damage: {damage}");
    }
}