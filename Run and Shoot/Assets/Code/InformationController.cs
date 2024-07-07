using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    public static InformationController singleton;
    public DataHandler dataHandler;
    private void Awake()
    {
        singleton = this;
    }
    public void LoadData()
    {
        dataHandler.LoadData();
        GameController.Singleton.level = dataHandler.serializedScript.level;
        Player.Singleton.weapon.speedBullet = dataHandler.serializedScript.bulletSpeed;
        Player.Singleton.weapon.intervalShoot = dataHandler.serializedScript.bulletInterval;
        Player.Singleton.speedPlayer = dataHandler.serializedScript.playerSpeed;
        Player.Singleton.weapon.damageBullet = dataHandler.serializedScript.damagePlayer;
    }
    public void SaveData()
    {
        dataHandler.serializedScript.level = GameController.Singleton.level;
        dataHandler.serializedScript.bulletSpeed = Player.Singleton.weapon.speedBullet;
        dataHandler.serializedScript.bulletInterval = Player.Singleton.weapon.intervalShoot;
        dataHandler.serializedScript.playerSpeed = Player.Singleton.speedPlayer;
        dataHandler.serializedScript.damagePlayer = Player.Singleton.weapon.damageBullet;
        print(dataHandler.serializedScript.level);
        dataHandler.SaveData();

    }
}
