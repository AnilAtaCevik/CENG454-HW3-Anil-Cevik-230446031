using UnityEngine;

public class PlayerDefender : MonoBehaviour
{
    [SerializeField] private ProjectilePoolManager poolManager;
    [SerializeField] private Transform firePoint;
    
    private IWeapon currentWeapon;

    private void Start()
    {
        currentWeapon = new BasicWeapon(poolManager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentWeapon.Fire(firePoint.position, firePoint.rotation);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeWeapon();
        }
    }

    private void UpgradeWeapon()
    {
        currentWeapon = new MultiShotDecorator(currentWeapon);
        Debug.Log("Weapon Upgraded: MultiShot!");
    }
}
