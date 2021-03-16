using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class ControllerV2 : MonoBehaviour
{
    public DataAccessLayer dal;
    public Toggle proficiency, expertise;
    public TotalResults tResults;
    public bool inUI ; 
    public string currentDice = "d20"; //default

    private void Start()
    {
        //app starts in login page
        inUI = true;

        //grab info from db.
        dal.GetCharFromDB();
    }
}
