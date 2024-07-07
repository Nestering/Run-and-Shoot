using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Singleton;
    [SerializeField] private Image levelBar;
    [SerializeField] private Text levelNumber;
    [SerializeField] private Text intervalShoot;
    [SerializeField] private Text damageBullet;
    [SerializeField] private GameObject panelWin;
    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        FillLevelBar();
        FillExplorerWeapon();
    }
    public void FillLevelBar()
    {
        levelBar.fillAmount = (float)GameController.Singleton.numberPlatform / (float)(GameController.Singleton.CountPlatform - 1);
        levelNumber.text = GameController.Singleton.level.ToString();
    }
    public void FillExplorerWeapon()
    {
        damageBullet.text = Player.Singleton.weapon.damageBullet.ToString();
        intervalShoot.text = Player.Singleton.weapon.intervalShoot.ToString();
    }
    public void EnabledPanelWin()
    {
        panelWin.SetActive(true);
    }
}
