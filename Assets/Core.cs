using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core instance;

    public bool isRunning = false;

    void Awake()
    {
        instance = this;
    }
}
