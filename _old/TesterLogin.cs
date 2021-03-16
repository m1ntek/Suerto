using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TesterLogin : MonoBehaviour
{
    public InputField email, pw;

    public void Login()
    {
        email.text = "tester1@test.com";
        pw.text = "1234567";
    }
}
