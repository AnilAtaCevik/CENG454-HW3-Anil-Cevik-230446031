using UnityEngine;

public class BasicWeapon : IWeapon
{
    private ProjectilePoolManager poolManager;

    public BasicWeapon(ProjectilePoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public void Fire(Vector3 position, Quaternion rotation)
    {
        if (poolManager != null)
        {
            poolManager.GetProjectile(position, rotation);
        }
    }
}