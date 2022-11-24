using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementMenu : MonoBehaviour
{
    public int total_money;
    //[SerializeField] Button firstAch;
    [SerializeField] bool isFirst;

    public string[] arrayTitles; //for achievments titles
    public Sprite[] arraySprites; //for achievments sprites
    public GameObject button; //achieve button
    public GameObject content;

    private List<GameObject> list = new List<GameObject>();
    private VerticalLayoutGroup _group;
    // Start is called before the first frame update
    void Start()
    {
        total_money = PlayerPrefs.GetInt("total_money");
        isFirst = PlayerPrefs.GetInt("isFirst") == 1 ? true : false;
        
        RectTransform rectT = content.GetComponent<RectTransform>();
        rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        _group = GetComponent<VerticalLayoutGroup>();
        setAchievs();//*

        /*if(isFirst)
        {
            StartCoroutine(IdleFarm());
        }*/
        

        /*if (total_money >= 10 && !isFirst)
        {
            firstAch.interactable = true;
        }
        else
        {
            firstAch.interactable = false;
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
        }
    }
    public void GetFirst()
=======
            StartCoroutine(IdleFarm());
        }*/
    }

    private void RemovedList()
    {
        foreach(var elem in list)
        {
            Destroy(elem);
>>>>>>> Stashed changes
        }
        list.Clear();
    }
<<<<<<< Updated upstream
    public void GetFirst()
=======

    void setAchievs()
    {
        
        RectTransform rectT = content.GetComponent<RectTransform>();
        rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        RemovedList();
        if (arrayTitles.Length > 0)
        {
            //create sample for achievment
            var pr1 = Instantiate(button, transform); // sample
            var h = pr1.GetComponent<RectTransform>().rect.height; // height
            var tr = GetComponent<RectTransform>(); //feature component RectTransporm
            tr.sizeDelta = new Vector2(tr.rect.width, h * arrayTitles.Length); // size feateru component object
            Destroy(pr1);

            for (var i = 0; i < arrayTitles.Length; i++)
            {
                var pr = Instantiate(button, transform);
                pr.GetComponentInChildren<Text>().text = arrayTitles[i];//*
                pr.GetComponentInChildren<Image>().sprite = arraySprites[i];
                var i1 = i;
                pr.GetComponent<Button>().onClick.AddListener(() => PlayerPrefs.SetInt("money", money += 10));
                list.Add(pr);
            }
        }
    }

    /*void GetAchievment(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log(id);
                break;
            case 1:
                Debug.Log(id);
                money += 10;
                PlayerPrefs.SetInt("money", money);
                break;
        }
    }*/

    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        money++;
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(IdleFarm());
    }
    /*public void GetFirst()
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    {
        if (!isFirst)
        {
            int money = PlayerPrefs.GetInt("money");
            money += 10;
            PlayerPrefs.SetInt("money", money);
            isFirst = true;
            PlayerPrefs.SetInt("isFirst", isFirst ? 1 : 0);
        }
    }*/
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
