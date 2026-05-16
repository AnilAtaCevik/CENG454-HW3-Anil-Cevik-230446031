using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePoolManager : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private ObjectPool<Projectile> pool;

    private void Awake()
    {
        pool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.SetPool(pool);
        return projectile;
    }

    private void OnTakeFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.OnSpawn();
    }

    private void OnReturnedToPool(Projectile projectile)
    {
        projectile.OnDespawn();
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public Projectile GetProjectile(Vector3 position, Quaternion rotation)
    {
        Projectile p = pool.Get();
        p.transform.position = position;
        p.transform.rotation = rotation;
        return p;
    }
}