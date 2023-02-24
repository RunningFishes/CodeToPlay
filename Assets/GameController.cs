using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int stageReach = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        int nowStage = PlayerPrefs.GetInt("stageReach", 1);
        if (nowStage > stageReach)
        {
            stageReach = nowStage;
        }
    }

    public void SaveStage()
    {
        PlayerPrefs.SetInt("stageReach", stageReach);
    }

    public void NextStage()
    {
        stageReach++;
        SaveStage();
    }
    public int GetStage()
    {
        return stageReach;
    }

}
