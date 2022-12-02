using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public int money;
    public int total_money;
    public int multiplayer; // miltiplayer buffs
    public int buffs; //buffs
    public int[] prises = { 100, 200, 500, 700, 1000 }; //shop prises
    public Text MoneyText;

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
        multiplayer = PlayerPrefs.GetInt("multiplayer");
        buffs = PlayerPrefs.GetInt("buffs");

        _group = GetComponent<VerticalLayoutGroup>();
        setBuffs();
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
                pr.GetComponentInChildren<Text>().text = arrayTitles[i]; // text of each component
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

        switch (id)
        {
            case 0:
                if (money >= 100)
                {
                    money -= 100;
                    buffs++;
                    PlayerPrefs.SetInt("money", money);  
                }
                else
                {

                }
                break;
            case 1:
                if (money >= 200)
                {
                    money -= 200;
                    buffs += 2;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 2:
                if (money >= 500)
                {
                    money -= 500;
                    buffs += 5;
                    PlayerPrefs.SetInt("money", money);
                } 
                break;
            case 3:
                if (money >= 700)
                {
                    money -= 700;
                    buffs += 7;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
            case 4:
                if (money >= 1000)
                {
                    money -= 1000;
                    buffs += 10;
                    PlayerPrefs.SetInt("money", money);
                }
                break;
        }
    }
    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        money += buffs * multiplayer;
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(IdleFarm());
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = money.ToString();
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
