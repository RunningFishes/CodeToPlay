using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetBool("Open", true);
        Invoke("GoNextScene", 5f);

    }

    public void GoNextScene()
    {
        string name = SceneManager.GetActiveScene().name;
        Debug.Log(name);
        string index = name.Substring(5);
        int nextIndex = int.Parse(index) + 1;
        SceneManager.LoadScene("Stage" + nextIndex);
    }
}
