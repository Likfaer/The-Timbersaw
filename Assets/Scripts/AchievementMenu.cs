using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class AchievementMenu : MonoBehaviour
{
    //BASE SETUP FOR EACH SCENE
    //text visual info
    public Text MoneyText;
    public Text IncomeText;
    //main values
    public int money;
    public int total_money;
    public int tickmoney;
    //links to launch other scripts
    public Income IncomeLink;
    // list of buttons
    public string[] arrayTitles; //for achievments titles
    public Sprite[] arraySprites; //for achievments sprites
    public GameObject button; // Achievement button (for taking achievement)
    public GameObject content; // list of buttons
    private List<GameObject> list = new List<GameObject>();
    private VerticalLayoutGroup _group;
    void Start()
    {
        //setup main values
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");

        _group = GetComponent<VerticalLayoutGroup>();
        setAchievs();
        Zaika();

        //setup other scripts
        IncomeLink = GameObject.FindObjectOfType(typeof(Income)) as Income;
        IncomeLink.IdleFarm();
        StartCoroutine(CoinsUpdate());
    }
    void Zaika()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var item in buttons)
        {
            if (item.gameObject.name == "CrolikButton")
            {
                item.interactable = false;
                item.enabled = false;
            }
        }
    }
    private void RemovedList()
    {
        foreach (var elem in list)
        {
            Destroy(elem);
        }
        list.Clear();
    }

    void setAchievs()
    {
        RemovedList();
        if (arrayTitles.Length > 0)
        {
            var pr1 = Instantiate(button, transform); 
            var h = pr1.GetComponent<RectTransform>().rect.height; 
            var tr = GetComponent<RectTransform>(); 
            tr.sizeDelta = new Vector2(tr.rect.width, h * arrayTitles.Length);
            Destroy(pr1);
            for (var i = 0; i < arrayTitles.Length; i++)
            {
                var pr = Instantiate(button, transform);
                pr.GetComponentInChildren<Text>().text = arrayTitles[i]; // text of each component
                //pr.GetComponentsInChildren<Image>()[0].sprite = arraySprites[i]; // image of each component
                string line = "Ach" + i;
                //Debug.Log(PlayerPrefs.GetInt(line));
                if (PlayerPrefs.GetInt(line) == 1)
                {
                    pr.GetComponent<Button>().interactable = false;
                }
                var i1 = i;
                pr.GetComponent<Button>().onClick.AddListener(() => GetAchievement(i1));
                list.Add(pr);
            }
        }
    }
    bool IsTaken(int id)
    {
        string line = "Ach" + id;
        //Debug.Log(line + " " + PlayerPrefs.GetInt(line));
        if (PlayerPrefs.GetInt(line) == 0)
        {
            PlayerPrefs.SetInt(line, 1);
            return true;
        }
        else return false;  
    }

    void GetAchievement(int id)
    {
        string line = "Ach" + id;
        switch (id)
        {
            case 0:
                if (money > 100)
                {
                    if (IsTaken(id))
                    {
                        PlayerPrefs.SetInt(line, 1);
                        money += 10;
                        PlayerPrefs.SetInt("money", money);
                    }
                }
                break;
            case 1:
                if (money > 1000)
                {
                    if (IsTaken(id))
                    {
                        money += 100;
                        PlayerPrefs.SetInt(line, 1);
                        PlayerPrefs.SetInt("money", money);
                    }
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("Cralya") == 1)
                {
                    if (IsTaken(id))
                    {
                        money += 1000;
                        PlayerPrefs.SetInt(line, 1);
                        PlayerPrefs.SetInt("money", money);
                    }
                }
                break;
        }
    }
    public void Cralya()
    {
        PlayerPrefs.SetInt("Cralya", 1);
    }
    IEnumerator CoinsUpdate()
    {
        yield return new WaitForSeconds(0);
        money = IncomeLink.money;
        tickmoney = IncomeLink.tickmoney;
        StartCoroutine(CoinsUpdate());
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = money.ToString();
        IncomeText.text = tickmoney.ToString();
    }
}
