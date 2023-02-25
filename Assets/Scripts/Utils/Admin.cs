using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Admin : MonoBehaviour
{
    GameObject Main;
    
    void Start()
    {
        Main = GameObject.Find("Main");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Core.instance.isGameRunning())
                Main.GetComponent<Function>().Execute();
        }
    }
}
