using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using Proyecto26;

public class SaveButton : MonoBehaviour
{
    public ControllerV2 controller;
    public void Save()
    {
        controller.dal.character.UpdateStats();
        controller.dal.SaveCharToDB();
    }
}
