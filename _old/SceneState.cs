using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneState : MonoBehaviour
{
    public static GameObject[] diceStates;
    private static GameObject[] dice;
    //private static Rigidbody2D rb;
    public static Rigidbody2D Rb { get; set; }
    //public static GameObject lastDice;

    public static void SaveDice()
    {
        diceStates = GameObject.FindGameObjectsWithTag("dice");
    }
    public static void GetDice()
    {
        dice = new GameObject[diceStates.Length];

        if (diceStates.Length != 0)
        {
            for (int i = 0; i < diceStates.Length; i++)
            {
                //Instantiate(diceStates[i], GameObject.FindGameObjectWithTag("canvas").transform);
                //dice[i] = new GameObject();
                //dice[i] = Instantiate(GameObject.Find("Dice"), GameObject.FindGameObjectWithTag("canvas").transform);
                dice[i] = Instantiate(Rb.gameObject, GameObject.FindGameObjectWithTag("canvas").transform);
                dice[i] = diceStates[i];
            }
        }
    }
}
