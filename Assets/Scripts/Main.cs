using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    //BASE SETUP FOR EACH SCENE
    //text visual info
    public Text MoneyText;
    public Text IncomeText;
    public int money;
    public int total_money;
    public int tickmoney;
    //links to launch other scripts
    public AudioSource audioSource;
    public Income IncomeLink;
    private void Start()
    {
        //setup main values
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");
        //setup other scripts
        audioSource = GetComponent<AudioSource>();
        IncomeLink = GameObject.FindObjectOfType(typeof(Income)) as Income;
        IncomeLink.IdleFarm();
    }

    public void ButtonClickVisual()
    {
        money++;
        total_money++;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("total_money", total_money);
        audioSource.Play();
    }

    public void ToAchievements()
    {
        SceneManager.LoadScene(1);
    }

    public void ToShop()
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        money = IncomeLink.money;
        tickmoney = IncomeLink.tickmoney;
        MoneyText.text = money.ToString();
        IncomeText.text = tickmoney.ToString();
    }
}
