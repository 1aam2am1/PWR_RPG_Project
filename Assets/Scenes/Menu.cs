using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isStart;
    public bool isQuit;

    void Start()
    {
        Debug.Log("Start");
        GetComponent<Graphic>().color = Color.black;
        //GetComponent<Renderer>().material.color = Color.black;
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        GetComponent<Graphic>().color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Leve");
        GetComponent<Graphic>().color = Color.black;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isStart)
        {
            //Application.LoadScene(1);
            Debug.Log("Nomore");
        }
        if (isQuit)
        {
            // save any game data here
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
