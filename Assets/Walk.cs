using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : Command
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    GameObject player;
    [SerializeField]
    Direction direction;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    public override void Execute()
    {
        StartCoroutine(WalkOnDirection());
    }

    IEnumerator WalkOnDirection()
    {
        Core.instance.SetBool(true);
        
        if (direction == Direction.Up)
        {
            for (int i = 0; i < 10; i++)
            {
                player.transform.position += new Vector3(0, 1, 0) * 0.03f * 5;
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (direction == Direction.Down)
        {
            for (int i = 0; i < 10; i++)
            {
                player.transform.position += new Vector3(0, -1, 0) * 0.03f * 5;
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = 0; i < 10; i++)
            {
                player.transform.position += new Vector3(-1, 0, 0) * 0.03f * 5;
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (direction == Direction.Right)
        {
            for (int i = 0; i < 10; i++)
            {
                player.transform.position += new Vector3(1, 0, 0) * 0.03f * 5;
                yield return new WaitForSeconds(0.03f);
            }
        }
        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        else
            Core.instance.SetBool(false);
    }
}
