using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{

    public InputField username;
    public InputField password;

    public GameObject errorMessage;

    public void SignInButtonAction()
    {
        // Username and password check

        if (username.text == "admin" && password.text == "1234")
        {
            errorMessage.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
        else
            errorMessage.SetActive(true);
    }

}
