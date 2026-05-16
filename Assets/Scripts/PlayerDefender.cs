using UnityEngine;

public class PlayerDefender : MonoBehaviour
{
    [SerializeField] private ProjectilePoolManager poolManager;
    [SerializeField] private Transform firePoint;
    
    private IWeapon currentWeapon;
    private Camera mainCamera;

    private void Start()
    {
        currentWeapon = new BasicWeapon(poolManager);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AimTowardsMouse();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            currentWeapon.Fire(firePoint.position, firePoint.rotation);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeWeapon();
        }
    }

    private void AimTowardsMouse()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float hitDistance;

        if (groundPlane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            
            targetPoint.y = transform.position.y;

            transform.LookAt(targetPoint);
        }
    }

    private void UpgradeWeapon()
    {
        currentWeapon = new MultiShotDecorator(currentWeapon);
        Debug.Log("Weapon Upgraded: MultiShot!");
    }
}
