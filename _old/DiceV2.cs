using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using UnityEngine.EventSystems;

public class DiceV2 : MonoBehaviour
{
    public int diceMax;
    public int result;
    public bool isThrown = false, isPressed = false, cloneCreated = false, rollEnded = false;
    public float angle, timeStart;
    public Vector2 force, startPos, endPos;
    public Transform diceText;

    private void Start()
    {
        diceMax = 21; //default

        //had to put this into the start method...
        //unfortunately, due to the code being slapped together like mcgyver,
        //I had to initiate the value through the start method.
    }
}
