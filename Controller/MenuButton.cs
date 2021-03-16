using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject menu;
    public ControllerV2 controller;

    //was going to put it in the "view" section but thought because it's
    //doing some logic, maybe it should be in controller.
    public void ToggleMenu()
    {
        if (menu.activeSelf == false)
        {
            controller.inUI = true;
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            controller.inUI = false;
            menu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
