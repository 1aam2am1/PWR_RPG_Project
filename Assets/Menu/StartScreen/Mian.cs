using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mian : MonoBehaviour
{
    public static Mian Instance;

    public Web Web;
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
    }


}
