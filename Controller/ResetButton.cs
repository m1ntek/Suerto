using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public DiceV3 dice;
    public TotalResultsController trController;
    public ControllerV2 controller;
    public TMPro.TextMeshPro totalSubtext;

    public void DestroyDice()
    {
        GameObject[] thrownDices = GameObject.FindGameObjectsWithTag("dice");
        if (thrownDices.Length != 0)
        {
            DiceCollection.roundList.Clear();

            foreach (GameObject dice in thrownDices)
            {
                Destroy(dice);
            }
        }
    }

    public void ResetText()
    {
        trController.totalText.text = "0";
        trController.defaultTrModel.total = 0;
        trController.defaultTrModel.results.Clear();
        trController.defaultTrModel.resultLabel.Clear();
        totalSubtext.text = "";
        trController.DetectResultsView();
    }
}
