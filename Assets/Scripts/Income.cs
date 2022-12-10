using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Income : MonoBehaviour
{
    public int money;
    public int total_money;
    public int[] intbuffs = new int[5];
    public int[] multi = {1, 2, 5, 7, 10};// miltiplayer buffs
    public string buffs;
    // Start is called before the first frame update
    public void Start()
    {
        money = PlayerPrefs.GetInt("money");
        total_money = PlayerPrefs.GetInt("total_money");
        Debug.Log(">" +  PlayerPrefs.GetString("buffs") + "<PPbuffs");
        if (PlayerPrefs.GetString("buffs") == null) buffs = "0,0,0,0,0";
        else buffs = PlayerPrefs.GetString("buffs");
        Debug.Log(">" +  PlayerPrefs.GetString("buffs") + "<PPbuffs");
        Debug.Log(">" + buffs + "<buffs");
        intbuffs = StringToArray(buffs);
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
    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetMoney()
    {
        return this.money;
    }
    public IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        for (int i = 0; i < intbuffs.Length; i++)
        {
            money += intbuffs[i] * multi[i];
        }
        //Debug.Log(money);
        PlayerPrefs.SetInt("money", money);
        StartCoroutine(IdleFarm());
    }
}
