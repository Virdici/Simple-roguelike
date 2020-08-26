using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public RawImage image;
    public float opacity = 0;
    public float fadeSpeed = 0.05f;

    private void Awake()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }

    

    private void Update()
    {
        if (GameController.IsDoneLoading == false)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }

        if (GameController.IsDoneLoading == true)
        {
            
                image.color = Color.Lerp(image.color, image.color = new Color(image.color.r, image.color.g, image.color.b, 0f), fadeSpeed * Time.deltaTime);
           
        }

    }
}
