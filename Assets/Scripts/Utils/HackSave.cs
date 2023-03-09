using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSave : MonoBehaviour
{
    [SerializeField]
    int stageReach;
    [SerializeField]
    bool active;
    
    void Awake()
    {
        if (active)
        {
            GameController.instance.stageReach = stageReach;
        }
    }
}
