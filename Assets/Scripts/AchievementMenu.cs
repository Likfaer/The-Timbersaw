using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class AchievementMenu : MonoBehaviour
{
    public int money;
    public int total_money;

    public string[] arrayTitles; //for achievments titles
    public Sprite[] arraySprites; //for achievments sprites
    public GameObject button; // Achievement button (for taking achievement)
    public GameObject content; // list of buttons
    private List<GameObject> list = new List<GameObject>();
    private VerticalLayoutGroup _group;
    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        _group = GetComponent<VerticalLayoutGroup>();
        setAchievs();
        Zaika();
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
            //create sample for achievment
            var pr1 = Instantiate(button, transform); // sample
            var h = pr1.GetComponent<RectTransform>().rect.height; // height
            var tr = GetComponent<RectTransform>(); //features of component RectTransporm
            tr.sizeDelta = new Vector2(tr.rect.width, h * arrayTitles.Length); // size of features
            Destroy(pr1);
            for (var i = 0; i < arrayTitles.Length; i++)
            {
                var pr = Instantiate(button, transform);
                pr.GetComponentInChildren<Text>().text = arrayTitles[i]; // text of each component
                pr.GetComponentsInChildren<Image>()[0].sprite = arraySprites[i]; // image of each component
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
                        PlayerPrefs.SetInt("isFirst", 1);
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
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
