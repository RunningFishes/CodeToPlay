using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onCommandComplete;

    public void OnCommandComplete()
    {
        onCommandComplete?.Invoke();
    }
}
