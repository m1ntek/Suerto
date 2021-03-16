using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelector : MonoBehaviour
{
    public DiceV3 dice;
    public ControllerV2 controller;

    public void SetDice(string diceTag)
    {
        switch (diceTag)
        {
            case "d20":
                dice.diceMax = 21;
                SetText("D20");
                controller.currentDice = "d20";
                break;
            case "d12":
                dice.diceMax = 13;
                SetText("D12");
                controller.currentDice = "d12";
                break;
            case "d10":
                dice.diceMax = 11;
                SetText("D10");
                controller.currentDice = "d10";
                break;
            case "d100":
                dice.diceMax = 101;
                SetText("D100");
                controller.currentDice = "d100";
                break;
            case "d8":
                dice.diceMax = 9;
                SetText("D8");
                controller.currentDice = "d8";
                break;
            case "d6":
                dice.diceMax = 7;
                SetText("D6");
                controller.currentDice = "d6";
                break;
            case "d4":
                dice.diceMax = 5;
                SetText("D4");
                controller.currentDice = "d4";
                break;
        }
    }

    private void SetText(string text)
    {
        Transform diceText = dice.transform.Find("Text (TMP)");
        diceText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        Debug.Log($"Dice text set to {text}");
    }
}
