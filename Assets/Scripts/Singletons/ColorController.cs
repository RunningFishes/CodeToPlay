using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController instance;

    public Color onExecuteColor;
    
    public float onHoldedTransparent;

    private void Awake()
    {
        instance = this;
    }
}
