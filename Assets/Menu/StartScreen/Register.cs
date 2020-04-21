using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{

    public InputField UsernameField;
    public InputField PasswordField;
    public Button SignInButton;
    public GameObject errorMessage;
    void Start()
    {


        SignInButton.onClick.AddListener(() =>
        {

            StartCoroutine(Mian.Instance.Web.Register(UsernameField.text, PasswordField.text));

        });

    }
}
