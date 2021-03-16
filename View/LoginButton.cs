using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    public ControllerV2 controller;
    public Text email, password;

    public void Login()
    {
        controller.dal.SignInUser(email.text, password.text);
    }
}
