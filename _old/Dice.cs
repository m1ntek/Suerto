//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Proyecto26;
//using System;
//using UnityEngine.EventSystems;

//public class Dice : MonoBehaviour
//{
//    //Public vars
//    public Rigidbody2D rb; //dice controller
//    public TotalResults tResults; //dice controller
//    public Canvas canvas; //dice controller
//    public TMPro.TextMeshPro totalText; //dice controller
//    public int diceMax; //dice
//    public float velocityCap = 15f; //dice controller
//    public int result, total; //result = dice, total = dice controller

//    //Private vars
//    private Rigidbody2D rbClone; //dice controller
//    private bool isThrown = false, isPressed = false, cloneCreated = false, rollEnded = false; //dice
//    private float angle, timeStart; //dice
//    private Vector2 force, startPos, endPos; //dice
//    private System.Random rnd = new System.Random(); //dice controller
//    private Transform diceText; //dice

//    private void Start()
//    {
//        if (tag == "dicecreator")
//        {
//            Controller.GetFromDB();
//        }
//        tResults = GameObject.Find("Controller").GetComponent<TotalResults>();
//    }

//    private void Update()
//    {
//        //dice controller
//        if (isPressed == true && Controller.inUI == false && tag == "dicecreator")
//        {
//            isThrown = false;
//            CreateClone();
//            startPos = rbClone.position;
//            TrackPosition();
//        }

//        //dice controller
//        else
//        {
//            if (isThrown == false && cloneCreated == true)
//            {
//                endPos = rbClone.position;
//                ThrowDice();
//            }
//        }

//        //dice controller
//        if (cloneCreated == true)
//        {
//            if (rollEnded == false &&
//            Time.time >= timeStart + 0.10f &&
//            isThrown == true)
//            {
//                cloneCreated = false;
//                rollEnded = true;
//                total += result;
//                if(Controller.proficiency.isOn)
//                {
//                    total += Controller.character.stats[Controller.character.selectedStat] + Controller.character.custom + Controller.character.proficiency;
//                    tResults.results.Add(result);
//                    tResults.results.Add(Controller.character.stats[Controller.character.selectedStat]);
//                    tResults.results.Add(Controller.character.custom);
//                    tResults.results.Add(Controller.character.proficiency);
//                }
//                else if(Controller.expertise.isOn)
//                {
//                    total += Controller.character.stats[Controller.character.selectedStat] + Controller.character.custom + (Controller.character.proficiency * 2);
//                    tResults.results.Add(result);
//                    tResults.results.Add(Controller.character.stats[Controller.character.selectedStat]);
//                    tResults.results.Add(Controller.character.custom);
//                    tResults.results.Add(Controller.character.proficiency*2);
//                }
//                else if(Controller.currentDice == "d100")
//                {
//                    //don't do any calculations.
//                }
//                else
//                {
//                    total += Controller.character.stats[Controller.character.selectedStat] + Controller.character.custom;
//                    tResults.results.Add(result);
//                    tResults.results.Add(Controller.character.stats[Controller.character.selectedStat]);
//                    tResults.results.Add(Controller.character.custom);
//                }

//                totalText.text = total.ToString();
//                tResults.PrepareResults();
//            }

//            else if (rollEnded == false)
//            {
//                diceText = rbClone.transform.Find("Text (TMP)");
//                SetResult();

//                if (result == 6 || result == 9) //underline 6 or 9 results so the user knows what they rolled more clearly.
//                {
//                    diceText.GetComponent<TMPro.TextMeshProUGUI>().text = result.ToString();
//                    diceText.GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
//                }
//                else
//                {
//                    diceText.GetComponent<TMPro.TextMeshProUGUI>().text = result.ToString();
//                    diceText.GetComponent<TMPro.TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
//                }
//            }

//            Debug.Log("RollEnded Status: " + rollEnded);
//            Debug.Log("Velocity: " + rbClone.velocity);
//        } 
//    }

//    //dice controller
//    private void SetResult()
//    {
//        result = rnd.Next(1, diceMax);
//    }

//    //dice controller
//    private void OnMouseDown()
//    {
//        isPressed = true;
//    }

//    //dice controller
//    private void OnMouseUp()
//    {
//        isPressed = false;
//    }

//    //dice controller
//    private void ThrowDice()
//    {

//        rbClone.isKinematic = false;

//        int trackerCount = DiceTracker.time.Count;
//        float tempTime = Time.time;

//        int indexPerTenthSecInt = 0;
//        float amountOfIndexPerTenthSecond = 0;

//        if (DiceTracker.time.Count>2) //Trying to prevent crashing
//        {
//            float timeDifferenceOfOneIndex = tempTime - DiceTracker.time[DiceTracker.time.Count - 2];
//            amountOfIndexPerTenthSecond = 0.10f / timeDifferenceOfOneIndex;
//            indexPerTenthSecInt = Convert.ToInt32(amountOfIndexPerTenthSecond);
//        }

//        Vector2 pos1 = DiceTracker.positions[indexPerTenthSecInt - 1];
//        Vector2 pos2 = DiceTracker.positions[trackerCount - 1];
//        force = (pos2 - pos1);
//        angle = Vector2.Angle(pos1, pos2);

//        rbClone.velocity = angle * force;

//        ApplyVelocityCap(velocityCap);

//        isThrown = true;
//        timeStart = Time.time;


//        DiceTracker.positions.Clear();
//        DiceTracker.time.Clear();
//    }

//    //dice controller
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

//    //dice controller
//    private void TrackPosition()
//    {
//        if (isPressed == true)
//        {
//            rbClone.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            DiceTracker.positions.Add(rbClone.position);
//            DiceTracker.time.Add(Time.time);

//            Debug.Log($"Dice velocity: {rbClone.velocity}");
//        }
//    }

//    //dice controller
//    private void CreateClone()
//    {
//        if (cloneCreated == false)
//        {
//            rollEnded = false;
//            rbClone = Instantiate(rb, canvas.transform);
//            rbClone.tag = "dice";

//            Debug.Log("New instantiation");

//            rbClone.isKinematic = true;

//            cloneCreated = true;
//        }
//    }
//}
