using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damage = 10f;
    
    private Rigidbody rb;
    private float currentLifeTime;
    
    private IObjectPool<Projectile> pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetPool(IObjectPool<Projectile> pool)
    {
        this.pool = pool;
    }

    public void OnSpawn()
    {
        currentLifeTime = lifeTime;
        
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void OnDespawn()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0)
        {
            ReleaseToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        ReleaseToPool();
    }

    private void ReleaseToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}