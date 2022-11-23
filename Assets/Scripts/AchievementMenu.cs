using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMeny : MonoBehaviour
{
    public int total_money;
    [SerializeField] Button firstAch;
    [SerializeField] bool isFirst;
    // Start is called before the first frame update
    void Start()
    {
        total_money = PlayerPrefs.GetInt("total_money");
        if (total_money >= 10 && !isFirst)
        {
            firstAch.interactable = true;
        }
        else
        {
            firstAch.interactable = FALSE;
        }
    }
    public void GetFirst()
    {
        int money = PlayerPrefs.GetInt("money");
        money += 10;
        PlayerPrefs.SetInt("money", money);
        isFirst = true;
        PlayerPrefs.SetInt("isFirst", isFirst ? 1 : 0);
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
