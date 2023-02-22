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

        Vector3 movement = SelectVectorFromDirection(direction);
        for (int i = 0; i < 10; i++)
        {
            player.transform.position += movement * 0.03f * 5;
            yield return new WaitForSeconds(0.03f);
        }

        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        else
            Core.instance.SetBool(false);
    }

    Vector3 SelectVectorFromDirection(Direction di)
    {
        if (direction == Direction.Up)
        {
            return new Vector3(0, 1, 0);
        }
        else if (direction == Direction.Down)
        {
            return new Vector3(0, -1, 0);
        }
        else if (direction == Direction.Left)
        {
            return new Vector3(-1, 0, 0);
        }
        else if (direction == Direction.Right)
        {
            return new Vector3(1, 0, 0);
        }
        return new Vector3(0, 0, 0);
    }
}
