using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public Text MoneyText;
    public Text IncomeText;
    [SerializeField] int money;
    public int total_money;
    public int tickmoney;

    public int multiplayer; // miltiplayer buffs
    public int buffs; //buffs
    public AudioSource audioSource;

    public Income m_someOtherScriptOnAnotherGameObject;
    private void Start()
    {
        m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(Income)) as Income;
        audioSource = GetComponent<AudioSource>();
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");
        buffs = PlayerPrefs.GetInt("buffs");

        m_someOtherScriptOnAnotherGameObject.IdleFarm();
        StartCoroutine(CoinsUpdate());
    }

    public void ButtonClick()
    {
        money++;
        total_money++;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("total_money", total_money);
        audioSource.Play();
    }
    IEnumerator CoinsUpdate()
    {
        yield return new WaitForSeconds(1);
        money = m_someOtherScriptOnAnotherGameObject.money;
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(CoinsUpdate());
    }

    public void ToAchievements()
    {
        SceneManager.LoadScene(1);
    }

    public void ToShop()
    {
        PlayerPrefs.SetInt("total_money", total_money);
        PlayerPrefs.SetInt("money", money);
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = money.ToString();
        IncomeText.text = tickmoney.ToString();
    }
}
