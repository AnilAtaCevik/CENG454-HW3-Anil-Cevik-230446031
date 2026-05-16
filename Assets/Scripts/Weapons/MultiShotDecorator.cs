using UnityEngine;

public class MultiShotDecorator : WeaponDecorator
{
    private float spreadAngle;

    public MultiShotDecorator(IWeapon weapon, float spreadAngle = 15f) : base(weapon)
    {
        this.spreadAngle = spreadAngle;
    }

    public override void Fire(Vector3 position, Quaternion rotation)
    {
        base.Fire(position, rotation);

        Quaternion rightRotation = rotation * Quaternion.Euler(0, spreadAngle, 0);
        Quaternion leftRotation = rotation * Quaternion.Euler(0, -spreadAngle, 0);

        base.Fire(position, rightRotation);
        base.Fire(position, leftRotation);
    }
}