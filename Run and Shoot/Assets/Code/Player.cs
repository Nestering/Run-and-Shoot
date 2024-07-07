using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Singleton;

    [Header("Setting Player")]
    public bool isMove = false;
    public bool isShoot = false;
    public float speedPlayer = 2.5f;

    [Space()]

    [Header("Links")]
    public GameObject character;
    public Weapon weapon;
    private void Awake()
    {
        Singleton = this;
    }
    public IEnumerator Move(List<Transform> moveToPoints)
    {
        
        for (int i = 0; i < moveToPoints.Count; i++)
        {
            while (transform.position != moveToPoints[i].position)
            {
                Rotate(moveToPoints, i);
                transform.position = Vector3.MoveTowards(transform.position, moveToPoints[i].position, speedPlayer * Time.deltaTime);
                yield return new WaitForSeconds(0f);
                isMove = true;
            }
            yield return new WaitForSeconds(0f);
        }
        isMove = false;
    }
    void Rotate(List<Transform> moveToPoints, int numberPoint)
    {
        Vector3 direction = moveToPoints[numberPoint].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        character.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    }
    public void TurnTowardsTheBullet(Vector3 moveToPoint)
    {
        isShoot = true;
        StopAllCoroutines();
        StartCoroutine(TowardsTheBullet(moveToPoint));
    }
    IEnumerator TowardsTheBullet(Vector3 moveToPoint)
    {

        Vector3 direction = moveToPoint - character.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        while (character.transform.rotation != rotation)
        {
            Quaternion newrotation = Quaternion.Slerp(character.transform.rotation, rotation, 8f * Time.deltaTime);
            Quaternion addRotation = Quaternion.Euler(0, 10, 0);
            character.transform.rotation = Quaternion.Euler(0, newrotation.eulerAngles.y + addRotation.y, 0);
            yield return new WaitForSeconds(0f);
            if (isMove)
            {
                break;
            }
        }
        
    }
}
