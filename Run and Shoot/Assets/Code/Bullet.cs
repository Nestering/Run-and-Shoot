using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setting Bullet")]
    private Vector3 direction;
    private float speed;
    private float lifeTime;
    private int damage = 10;
    private Rigidbody rb;
    [SerializeField] private GameObject effectPrefabs;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void FillParametrs(float newspeed,float newLifeTime, int newDamage, Vector3 newDirection)
    {
        speed = newspeed;
        lifeTime = newLifeTime;
        damage = newDamage;
        direction = newDirection;
        rb.velocity = direction * speed;
    }
    void Update()
    {
        CheckHealthTimer();
    }
    private void CheckHealthTimer()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            ReturnPoolObject();
            lifeTime = 2f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            lifeTime = 2f;
            InstanceEffect(1f);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            InstanceEffect(2f);
            other.gameObject.SetActive(false);
        }
        ReturnPoolObject();
    }
    void ReturnPoolObject()
    {
        Player.Singleton.weapon.poolObjects.Add(this);
        gameObject.SetActive(false);
    }
    void InstanceEffect(float scaleEffect)
    {
        GameObject effect = Instantiate(effectPrefabs, transform.position, Quaternion.identity);
        effect.transform.localScale *= scaleEffect;
    }
}
