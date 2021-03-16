using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SignupErrors
{
    public static string InvalidEmail()
    {
        return "Please ensure correct e-mail format, e.g. email@email.com";
    }

    public static string InvalidPassword()
    {
        return "Password must be at least 7 characters long.";
    }
}
