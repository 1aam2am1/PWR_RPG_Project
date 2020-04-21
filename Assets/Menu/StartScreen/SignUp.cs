using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;
    public InputField password2;

    public GameObject errorMessage;

    public void Start()
    {

    }

    public void SignUpButtonAction()
    {
        // Username check
        // Email check
        // Password check
        if (password.text != password2.text)
            errorMessage.SetActive(true);
        else
            errorMessage.SetActive(false);

    }


}
