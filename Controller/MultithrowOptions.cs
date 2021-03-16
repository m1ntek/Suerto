using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultithrowOptions : MonoBehaviour
{
    public Text valueText;

    public void IncreaseValue()
    {
        Multithrow.value = CapValue(++Multithrow.value);
        UpdateText();
    }

    public void DecreaseValue()
    {
        Multithrow.value = CapValue(--Multithrow.value);
        UpdateText();
    }

    public void ResetValue()
    {
        Multithrow.value = 1;
        UpdateText();
    }

    public int CapValue(int _value)
    {
        if (_value >= Multithrow.maxCap)
        {
            _value = Multithrow.maxCap;
        }
        else if (_value <= Multithrow.minCap)
        {
            _value = Multithrow.minCap;
        }

        return _value;
    }

    public void UpdateText()
    {
        valueText.text = Multithrow.value.ToString();
    }
}
