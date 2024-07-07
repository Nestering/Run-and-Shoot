using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("All prefabs")]
    [SerializeField] private GameObject[] PrefabsEnemy;
    [SerializeField] private GameObject[] PrefabsObstacle;

    [Space()]

    [Header("Setting filling platform")]
    [SerializeField] private Transform[] ListSpawnPoint;
    [SerializeField] private Transform[] UnoccupiedPoints;
    [SerializeField] private List<Enemy> ListEnemy = new List<Enemy>();
    [SerializeField] private List<Transform> MoveToPoint;

    public bool StartPoint;
    private void Start()
    {   
        StartSpawnEnemy();
        GameController.Singleton.FillMoveToPoint(MoveToPoint);
    }
    public void StartSpawnEnemy()
    {
        UnoccupiedPoints = ListSpawnPoint;
        SpwanEnemy(0);//обязательный спавн одного противника.
        for (int i = 1; i < ListSpawnPoint.Length; i++)
        {
            bool random = new System.Random().Next(0, 2) == 0;
            if (random)
            {
                SpwanEnemy(i);
            }
        }
        GameController.Singleton.FillListEnemy(ListEnemy);
        SpawnObstacle();
    }
    void SpwanEnemy(int numberPoint)
    {
        int enemyIndex = GetIndexEnemy();
        GameObject enemy  = Instantiate(PrefabsEnemy[enemyIndex], ListSpawnPoint[numberPoint].position, Quaternion.identity, transform);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        UnoccupiedPoints[numberPoint] = null;
        ListEnemy.Add(enemyScript);

    }
    int GetIndexEnemy()// Логика получения номера врага.
    {
        int enemyIndex = Random.Range(0, PrefabsEnemy.Length);
        return enemyIndex;
    }
    public void SpawnObstacle()
    {
        for (int i = 0; i < UnoccupiedPoints.Length; i++)
        {
            if (UnoccupiedPoints[i] != null)
            {
                int obstacleIndex = Random.Range(0, PrefabsObstacle.Length);
                GameObject Obstacle = Instantiate(PrefabsObstacle[obstacleIndex], ListSpawnPoint[i].position, Quaternion.identity, transform);
                ListSpawnPoint[i] = null;
            }
        }
    }
}

