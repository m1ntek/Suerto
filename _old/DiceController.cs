//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DiceController : MonoBehaviour
//{
//    public ControllerV2 controller;
//    public Rigidbody2D rb;
//    public TotalResults tResults; 
//    public Canvas canvas; 
//    public TMPro.TextMeshPro totalText; 
//    public float velocityCap = 15f; 
//    public int total;
//    public DiceV2 dice;
//    //public bool dice.isPressed = false;

//    private Rigidbody2D rbClone;
//    private System.Random rnd = new System.Random();
//    private void Update()
//    {
//        if (dice.isPressed == true && controller.inUI == false && tag == "dicecreator")
//        {
//            dice.isThrown = false;
//            CreateClone();
//            dice.startPos = rbClone.position;
//            TrackPosition();
//        }

//        else
//        {
//            if (dice.isThrown == false && dice.cloneCreated == true)
//            {
//                dice.endPos = rbClone.position;
//                ThrowDice();
//            }
//        }

//        if (dice.cloneCreated == true)
//        {
//            if (dice.rollEnded == false &&
//            Time.time >= dice.timeStart + 0.10f &&
//            dice.isThrown == true)
//            {
//                dice.cloneCreated = false;
//                dice.rollEnded = true;
//                total += dice.result;
//                if (controller.proficiency.isOn)
//                {
//                    total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom + controller.dal.character.proficiency;
//                    tResults.resultLabel.Add("dice: ");
//                    tResults.results.Add(dice.result);
//                    tResults.resultLabel.Add(controller.dal.character.selectedStat.ToLower() + ": ");
//                    tResults.results.Add(controller.dal.character.stats[controller.dal.character.selectedStat]);
//                    tResults.resultLabel.Add("pro: ");
//                    tResults.results.Add(controller.dal.character.proficiency);

//                    if (controller.dal.character.custom != 0)
//                    {
//                        tResults.resultLabel.Add("cus: ");
//                        tResults.results.Add(controller.dal.character.custom);
//                    }
//                }
//                else if (controller.expertise.isOn)
//                {
//                    total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom + (controller.dal.character.proficiency * 2);
//                    tResults.resultLabel.Add("dice: ");
//                    tResults.results.Add(dice.result);
//                    tResults.resultLabel.Add(controller.dal.character.selectedStat.ToLower() + ": ");
//                    tResults.results.Add(controller.dal.character.stats[controller.dal.character.selectedStat]);
//                    tResults.resultLabel.Add("exp: ");
//                    tResults.results.Add(controller.dal.character.proficiency * 2);

//                    if (controller.dal.character.custom != 0)
//                    {
//                        tResults.resultLabel.Add("cus: ");
//                        tResults.results.Add(controller.dal.character.custom);
//                    }
//                }
//                else
//                {
//                    total += controller.dal.character.stats[controller.dal.character.selectedStat] + controller.dal.character.custom;
//                    tResults.resultLabel.Add("dice: ");
//                    tResults.results.Add(dice.result);
//                    tResults.resultLabel.Add(controller.dal.character.selectedStat.ToLower() + ": ");
//                    tResults.results.Add(controller.dal.character.stats[controller.dal.character.selectedStat]);

//                    if (controller.dal.character.custom != 0)
//                    {
//                        tResults.resultLabel.Add("cus: ");
//                        tResults.results.Add(controller.dal.character.custom);
//                    }                        

//                }

//                totalText.text = total.ToString();
//                tResults.PrepareResults();
//                tResults.DisplayTrimResults();
//            }

//            else if (dice.rollEnded == false)
//            {
//                dice.diceText = rbClone.transform.Find("Text (TMP)");
//                SetResult();

//                if (dice.result == 6 || dice.result == 9) //underline 6 or 9 results so the user knows what they rolled more clearly.
//                {
//                    dice.diceText.GetComponent<TMPro.TextMeshProUGUI>().text = dice.result.ToString();
//                    dice.diceText.GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
//                }
//                else
//                {
//                    dice.diceText.GetComponent<TMPro.TextMeshProUGUI>().text = dice.result.ToString();
//                    dice.diceText.GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
//                }
//            }
//        }
//    }

//    private void OnMouseDown()
//    {
//        dice.isPressed = true;
//    }

//    private void OnMouseUp()
//    {
//        dice.isPressed = false;
//    }

//    private void SetResult()
//    {
//        dice.result = rnd.Next(1, dice.diceMax);
//    }

//    private void ThrowDice()
//    {

//        rbClone.isKinematic = false;

//        int trackerCount = DiceTracker.time.Count;
//        float tempTime = Time.time;

//        int indexPerTenthSecInt = 0;
//        float amountOfIndexPerTenthSecond = 0;

//        if (DiceTracker.time.Count > 2) //to prevent crashing
//        {
//            float timeDifferenceOfOneIndex = tempTime - DiceTracker.time[DiceTracker.time.Count - 2];
//            amountOfIndexPerTenthSecond = 0.10f / timeDifferenceOfOneIndex;
//            indexPerTenthSecInt = Convert.ToInt32(amountOfIndexPerTenthSecond);
//        }

//        Vector2 pos1 = DiceTracker.positions[indexPerTenthSecInt - 1];
//        Vector2 pos2 = DiceTracker.positions[trackerCount - 1];
//        dice.force = (pos2 - pos1);
//        dice.angle = Vector2.Angle(pos1, pos2);

//        rbClone.velocity = dice.angle * dice.force;

//        ApplyVelocityCap(velocityCap);

//        dice.isThrown = true;
//        dice.timeStart = Time.time;


//        DiceTracker.positions.Clear();
//        DiceTracker.time.Clear();
//    }

//    private void ApplyVelocityCap(float vCap)
//    {
//        if (rbClone.velocity.x >= vCap)
//        {
//            rbClone.velocity = new Vector2(vCap, rbClone.velocity.y);
//        }

//        if (rbClone.velocity.y >= vCap)
//        {
//            rbClone.velocity = new Vector2(rbClone.velocity.x, vCap);
//        }
//        if (rbClone.velocity.x <= -vCap)
//        {
//            rbClone.velocity = new Vector2(-vCap, rbClone.velocity.y);
//        }

//        if (rbClone.velocity.y <= -vCap)
//        {
//            rbClone.velocity = new Vector2(rbClone.velocity.x, -vCap);
//        }
//    }

//    private void TrackPosition()
//    {
//        if (dice.isPressed == true)
//        {
//            rbClone.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            DiceTracker.positions.Add(rbClone.position);
//            DiceTracker.time.Add(Time.time);

//            Debug.Log($"Dice velocity: {rbClone.velocity}");
//        }
//    }

//    private void CreateClone()
//    {
//        if (dice.cloneCreated == false)
//        {
//            dice.rollEnded = false;
//            rbClone = Instantiate(rb, canvas.transform);
//            rbClone.tag = "dice";

//            Debug.Log("New instantiation");

//            rbClone.isKinematic = true;

//            dice.cloneCreated = true;
//        }
//    }
//}
