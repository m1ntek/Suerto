using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class Controller : MonoBehaviour
{
    public static Character character;
    public static CustomModifier customModifier;
    public static Toggle proficiency, expertise;
    public static bool inUI;
    public static string currentDice = "d20"; //default

    private void Start()
    {
        proficiency = GameObject.Find("ProficiencyToggle").GetComponent<Toggle>();
        expertise = GameObject.Find("ExpertiseToggle").GetComponent<Toggle>();
        customModifier = GameObject.Find("CustomModifier").GetComponent<CustomModifier>();
    }

    public static void GetFromDB()
    {
        //Controller.character = new Character();
        RestClient.Get<Character>("https://project-suerto.firebaseio.com/.json").Then(
            response =>
            {
                character = response;
                character.LoadStats();
                customModifier.SetText();
                //throwTypeText.text = character.selectedStat;
                Debug.Log("Char initiated");
            });
    }

    public static void SaveCharToDB()
    {
        //character.Strength = int.Parse(strengthValue.text);
        //Debug.Log(character.Strength);
        Controller.character.UpdateStats();
        RestClient.Put($"https://project-suerto.firebaseio.com/.json", Controller.character);
        //RestClient.Post($"https://project-suerto.firebaseio.com/.json", Controller.character);
        Debug.Log("SAVED");

        //StartCoroutine(GetUpdatedDB());
    }

    //    public static Dictionary<string, int> throwTypes = new Dictionary<string, int>();
    //public static int selectedStat;

    //private void Start()
    //{
    //    RestClient.Get<Character>("https://project-suerto.firebaseio.com/.json").Then(
    //        response =>
    //        {
    //            character = response;
    //            Debug.Log("Character initiated!");
    //        });
    //}
}
