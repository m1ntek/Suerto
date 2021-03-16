using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultController : MonoBehaviour
{
    public DiceV3 diceModel;
    public TMPro.TextMeshProUGUI diceText;
    public int total;

    private bool diceAdded = false;

    private void Update()
    {
        if (diceModel.rollEnded == false && diceModel.tag == "dice")
        {
            //keep randomising results until dice slows down or stops.
            SetResult();
            ClarifyDiceText();
        }
        else if (diceModel.rollEnded == true && diceAdded==false)
        {
            //add to this list when results are complete
            DiceCollection.roundList.Add(diceModel);
            diceAdded = true;
        }
    }

    private void ClarifyDiceText()
    {
        Debug.Log("result: " + diceModel.result + "\tresult%60: " + diceModel.result % 60);
        Debug.Log("result: " + diceModel.result + "\tresult%90: " + diceModel.result % 90);
        if (diceModel.result == 6 || diceModel.result == 9 ||
            diceModel.result >= 60 && (diceModel.result % 60) < 10 && (diceModel.result % 60) > 0 ||
            diceModel.result >= 90 && (diceModel.result % 90) < 10 && (diceModel.result % 90) > 0)
            //underline 6 or 9 results so the user knows what they rolled more clearly.
            //use modulus to check for numbers that are 6x or 9x as well (d100 and 66 may look like 99)
        {
            diceText.text = diceModel.result.ToString();
            diceText.fontStyle = TMPro.FontStyles.Underline;
        }
        else
        {
            diceText.text = diceModel.result.ToString();
            diceText.fontStyle = TMPro.FontStyles.Normal;
        }
    }

    private void SetResult()
    {
        diceModel.result = NumGenerator.GetResult(diceModel.diceMax);
    }

}
