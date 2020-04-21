using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class Web : MonoBehaviour
{

    public GameObject errorMessage;
    public GameObject errorMessage2;
    public GameObject errorMessage3;
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetText());
       // StartCoroutine(Login("Tester", "Test123"));
        //StartCoroutine(Register("Romek", "Romek"));
    }

    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/connectphp/GetDate.php"))
        {

            yield return www.Send();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }
   public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/connectphp/SignIn.php", form))
        {

            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                string dostep = www.downloadHandler.text;
                if (dostep == " Login success")
                {
                    Debug.Log(www.downloadHandler.text);
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    Debug.Log("hehehe zle");
                    errorMessage.SetActive(true);
                }
            }
        }
    }
    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/connectphp/register.php", form))
        {

            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string dostep = www.downloadHandler.text;
                if (dostep == "Account Created")
                {
                    Debug.Log(www.downloadHandler.text);
                    errorMessage2.SetActive(false);
                    errorMessage3.SetActive(true);
                }
                else
                {
                    Debug.Log("hehehe zle");
                    errorMessage2.SetActive(true);
                }

            }
        }
    }

}
