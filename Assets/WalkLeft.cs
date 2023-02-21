using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkLeft : Command
{
    GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    public override void Execute()
    {
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        Core.instance.isRunning = true;
        for (int i = 0; i < 10; i++)
        {
            player.transform.position += Vector3.left * 0.03f * 5;
            yield return new WaitForSeconds(0.03f);
        }
        Core.instance.isRunning = false;
    }
}
