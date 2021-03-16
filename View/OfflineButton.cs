using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineButton : MonoBehaviour
{
	public ControllerV2 controller;
	public GameObject loginPanel;

    public void OfflineLogin()
	{
		//unfreeze rest of app.
		controller.inUI = false;

		//close login canvas
		loginPanel.SetActive(false);

        //create a new character
        controller.dal.CreateNewChar();
	}
}
