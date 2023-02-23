using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Admin : MonoBehaviour
{
    GameObject Programs;

    void Start()
    {
        Programs = GameObject.Find("Programs");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Programs.GetComponent<Function>().Execute();
        }
    }
}
