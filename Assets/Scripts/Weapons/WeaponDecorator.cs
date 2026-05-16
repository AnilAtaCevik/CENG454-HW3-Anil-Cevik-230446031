using UnityEngine;

public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon decoratedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        this.decoratedWeapon = weapon;
    }

    public virtual void Fire(Vector3 position, Quaternion rotation)
    {
        decoratedWeapon.Fire(position, rotation);
    }
}