using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SignupButton : MonoBehaviour
{
    public ControllerV2 controller;
    public Text email, password;
  
    public void Signup()
    {
        controller.dal.SignUpUser(email.text, password.text);
    }
}
