using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Income : MonoBehaviour
{
    //main values
    public int money;
    public int total_money;
    public int tickmoney;
    //values for items
    public int[] multi;
    public int[] intbuffs = new int[12];
    public string buffs;
    //links to launch other scripts
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void Start()
    {
        //setup main values
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        tickmoney = PlayerPrefs.GetInt("tickmoney");
        if (PlayerPrefs.GetString("buffs") == null) buffs = "0,0,0,0,0,0,0,0,0,0,0,0";
        else buffs = PlayerPrefs.GetString("buffs");
        intbuffs = StringToArray(buffs);
        StartCoroutine(IdleFarm());
    }
    int[] StringToArray(string temp_line)
    {
        int[] temp_arr_int = new int[12];
        string[] temp_arr_string = temp_line.Split(',');
        System.Console.WriteLine(temp_line);
        for (int i = 0; i < temp_arr_string.Length; i++)
        {
            temp_arr_int[i] = System.Int32.Parse(temp_arr_string[i].ToString());
        }
        return temp_arr_int;
    }
    public void ButtonClickReal()
    {
        money++;
        total_money++;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("total_money", total_money);
    }
    // Update is called once per frame
    void Update()
    {
        buffs = PlayerPrefs.GetString("buffs");
        intbuffs = StringToArray(buffs);
    }
    public int GetMoney()
    {
        return this.money;
    }
    public IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        tickmoney = 0;
        for (int i = 0; i < intbuffs.Length; i++)
        {
            money += intbuffs[i] * multi[i];
            tickmoney += intbuffs[i] * multi[i];
        }
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("tickmoney", tickmoney);
        StartCoroutine(IdleFarm());
    }
}
