using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultithrowController : MonoBehaviour
{
    public Canvas mainCanvas;
    public Rigidbody2D diceRb;
    public DiceV3 diceModel;
    public ControllerV2 controller;
    public TotalResultsController trController;

    public bool dieCreated = false, dieThrown = false;

    private void Update()
    {
        if (controller.inUI == false)
        {
            if (diceModel.isPressed == true)
            {
                CreateMultipleDice();
                TrackMultipleDice();
            }
            else if (diceModel.isPressed == false && dieCreated == true)
            {
                ThrowMultipleDice();

                dieCreated = false;
                dieThrown = false;

                DiceCollection.throwList.Clear();
            }
        }
    }

    public void TrackMultipleDice()
    {
        if(dieCreated==true && dieThrown==false)
        {
            for (int i = 0; i < Multithrow.value; i++)
            {
                DiceCollection.throwList[i].GetComponent<DiceThrowController>().TrackPosition();
            }
        }
    }

    public void ThrowMultipleDice()
    {
        if(dieThrown==false)
        {
            for (int i = 0; i < Multithrow.value; i++)
            {
                DiceCollection.throwList[i].GetComponent<DiceV3>().endPos = DiceCollection.throwList[i].GetComponent<Rigidbody2D>().position;
                DiceCollection.throwList[i].GetComponent<DiceV3>().isPressed = false;
                DiceCollection.throwList[i].GetComponent<DiceThrowController>().ThrowDice();
            }
            dieThrown = true;
        }
    }

    public void CreateMultipleDice()
    {
        if(dieCreated == false)
        {
            for (int i = 0; i < Multithrow.value; i++)
            {
                DiceCollection.throwList.Add(Instantiate(diceRb, mainCanvas.transform));
                DiceCollection.throwList[i].tag = "dice";
                DiceCollection.throwList[i].GetComponent<DiceV3>().diceIndex = i;
                DiceCollection.throwList[i].GetComponent<DiceV3>().isPressed = true;
                DiceCollection.throwList[i].GetComponent<DiceV3>().cloneCreated = true;
                DiceCollection.throwList[i].GetComponent<Rigidbody2D>().isKinematic = true;
            }
            dieCreated = true;
        }
    }
}
