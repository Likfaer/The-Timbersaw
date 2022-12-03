using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class Shop : MonoBehaviour
{
    public Text MoneyText;
    public Text IncomeText;
    public int money;
    public int total_money;
    public int tickmoney;

    public int[] multiplayer = new int[5] {1, 2, 5, 7, 10}; // miltiplayer buffs
    public int[] intbuffs = new int[5]; 
    public string buffs; //buffs
    public int[] prises = { 100, 200, 500, 700, 1000 }; //shop prises
    

    public string[] arrayTitles; //for achievments titles
    public Sprite[] arraySprites; //for achievments sprites
    public GameObject button; // Achievement button (for taking achievement)
    public GameObject content; // list of buttons\

    private List<GameObject> list = new List<GameObject>();
    private VerticalLayoutGroup _group;
    void Start()
    {
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");
        if (PlayerPrefs.GetString("buffs") == "0") buffs = "0,0,0,0,0";
        else buffs = PlayerPrefs.GetString("buffs");
        intbuffs = StringToArray(buffs);

        _group = GetComponent<VerticalLayoutGroup>();
        setBuffs();
        StartCoroutine(IdleFarm());
    }
    int[] StringToArray(string teamp_line)
    {
        int[] temp_arr_int = new int[5];
        string[] temp_arr_string = teamp_line.Split(',');
        for (int i = 0; i < temp_arr_string.Length; i++)
        {
            temp_arr_int[i] = System.Int32.Parse(temp_arr_string[i].ToString());
        }
        return temp_arr_int;
    }

    void setBuffs()
    {
        RectTransform rectT = content.GetComponent<RectTransform>();
        rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
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
                pr.GetComponentInChildren<Text>().text = arrayTitles[i] + " " + intbuffs[i]; // text of each component
                pr.GetComponentInChildren<Image>().sprite = arraySprites[i]; // image of each component
                var i1 = i;
                if (!Prises(i)) 
                {
                    pr.GetComponent<Button>().interactable = false;
                    pr.GetComponent<Image>().color = Color.gray;
                } 
                pr.GetComponent<Button>().onClick.AddListener(() => GetBuff(i1));
                list.Add(pr);
            }
        }
    }
    bool Prises(int id)
    {
        if (prises[id] < money) return true;
        else return false;
    }
    void GetBuff(int id)
    {
        string temp_collab = "";
        switch (id)
        {
            case 0:
                if (money >= prises[id])
                {
                    money -= prises[id];
                    intbuffs[id]++;
                    PlayerPrefs.SetInt("money", money);  
                }
                break;
            case 1:
                if (money >= prises[id])
                {
                    money -= prises[id];
                    intbuffs[id]++;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 2:
                if (money >= prises[id])
                {
                    money -= prises[id];
                    intbuffs[id]++;
                    PlayerPrefs.SetInt("money", money);
                } 
                break;
            case 3:
                if (money >= prises[id])
                {
                    money -= prises[id];
                    intbuffs[id]++;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 4:
                if (money >= prises[id])
                {
                    money -= prises[id];
                    intbuffs[id]++;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
        }
        for(int j = 0; j < intbuffs.Length; j++)
        {
            temp_collab = temp_collab.Insert(temp_collab.Length, intbuffs[j].ToString());
            temp_collab += ",";
        }
        temp_collab = temp_collab.Remove(temp_collab.Length - 1, 1);
        buffs = temp_collab;
        PlayerPrefs.SetString("buffs", buffs);
    }
    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        for (int i = 0; i < intbuffs.Length; i++)
        {
            Debug.Log(intbuffs[i] + " * " + multiplayer[i]);
            money += intbuffs[i] * multiplayer[i];
        }
        tickmoney = money - PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("tickmoney", tickmoney);
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(IdleFarm());
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = money.ToString();
        IncomeText.text = tickmoney.ToString();
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
