using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSetupButton : MonoBehaviour
{
    public GameObject characterSetup, menu;
    public void LoadCharacterSetup()
    {
        menu.SetActive(false);
        characterSetup.SetActive(true);
    }
}
