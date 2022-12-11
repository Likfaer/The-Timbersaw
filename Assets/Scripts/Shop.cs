using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;
using System;

public class Shop : MonoBehaviour
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
    public AudioSource audioSource;
    public Income IncomeLink;
    //values for items
    public double economicMultipler = 1.07;
    public int[] baseMass = { 100, 200, 500, 700, 1000 };
    public int[] multi = {1, 2, 5, 7, 10};
    public int[] intbuffs = new int[5];
    public int[] prises = new int[5];
    public string buffs; //buffs
    public string prise; //shop prises
    // list of buttons
    public string[] arrayTitles;
    public Sprite[] arraySprites;
    public GameObject button; 
    public GameObject content;
    private List<GameObject> list = new List<GameObject>();
    private VerticalLayoutGroup _group;
    void Start()
    {
        //setup main values
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");

        if (PlayerPrefs.GetString("buffs") == "0") buffs = "0,0,0,0,0";
        else buffs = PlayerPrefs.GetString("buffs");
        if (PlayerPrefs.GetString("prise") == "0") prise = "100,200,500,700,1000";
        else prise = PlayerPrefs.GetString("prise");
        prises = StringToArray(prise);
        intbuffs = StringToArray(buffs);
        _group = GetComponent<VerticalLayoutGroup>();
        setBuffs();
        //setup other scripts
        audioSource = GetComponent<AudioSource>();
        IncomeLink = GameObject.FindObjectOfType(typeof(Income)) as Income;
        IncomeLink.IdleFarm();
        StartCoroutine(CoinsUpdate());
    }
    int[] StringToArray(string temp_line)
    {
        int[] temp_arr_int = new int[5];
        string[] temp_arr_string = temp_line.Split(',');
        System.Console.WriteLine(temp_line);
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
            var pr1 = Instantiate(button, transform);
            var h = pr1.GetComponent<RectTransform>().rect.height;
            var tr = GetComponent<RectTransform>(); 
            tr.sizeDelta = new Vector2(tr.rect.width, h * arrayTitles.Length); 
            Destroy(pr1);

            for (var i = 0; i < arrayTitles.Length; i++)
            {
                var pr = Instantiate(button, transform);
                pr.GetComponentsInChildren<Text>()[0].text = System.Convert.ToString(arrayTitles[i]);
                pr.GetComponentsInChildren<Text>()[1].text = System.Convert.ToString(intbuffs[i]);
                pr.GetComponentsInChildren<Text>()[2].text = System.Convert.ToString(prises[i]);
                pr.GetComponentsInChildren<Text>()[3].text = System.Convert.ToString(intbuffs[i] * multi[i]);
                // pr.GetComponentInChildren<Image>().sprite = System.Convert.ToString(arraySprites[i]); // image of each component
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
        switch (id)
        {
            case 0:
                if (money >= (int)prises[id])
                {
                    IncomeLink.money -= (int)prises[id];
                    intbuffs[id]++;
                    prises[id] = (int)(baseMass[id] * Mathf.Pow((float)economicMultipler, intbuffs[id]));
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 1:
                if (money >= (int)prises[id])
                {
                    IncomeLink.money -= (int)prises[id];
                    intbuffs[id]++;
                    prises[id] = (int)(baseMass[id] * Mathf.Pow((float)economicMultipler, intbuffs[id]));
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 2:
                if (money >= (int)prises[id])
                {
                    IncomeLink.money -= (int)prises[id];
                    intbuffs[id]++;
                    prises[id] = (int)(baseMass[id] * Mathf.Pow((float)economicMultipler, intbuffs[id]));
                    PlayerPrefs.SetInt("money", money);
                } 
                break;
            case 3:
                if (money >= (int)prises[id])
                {
                    IncomeLink.money -= (int)prises[id];
                    intbuffs[id]++;
                    prises[id] = (int)(baseMass[id] * Mathf.Pow((float)economicMultipler, intbuffs[id]));
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 4:
                if (money >= (int)prises[id])
                {
                    IncomeLink.money -= (int)prises[id];
                    intbuffs[id]++;
                    prises[id] = (int)(baseMass[id] * Mathf.Pow((float)economicMultipler, intbuffs[id]));
                    PlayerPrefs.SetInt("money", money);
                }
                break;
        }
        string temp_collab = "";
        string tempString = "";
        for (int j = 0; j < intbuffs.Length; j++)
        {
            tempString += prises[j] + ",";
            temp_collab += intbuffs[j] + ",";
        }
        temp_collab = temp_collab.Remove(temp_collab.Length - 1, 1);
        buffs = temp_collab;
        PlayerPrefs.SetString("buffs", buffs);
        tempString = tempString.Remove(tempString.Length - 1, 1);
        prise = PlayerPrefs.GetString("prise");
        PlayerPrefs.SetString("prise",tempString);
    }
    IEnumerator CoinsUpdate()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        money = IncomeLink.money;
        tickmoney = IncomeLink.tickmoney;
        StartCoroutine(CoinsUpdate());
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

    public void GameReset()
    {
        PlayerPrefs.SetInt("money", 0);
        PlayerPrefs.SetInt("tickmoney", 0);
        PlayerPrefs.SetInt("total_money", 0);
        PlayerPrefs.SetString("buffs","0,0,0,0,0");
        PlayerPrefs.SetString("prise", "100,200,500,700,1000");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
