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

    void Awake()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        clockPerExecute = Core.instance.clockPerExecute;
    }

    public override void Execute()
    {
        StartCoroutine(WalkOnDirection());
    }

    IEnumerator WalkOnDirection()
    {
        // before execute
        spriteRenderer.color = ColorExecute.instance.onExecuteColor;

        // execute
        Core.instance.SetBool(true);
        Vector3 movement = SelectVectorFromDirection(direction);
        for (int i = 0; i < 10; i++)
        {
            player.transform.position += movement * clockPerExecute/10f * 5;
            yield return new WaitForSeconds(clockPerExecute / 10f);
        }

        // after execute
        spriteRenderer.color = originalColor;

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
