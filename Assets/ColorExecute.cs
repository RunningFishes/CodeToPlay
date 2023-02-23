using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorExecute : MonoBehaviour
{
    public static ColorExecute instance;

    public Color onExecuteColor;

    private void Awake()
    {
        instance = this;
    }
}
