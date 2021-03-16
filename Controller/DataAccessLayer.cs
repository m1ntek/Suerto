using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using System;

public class DataAccessLayer : MonoBehaviour
{
    public User user;
    public Character character;
    public CustomModifier customModifier;
    public Text statusText;
    public ControllerV2 controller;
    public GameObject loginPanel;

    private string apiKey = "AIzaSyBGZxjrX_Lg_YW8R-uf5Vag4mBgppMfb0U";
    private string signinUrl = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=";
    private string signupUrl = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=";

    public void SignInUser(string _email, string _pw)
    {
        //quick json conversion
        string userJson = "{\"email\":\"" + _email + "\",\"password\":\"" + _pw + "\",\"returnSecureToken\":true}";

        RestClient.Post<User>($"{signinUrl}{apiKey}", userJson).Then(
            response =>
            {
                user.localId = response.localId;
                user.idToken = response.idToken;

                //for testing
                Debug.Log(response.localId);
                Debug.Log(response.idToken);

                //grab user's char
                GetCharFromDB();

                //unfreeze rest of app.
                controller.inUI = false;

                //close login canvas
                loginPanel.SetActive(false);
            }).Catch(
            error =>
            {
                clearMsg(statusText);
                statusText.text = "Username or password incorrect: " + error.Message;
                StartCoroutine(clearMsg(statusText));
            });
    }

    public void SignUpUser(string _email, string _pw)
    {
        //quick json conversion
        string userJson = "{\"email\":\"" + _email + "\",\"password\":\"" + _pw + "\",\"returnSecureToken\":true}";

        RestClient.Post<User>($"{signupUrl}{apiKey}", userJson).Then(
            response =>
            {
                //save ID's from firebase
                user.localId = response.localId;
                user.idToken = response.idToken;

                //unfreeze rest of app.
                controller.inUI = false;

                //close login canvas
                loginPanel.SetActive(false);

                //save new character to new user after response
                CreateNewChar();
                SaveCharToDB();
            }).Catch(
            error =>
            {
                clearMsg(statusText);
                if (_email.Contains("@") != true || _email.Contains(".") != true)
                {
                    statusText.text = SignupErrors.InvalidEmail();
                }
                else if (_pw.Length < 7)
                {
                    statusText.text = SignupErrors.InvalidPassword();
                }
                else
                {
                    statusText.text = error.Message;
                }
                StartCoroutine(clearMsg(statusText));
            });
    }

    public void GetCharFromDB()
    {
        RestClient.Get<Character>($"https://project-suerto.firebaseio.com/users/{user.localId}/characters/.json?auth={user.idToken}").Then(
            response =>
            {
                character = response;
                character.LoadStats();
                customModifier.SetText();
                Debug.Log("Char initiated");
            });
    }

    public void SaveCharToDB()
    {
        //character.UpdateStats();
        RestClient.Put($"https://project-suerto.firebaseio.com/users/{user.localId}/characters/.json?auth={user.idToken}", character);
        Debug.Log("SAVED");
        Debug.Log(user.idToken);
    }

    public void CreateNewChar()
    {
        character = new Character();
    }

    public void GetCharFromDB_OLD()
    {
        RestClient.Get<Character>("https://project-suerto.firebaseio.com/.json").Then(
            response =>
            {
                character = response;
                character.LoadStats();
                customModifier.SetText();
                Debug.Log("Char initiated");
            });
    }

    public void SaveCharToDB_OLD()
    {
        character.UpdateStats();
        RestClient.Put($"https://project-suerto.firebaseio.com/.json", character);
        Debug.Log("SAVED");
    }

    IEnumerator clearMsg(Text _text)
    {
        yield return new WaitForSecondsRealtime(5);

        _text.text = "";
    }
}
