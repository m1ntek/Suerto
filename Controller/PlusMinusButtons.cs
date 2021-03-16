using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusMinusButtons : MonoBehaviour
{
    public ControllerV2 controller;
    public Text value;

    //on plus or minus button
    //find tag of value text
    //increase character stat of tag
    //update text with stat

    int statValue;
    
    private void Start()
    {
        string tag = value.tag;
        value.text = controller.dal.character.stats[tag].ToString();
    }

    public void ProcessStat()
    {
        string tag = value.tag;
        controller.dal.character.stats[tag] = ChangeStat(controller.dal.character.stats[tag]);
    }

    public int ChangeStat(int statValue)
    {
        if(tag == "plus")
        {
            statValue = IncreaseStat(statValue);
        }
        else if(tag == "minus")
        {
            statValue = DecreaseStat(statValue);
        }

        return statValue;
    }

    public int IncreaseStat(int statValue)
    {
        if(value.tag == "proficiency")
        {
            if (statValue >= 6)
                statValue = 6;
            else
                statValue += 1;
        }
        else if(value.tag == "weap")
        {
            if (statValue >= 30)
                statValue = 30;
            else
                statValue += 1;
        }
        else
        {
            if (statValue >= 10)
                statValue = 10;
            else
            {
                statValue += 1;
            }
        }
        

        value.text = statValue.ToString();
        return statValue;
    }

    public int DecreaseStat(int statValue)
    {
        if(value.tag == "proficiency")
        {
            if (statValue <= 2)
                statValue = 2;
            else
                statValue -= 1;
        }
        else if (value.tag == "weap")
        {
            if (statValue <= -15)
                statValue = -15;
            else
                statValue -= 1;
        }
        else
        {
            if (statValue <= -5)
                statValue = -5;
            else
            {
                statValue -= 1;
            }
        }
        

        value.text = statValue.ToString();
        return statValue;
    }
}
