using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attaches to the dice
public class DiceThrowController : MonoBehaviour
{
    public ControllerV2 controller;
    public Rigidbody2D diceRb;
    public DiceTrackerV2 diceTracker = new DiceTrackerV2();
    public float velocityCap = 15f;
    public DiceV3 diceModel;
    public TotalResultsController trController;

    private void Update()
    {
        if (diceModel.tag == "dice")
        {
            //keep checking if the roll is considered ended
            SetRollStatus();
        }

        //re-throws
        //if all the conditions of an already thrown dice is true
        if (diceModel.isPressed == true && diceModel.rollEnded == true &&
            diceModel.cloneCreated == true && diceModel.isThrown == true)
        {
            diceModel.rollEnded = false;
            diceModel.isThrown = false;
        }
        else if (diceModel.isPressed == true && diceModel.rollEnded == false &&
            diceModel.cloneCreated == true && diceModel.isThrown == false)
        {
            TrackPosition();
        }
        else if (diceModel.isPressed == false && diceModel.rollEnded == false &&
            diceModel.cloneCreated == true && diceModel.isThrown == false)
        {
            ThrowDice();
        }
    }

    private void OnMouseDown()
    {
        diceModel.isPressed = true;
    }

    private void OnMouseUp()
    {
        diceModel.isPressed = false;
    }

    public void ThrowDice()
    {
        diceRb.isKinematic = false;

        //calculation to determine how fast the dice will go

        //temp vars
        int indexPerTenthSec = 0;
        float amountOfIndexPerTenthSec = 0;
        
        CalcSpeed(ref indexPerTenthSec, ref amountOfIndexPerTenthSec);
        CalcAngle(indexPerTenthSec);

        diceRb.velocity = diceModel.angle * diceModel.force;

        ApplyVelocityCap(velocityCap);

        diceModel.isThrown = true;
        diceModel.timeStart = Time.time;

        diceTracker.positions.Clear();
        diceTracker.time.Clear();
    }

    private void CalcAngle(int indexPerTenthSec)
    {
        if (diceTracker.positions.Count > 1) //prevent crashing
        {
            Vector2 pos1 = diceTracker.positions[indexPerTenthSec - 1];
            Vector2 pos2 = diceTracker.positions[diceTracker.time.Count - 1];
            diceModel.force = (pos2 - pos1);
            diceModel.angle = Vector2.Angle(pos1, pos2);
        }
    }

    private void CalcSpeed(ref int indexPerTenthSec, ref float amountOfIndexPerTenthSec)
    {
        if (diceTracker.time.Count > 2) //to prevent crashing
        {
            //current time minus the time of the last index
            float timeDifferenceOfOneIndex = Time.time - diceTracker.time[diceTracker.time.Count - 2];
            amountOfIndexPerTenthSec = 0.10f / timeDifferenceOfOneIndex;
            indexPerTenthSec = Convert.ToInt32(amountOfIndexPerTenthSec);
        }
    }

    private void ApplyVelocityCap(float vCap)
    {
        if (diceRb.velocity.x >= vCap)
        {
            diceRb.velocity = new Vector2(vCap, diceRb.velocity.y);
        }

        if (diceRb.velocity.y >= vCap)
        {
            diceRb.velocity = new Vector2(diceRb.velocity.x, vCap);
        }
        if (diceRb.velocity.x <= -vCap)
        {
            diceRb.velocity = new Vector2(-vCap, diceRb.velocity.y);
        }

        if (diceRb.velocity.y <= -vCap)
        {
            diceRb.velocity = new Vector2(diceRb.velocity.x, -vCap);
        }
    }

    public void TrackPosition()
    {
        diceRb.isKinematic = true;
        diceRb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        diceTracker.positions.Add(diceRb.position);
        diceTracker.time.Add(Time.time);
    }

    private void SetRollStatus()
    {
        //if the dice has finished moving or slowed down enough
        if (diceRb.velocity.x <= 0.5 &&
            diceRb.velocity.x >= -0.5 &&
            diceRb.velocity.y <= 0.5 &&
            diceRb.velocity.y >= -0.5 &&
            tag == "dice" &&
            diceModel.isThrown == true)
        {
            diceModel.rollEnded = true;
        }
    }
}
