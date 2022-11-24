using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementMenu : MonoBehaviour
{
    public int money;
    public int total_money;
    [SerializeField] Button firstAch;
    [SerializeField] bool isFirst;
    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        isFirst = PlayerPrefs.GetInt("isFirst") == 1 ? true : false;
        if (total_money >= 10 && !isFirst)
        {
            firstAch.interactable = true;
        }
        else
        {
            firstAch.interactable = false;
            StartCoroutine(IdleFarm());
        }
    }
    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        money++;
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(IdleFarm());
    }
    public void GetFirst()
    {
       
        if (!isFirst)
        {
            int money = PlayerPrefs.GetInt("money");
            money += 10;
            PlayerPrefs.SetInt("money", money);
            isFirst = true;
            PlayerPrefs.SetInt("isFirst", isFirst ? 1 : 0);

        }
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
