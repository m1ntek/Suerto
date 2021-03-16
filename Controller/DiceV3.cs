using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using UnityEngine.EventSystems;

public class DiceV3 : MonoBehaviour
{
    //wasn't sure if this is a model or not because it is still a monobehaviour and
    //must be attached to the dice object.
    public int diceMax;
    public int result;
    public int diceIndex;
    public bool isThrown = false, isPressed = false, cloneCreated = false, rollEnded = false;
    public float angle, timeStart;
    public Vector2 force, startPos, endPos;
    public Transform diceText;
}
