using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;

    [Header("Player movement settings")]
    public List<GameObject> ListPlatforms = new List<GameObject>();
    public List<Transform>[] ListMoveToPoint;
    public List<Transform> StartListMoveToPoint;
    public List<Enemy>[] ListEnemyLevel;
    private Enemy[] ThisListEnemyLevel;

    [Space()]

    [Header("Setting Game")]
    public bool isPlay = false;
    public int level = 1;
    public int CountPlatform;
    public int numberPlatform;
    bool start = true;

    private void Awake()
    {
        Singleton = this;
    }
    public void DeleteKey()
    {
        PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("OneStart", 0) == 1)
        {   
            InformationController.singleton.LoadData();
        }
        else
        {
            PlayerPrefs.SetInt("OneStart", 1);
        }
        CountPlatform = level + 5;
        ListEnemyLevel = new List<Enemy>[CountPlatform];
        ListMoveToPoint = new List<Transform>[CountPlatform];
        UI.Singleton.FillLevelBar();
        UI.Singleton.FillExplorerWeapon();
    }

    public void FillListEnemy(List<Enemy> ListEnemy)
    {
        for (int i = 0; i < ListEnemyLevel.Length; i++)
        {
            if (ListEnemyLevel[i] == null)
            {
                ListEnemyLevel[i] = ListEnemy;
                return;
            }
        }
    }
    public void RemoveListEnemy(Enemy enemy)
    {
        var enemiesToRemove = ListEnemyLevel[numberPlatform].ToList();
        enemiesToRemove.Remove(enemy);
        ListEnemyLevel[numberPlatform] = enemiesToRemove.ToList();
    }
    public void FillMoveToPoint(List<Transform> ListPoint)
    {
        for (int i = 0; i < ListMoveToPoint.Length; i++)
        {
            if (ListMoveToPoint[i] == null)
            {
                ListMoveToPoint[i] = ListPoint;
                return;
            }
        }
    }
    public void StartMovePlayer()
    {
        isPlay = true;
        StartCoroutine(CheckEnemyPlatform());
    }
    public IEnumerator CheckEnemyPlatform()
    {
        MovePlayer();
        while (numberPlatform < CountPlatform-1)
        {
            ThisListEnemyLevel = ListEnemyLevel[numberPlatform].ToArray();
            // Проверяем, есть ли враги в списке
            if (!ThisListEnemyLevel.Any())
            {
                if (!Player.Singleton.isMove)
                {
                    MovePlayer();
                    numberPlatform++;
                    UI.Singleton.FillLevelBar();
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        if(numberPlatform == CountPlatform - 1)
        {
            isPlay = false;
            UI.Singleton.EnabledPanelWin();
        }
    }
    public void MovePlayer()
    {
        if (start)
        {
            StartCoroutine(Player.Singleton.Move(StartListMoveToPoint));
            start = false;
        }
        else
        {
            StartCoroutine(Player.Singleton.Move(ListMoveToPoint[numberPlatform]));
        }
        
    }
    public void ReloadGame()
    {
        level++;
        print(level);
        InformationController.singleton.SaveData();
        SceneManager.LoadScene(0);
    }


}
