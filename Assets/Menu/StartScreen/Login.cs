using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{


    public InputField UsernameField;
    public InputField PasswordField;
    public Button SignInButton;
    public GameObject errorMessage;
    void Start()
    {


        SignInButton.onClick.AddListener(() =>
    {
     
            StartCoroutine(Mian.Instance.Web.Login(UsernameField.text, PasswordField.text));

    });

    }


}
