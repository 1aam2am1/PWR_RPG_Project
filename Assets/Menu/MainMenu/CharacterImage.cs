using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour
{
    public Image image;
    private int idx;
    public List<Sprite> characterImages;

    // Start is called before the first frame update
    private void Start()
    {
        idx = 0;
        ShowImage();
    }
    public void Right()
    {
        idx = idx + 1;
        if (idx >= characterImages.Count)
            idx = 0;
        ShowImage();
    }

    public void Left()
    {
        idx = idx - 1;
        if (idx < 0)
            idx = characterImages.Count -1 ;
        ShowImage();
    }

    public void ShowImage()
    {
        image.sprite = characterImages[idx];
        image.color = Color.white;
    }

}
