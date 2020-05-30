using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    private Renderer[] renderers;

    bool isActive = false;
    bool makeTransparent = false;
    bool killMe = false;

    float currentTime = 0;
    float fadingSpeed = 2f;
    float startFadingAfterSeconds = 3f;
    private void Start()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>();
    }
    void Update()
    {
        if (isActive)
        {
            TimeUpdate();
        }

        if (makeTransparent)
        {
            MakeTransparent();
        }

        if (killMe)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("I shall be destroyed");
        isActive = true;
    }

    private void MakeTransparent()
    {

        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            if (color.a > 0)
                color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * fadingSpeed);
            renderer.material.color = color;

            if (color.a <= 0)
            {
                makeTransparent = false;
                killMe = true;
            }
        }

    }

    private void TimeUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= startFadingAfterSeconds)
        {
            isActive = false;
            currentTime = 0;
            makeTransparent = true;
        }
    }
}
