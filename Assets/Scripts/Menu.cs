using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    GameObject characterPanel;
    // Start is called before the first frame update
    void Start()
    {
        characterPanel = GameObject.Find("CharacterPanel");
    }

    // Update is called once per frame
    void Update()
    {
        ShowCharacterPanel();
    }

    private void ShowCharacterPanel()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!characterPanel.activeSelf)
            {
                characterPanel.SetActive(true);
            }
            else
            {
                characterPanel.SetActive(false);
            }
        }
    }
}
