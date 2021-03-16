using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalResultsController : MonoBehaviour
{
    public MultithrowController mtController;
    public ControllerV2 controller;
    public DiceV3 diceModel;
    public TMPro.TextMeshPro totalText, totalSubtext, d20d100AdvText, d20d100AdvSubtext, d20d100DadvText, d20d100DadvSubtext;

    public TotalResults tResults;
    public TotalResultsModel defaultTrModel = new TotalResultsModel();
    public TotalResultsModel advTrModel = new TotalResultsModel();
    public TotalResultsModel dadvTrModel = new TotalResultsModel();

    private bool allDiceRolled = false, summed = false;

    private void Update()
    {
        CheckAllDiceRollStatus();

        if (allDiceRolled == true && summed == false && DiceCollection.roundList.Count != 0)
        {
            DetectResultsView();
            ProcessD20D100DiceV2();
            SumOtherDice();
            summed = true;
        }
        //make totals update in real-time, keep calculating result
        else if (summed == true && DiceCollection.roundList.Count != 0 && allDiceRolled == true)
        {
            DetectResultsView();
            ProcessD20D100DiceV2();
            SumOtherDice();
            UpdateTotalText();
            UpdateAdvDadvTotalText(ref advTrModel, ref d20d100AdvText, ref d20d100AdvSubtext, "ADV");
            UpdateAdvDadvTotalText(ref dadvTrModel, ref d20d100DadvText, ref d20d100DadvSubtext, "D.ADV");
        }
    }

    public void PrepareResults(ref TotalResultsModel trModel)
    {
        for (int i = 0; i < trModel.results.Count; i++)
        {
            if (i == 0)
            {
                trModel.preText = trModel.resultLabel[i] + trModel.results[i];
            }
            else
            {
                trModel.preText += " + " + trModel.resultLabel[i] + trModel.results[i];
            }
        }
    }

    private void CheckAllDiceRollStatus()
    {
        for (int i = 0; i < DiceCollection.roundList.Count; i++)
        {
            //don't let loop finish if any dice rolls have not ended
            if (DiceCollection.roundList[i].rollEnded == false)
            {
                allDiceRolled = false;
                summed = false;
                return;
            }
            else if (i == (DiceCollection.roundList.Count - 1) && DiceCollection.roundList[i].rollEnded == true)
            {
                //loop finishes, all rolls have ended
                allDiceRolled = true;
            }
        }
    }

    public void ProcessD20D100DiceV2()
    {
        //reset
        ResetTrModel(ref advTrModel);
        ResetTrModel(ref dadvTrModel);

        //if we're throwing d20's or d100's and threw 2 of them
        if ((controller.currentDice == "d20" && DiceCollection.roundList.Count == 2 && allDiceRolled == true) ||
            (controller.currentDice == "d100" && DiceCollection.roundList.Count == 2 && allDiceRolled == true))
        {
            if (DiceCollection.roundList[0].result >= DiceCollection.roundList[1].result)
            {
                //advantage results
                advTrModel.total += DiceCollection.roundList[0].result;
                advTrModel.results.Add(DiceCollection.roundList[0].result);
                advTrModel.resultLabel.Add(""); //synchronise index numbers between both lists

                //disadvantage results
                dadvTrModel.total += DiceCollection.roundList[1].result;
                dadvTrModel.results.Add(DiceCollection.roundList[1].result);
                dadvTrModel.resultLabel.Add(""); //synchronise index numbers between both lists

                //modifiers after initial sum
                AddModifiers(ref advTrModel);
                AddModifiers(ref dadvTrModel);
            }
            else
            {
                //else opposite indices
                //advantage results
                advTrModel.total += DiceCollection.roundList[1].result;
                advTrModel.results.Add(DiceCollection.roundList[1].result);
                advTrModel.resultLabel.Add(""); //synchronise index numbers between both lists

                //disadvantage results
                dadvTrModel.total += DiceCollection.roundList[0].result;
                dadvTrModel.results.Add(DiceCollection.roundList[0].result);
                dadvTrModel.resultLabel.Add(""); //synchronise index numbers between both lists

                //modifiers after initial sum
                AddModifiers(ref advTrModel);
                AddModifiers(ref dadvTrModel);
            }
        }
    }

    private void ResetTrModel(ref TotalResultsModel trModel)
    {
        trModel.resultLabel.Clear();
        trModel.results.Clear();
        trModel.preText = null;
        trModel.total = 0;
    }

    public void DetectResultsView()
    {
        //switch between advantage/disadvantage results and total results
        if((controller.currentDice == "d20" && DiceCollection.roundList.Count == 2 && allDiceRolled==true) ||
            (controller.currentDice == "d100" && DiceCollection.roundList.Count == 2 && allDiceRolled == true))
        {
            //hide default results view
            totalText.gameObject.SetActive(false);
            totalSubtext.gameObject.SetActive(false);

            //enable d20 advantage/disadvantage view
            d20d100AdvText.gameObject.SetActive(true);
            d20d100AdvSubtext.gameObject.SetActive(true);
            d20d100DadvText.gameObject.SetActive(true);
            d20d100DadvSubtext.gameObject.SetActive(true);
        }

        else
        {
            //do the opposite

            d20d100AdvText.gameObject.SetActive(false);
            d20d100AdvSubtext.gameObject.SetActive(false);
            d20d100DadvText.gameObject.SetActive(false);
            d20d100DadvSubtext.gameObject.SetActive(false);

            totalText.gameObject.SetActive(true);
            totalSubtext.gameObject.SetActive(true);
        }

    }

    //for any other scenario
    public void SumOtherDice()
    {
        //reset
        ResetTrModel(ref defaultTrModel);

        foreach (DiceV3 _dice in DiceCollection.roundList)
        {
            defaultTrModel.total += _dice.result;

            //subtext
            defaultTrModel.resultLabel.Add(""); //just to keep index numbers synchronised
            defaultTrModel.results.Add(_dice.result);
        }

        AddModifiers(ref defaultTrModel);
    }

    private void AddModifiers(ref TotalResultsModel trModel)
    {
        //modifiers after initial sum
        if (controller.proficiency.isOn)
        {
            trModel.total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom + controller.dal.character.proficiency;

            AddSubtextStat(trModel);
            AddSubtextProficiency(trModel);
            AddSubtextCustomModifier(trModel);
        }

        else if (controller.expertise.isOn)
        {
            trModel.total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom + (controller.dal.character.proficiency * 2);

            AddSubtextStat(trModel);
            AddSubtextExpertise(trModel);
            AddSubtextCustomModifier(trModel);
        }

        //just selected stat and custom modifier if not 0
        else
        {
            trModel.total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom;

            AddSubtextStat(trModel);
            AddSubtextCustomModifier(trModel);
        }
    }

    private void UpdateAdvDadvTotalText(ref TotalResultsModel trModel, ref TMPro.TextMeshPro text, ref TMPro.TextMeshPro subtext, string label)
    {
        //display text
        text.fontSize = 250;
        text.text = label;
        text.text += "\n" + trModel.total;
        PrepareResults(ref trModel);
        trModel.maxSubtextChars = 20; //adjust length of calculation according to panel size

        if(trModel.preText != null) //prevent crashing
        {
            tResults.DisplayTrimResults(ref trModel, subtext);
        }
    }

    private void UpdateTotalText()
    {
        //display text
        totalText.text = defaultTrModel.total.ToString();
        PrepareResults(ref defaultTrModel);
        tResults.DisplayTrimResults(ref defaultTrModel, totalSubtext);
    }

    private void AddSubtextExpertise(TotalResultsModel trModel)
    {
        //expertise
        trModel.resultLabel.Add("exp: ");
        trModel.results.Add(controller.dal.character.proficiency * 2);
    }

    private void AddSubtextCustomModifier(TotalResultsModel trModel)
    {
        //display custom modifier if it wasn't 0
        if (controller.dal.character.custom != 0)
        {
            trModel.resultLabel.Add("cus: ");
            trModel.results.Add(controller.dal.character.custom);
        }
    }

    private void AddSubtextProficiency(TotalResultsModel trModel)
    {
        //proficiency
        trModel.resultLabel.Add("pro: ");
        trModel.results.Add(controller.dal.character.proficiency);
    }

    private void AddSubtextStat(TotalResultsModel trModel)
    {
        //stat
        trModel.resultLabel.Add(controller.dal.character.selectedStat.ToLower() + ": ");
        trModel.results.Add(controller.dal.character.stats[controller.dal.character.selectedStat]);
    }
}
