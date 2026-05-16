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
    private bool isReleased; 

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
        isReleased = false; 
        
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void OnDespawn() { }

    private void Update()
    {
        if (isReleased) return;

        float moveDistance = speed * Time.deltaTime;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.3f, transform.forward, moveDistance);

        foreach (RaycastHit hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                ReleaseToPool();
                return; 
            }
        }

        transform.Translate(Vector3.forward * moveDistance);

        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0)
        {
            ReleaseToPool();
        }
    }

    private void ReleaseToPool()
    {
        if (isReleased) return; 
        isReleased = true;

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