using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class ThrowTypeStatButton : MonoBehaviour
{
    public GameObject throwCanvas;
    public ControllerV2 controller;
    private Text throwLabel;

    private void Start()
    {
        //update selected stat text
        throwLabel = GameObject.FindGameObjectWithTag("typeText").GetComponent<Text>();
        throwLabel.text = tag.ToUpper();
    }

    public void SelectStat()
    {
        if(throwCanvas.activeSelf == true)
        {
            controller.dal.character.selectedStat = tag;
            throwLabel.text = tag.ToUpper();
            throwCanvas.SetActive(false);
            controller.dal.character.UpdateStats();
            controller.dal.SaveCharToDB();
            controller.inUI = false;
        }
    }
}
