using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCommand : MonoBehaviour
{
    public void LoadStageScene()
    {
        SceneManager.LoadScene("Stage");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void CreditRepository()
    {
        Application.OpenURL("https://github.com/JiMeow/CodeToPlay");
    }

    public void CreditJimeow()
    {
        Application.OpenURL("https://github.com/JiMeow");
    }

    public void CreditMinzung()
    {
        Application.OpenURL("https://github.com/minza55113151");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
