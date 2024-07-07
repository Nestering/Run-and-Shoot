using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectGenerator : MonoBehaviour
{
    public static ObjectGenerator Singleton;

    [Header("Platform Data")]
    [SerializeField] private Transform areaBuild;
    [SerializeField] private GameObject prefabEndPlatform;
    [SerializeField] private GameObject[] prefab; // Префаб объекта, который будет создаваться 
    [SerializeField] private List<GameObject> objects = new List<GameObject>();

    [Space()]

    [Header("Setting Platform")]
    [SerializeField] private float distanceBetweenObjects = 20f; // Расстояние между объектами 
    [SerializeField] private float rotationStep = 30f; // Шаг поворота 
    
    private void Awake()
    {
        Singleton = this;
    }
    void Start()
    {
        
        StartCoroutine(GenerateNextObjects());
    }
    IEnumerator GenerateNextObjects()
    {

        while (objects.Count < GameController.Singleton.CountPlatform)
        {
            GameObject lastObject = objects[objects.Count - 1];
            Vector3 nextPosition = lastObject.transform.position + (lastObject.transform.forward * distanceBetweenObjects);
            Quaternion nextRotation = RandomRotationAroundYAxis();
            GameObject newObject = Instantiate(prefab[0], nextPosition, nextRotation, areaBuild);
            newObject.name = "Platform - " + (objects.Count + 1);
            objects.Add(newObject);

            yield return new WaitForSeconds(0f);
        }
        if(objects.Count == GameController.Singleton.CountPlatform)
        {
            GameObject lastObject = objects[objects.Count-1];
            Vector3 nextPosition = lastObject.transform.position + (lastObject.transform.forward * distanceBetweenObjects);
            Quaternion nextRotation = RandomRotationAroundYAxis();
            GameObject newObject = Instantiate(prefabEndPlatform, nextPosition, nextRotation, areaBuild);
            newObject.name = "End Platform";
        }
        GameController.Singleton.ListPlatforms = objects;
    }
    Quaternion RandomRotationAroundYAxis()
    {
        float randomAngle = Random.Range(-(int)rotationStep / 30, (int)rotationStep/30 +1) * 30;
        
        return Quaternion.Euler(0f, randomAngle, 0f);
    }
}