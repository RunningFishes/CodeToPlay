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
    
    [SerializeField]
    Direction direction;
    
    private GameObject player;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
    }

    public override void Execute()
    {
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        // before execute
        spriteRenderer.color = ColorController.instance.onExecuteColor;

        // execute
        Core.instance.SetBool(true);
        Vector3 directionVector = SelectVectorFromDirection();
        for (int i = 0; i < 10; i++)
        {
            player.transform.position += directionVector * clockPerExecute/10f * 5;
            yield return new WaitForSeconds(clockPerExecute / 10f);
        }

        // after execute
        spriteRenderer.color = originalColor;

        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        else
            Core.instance.SetBool(false);
    }

    Vector3 SelectVectorFromDirection()
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
