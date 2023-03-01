using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Command
{
    // dash for 2 block
    // can dash through water
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    Direction direction;
    // cant dash through wall

    public override void Execute()
    {
        StartCoroutine(Dashing());
    }
    IEnumerator Dashing()
    {
        // before execute
        spriteRenderer.color = ColorController.instance.onExecuteColor;

        // execute
        Core.instance.SetBool(true);
        Vector3 movement = SelectVectorFromDirection();

        // decrease animate time for player dash looking fast
        float animationTime = clockPerExecute * 0.6f;

        // Here can add some charge animation
        yield return new WaitForSeconds(clockPerExecute * 0.2f);
        
        for (int i = 0; i < 10; i++)
        {
            Player.instance.transform.position += movement * animationTime / 10f * 5 * 2 / 0.6f;
            yield return new WaitForSeconds(animationTime / 10f);
        }
        // Here can add some stop animation
        yield return new WaitForSeconds(clockPerExecute * 0.2f);

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
