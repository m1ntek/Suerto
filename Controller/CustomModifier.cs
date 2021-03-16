using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomModifier : MonoBehaviour
{
    public Text valueText;
    public ControllerV2 controller;

    public void AddValue()
    {
        ++controller.dal.character.custom;
        CheckValue();
        SetText();
        controller.dal.character.UpdateStats();
        controller.dal.SaveCharToDB();
    }

    public void SubtractValue()
    {
        --controller.dal.character.custom;
        CheckValue();
        SetText();
        controller.dal.character.UpdateStats();
        controller.dal.SaveCharToDB();
    }

    public void ResetValue()
    {
        controller.dal.character.custom = 0;
        SetText();
        controller.dal.character.UpdateStats();
        controller.dal.SaveCharToDB();
    }

    public void SetText()
    {
        valueText.text = controller.dal.character.custom.ToString();
    }

    private void CheckValue()
    {
        if (controller.dal.character.custom >= 20)
            controller.dal.character.custom = 20;
        else if (controller.dal.character.custom <= -10)
            controller.dal.character.custom = -10;
    }
}
