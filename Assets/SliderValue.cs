using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    private Slider slider;
    private Text text;
    void Start()
    {
        slider = GetComponentInParent<Slider>();
        text = GetComponentInParent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}
