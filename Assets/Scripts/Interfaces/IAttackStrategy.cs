using UnityEngine;

public interface IAttackStrategy
{
    void ExecuteAttack(Transform attacker, IDamageable target);
}
