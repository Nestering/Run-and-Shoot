using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Initializing Bullets")]
    [SerializeField] Transform areaBullet;
    public List<Bullet> poolObjects = new List<Bullet>();
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public int maxBullets = 10;

    [Space()]

    [Header("Setting Bullet")]
    public float speedBullet = 10f;
    public float lifeTimeBullet = 10f;
    public int damageBullet = 10;

    [Space()]

    [Header("Setting Weapon")]
    private float timeShoot;
    public float intervalShoot;

    private void Start()
    {
        InitializingBullets();
    }
    private void Update()
    {
        Shoot();
        timeShoot -= Time.deltaTime;
    }
    private void InitializingBullets()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, areaBullet).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            poolObjects.Add(bullet);
        }
    }
    private void Shoot()
    {
        // Проверка нажатия и создание пули
        if (Input.GetMouseButtonDown(0) && timeShoot <= 0 && GameController.Singleton.isPlay && !Player.Singleton.isMove)
        {
            Vector3 direction;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Пытаемся найти столкновение луча с объектом
            if (Physics.Raycast(ray, out hit))
            {
                // Сохраняем позицию, куда мы кликнули
                Vector3 clickPosition = ray.GetPoint(hit.distance);

                // Узнаем направление движения пули
                direction = clickPosition - shootPoint.position;
                direction.Normalize();

                // Выстрел
                SpawnBullet(direction);
                Player.Singleton.TurnTowardsTheBullet(clickPosition);
                timeShoot = intervalShoot;
            }
        }
    }
    public void SpawnBullet(Vector3 direction)
    {
        Bullet bullet = poolObjects[0];
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootPoint.position;
        bullet.FillParametrs(speedBullet, lifeTimeBullet, damageBullet, direction);
        poolObjects.RemoveAt(0);
    }
}

