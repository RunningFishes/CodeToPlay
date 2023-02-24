using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlayStage : MonoBehaviour
{
    string stageName;
    public void LoadStage()
    {
        SceneManager.LoadScene(stageName);
    }
    public void SetStageName(string name)
    {
        stageName = name;
    }
}
