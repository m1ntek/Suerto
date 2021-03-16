using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleProcessor : MonoBehaviour
{
    public ControllerV2 controller;
    public void ProcessExpertise()
    {
        controller.proficiency.isOn = false;
    }

    public void ProcessProficiency()
    {
        controller.expertise.isOn = false;
    }
}
