using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] int money;
    public Text MoneyText;

    public void ButtonClick()
    {
        money++;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = money.ToString();
    }
}
