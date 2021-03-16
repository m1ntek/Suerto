using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ThrowTypeButton : MonoBehaviour
{
    public GameObject throwTypeCanvas;
    public ControllerV2 controller;

    public void ToggleThrowType()
    {
        if (throwTypeCanvas.activeSelf == false)
        {
            throwTypeCanvas.SetActive(true);
            controller.inUI = true;
        }
            
        else
        {
            throwTypeCanvas.SetActive(false);
        }
    }
}
