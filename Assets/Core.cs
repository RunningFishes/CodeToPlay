using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core instance;

    public List<bool> isRunning;

    void Awake()
    {
        instance = this;
        List<bool> isRunning = new List<bool>();
    }

    public void AddToIsRunning(bool isRunning)
    {
        this.isRunning.Add(isRunning);
    }

    public void RemoveLastIndexFromIsRunning()
    {
        this.isRunning.RemoveAt(this.isRunning.Count - 1);
    }

    public void SetBool(bool value)
    {
        if (isRunning.Count == 0) return;
        isRunning[this.isRunning.Count - 1] = value;
    }

    public bool GetBool(int index)
    {
        return isRunning[index];
    }

    public int GetIsRunningCount()
    {
        return isRunning.Count;
    }
}
