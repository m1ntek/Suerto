using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBackButton : MonoBehaviour
{
    public GameObject charCanvas, menu;

    public void CloseCanvas()
    {
        charCanvas.SetActive(false);
        menu.SetActive(true);
    }
}
