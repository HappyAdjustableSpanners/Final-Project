﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    //public static ColorManager Instance;
    public Color color;
    public static ColorManager Instance;

    void Awake()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    public Color GetCurrentColor()
    {
        return this.color;
    }

    void OnColorChange(HSBColor colorTemp)
    {
        this.color = colorTemp.ToColor();
    }
}