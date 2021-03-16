using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Character
{
    public int str = 0, dex = 0, con = 0, intelligence = 0, wis = 0, cha = 0, proficiency = 2, weap = 0, none = 0, custom = 0;

    public Dictionary<string, int> stats = new Dictionary<string, int>(); //using dictionary to avoid giant if statements...
    public string selectedStat = "none"; //default

    //Unfortunately ran out of time to make this one more modular.
    //Was going to move the methods to a CharacterController and just leave
    //the variables as a model.
    public Character()
    {
        stats.Add("str", 0);
        stats.Add("dex", 0);
        stats.Add("con", 0);
        stats.Add("int", 0);
        stats.Add("wis", 0);
        stats.Add("cha", 0);
        stats.Add("proficiency", 2);
        stats.Add("weap", 0);
        stats.Add("none", 0);
    }

    public void LoadStats()
    {
        stats["str"] = str;
        stats["dex"] = dex;
        stats["con"] = con;
        stats["int"] = intelligence;
        stats["wis"] = wis;
        stats["cha"] = cha;
        stats["proficiency"] = proficiency;
        stats["weap"] = weap;
        GameObject.FindGameObjectWithTag("typeText").GetComponent<Text>().text = selectedStat.ToUpper();
    }

    public void UpdateStats() //save into variables from dictionary to save into database.
    {
        str = stats["str"];
        dex = stats["dex"];
        con = stats["con"];
        intelligence = stats["int"]; //named in full due to clash with "int" value type
        wis = stats["wis"];
        cha = stats["cha"];
        proficiency = stats["proficiency"];
        weap = stats["weap"];
    }
}
