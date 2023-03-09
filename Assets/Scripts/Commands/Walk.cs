using System;
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
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private readonly float speed = 5.325f;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    public override void Execute()
    {
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        // before execute
        spriteRenderer.color = ColorController.instance.onExecuteColor;

        // get animation
        string animationName = SelectAnimationFromDirection(direction);
        // set animation
        playerAnim.SetBool(animationName, true);
        // set sprite depend on left or right
        if (direction == Direction.Left)
            playerSprite.flipX = true;
        if (direction == Direction.Right)
            playerSprite.flipX = false;

        // execute
        Core.instance.SetBool(true);
        Vector3 movement = SelectVectorFromDirection(direction);
        for (int i = 0; i < 10; i++)
        {
            player.transform.position += movement * clockPerExecute/10f * speed;
            yield return new WaitForSeconds(clockPerExecute / 10f);
        }

        // unset animation
        playerAnim.SetBool(animationName, false);

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

    String SelectAnimationFromDirection(Direction di)
    {
        if (direction == Direction.Up)
        {
            return "WalkUp";
        }
        else if (direction == Direction.Down)
        {
            return "WalkDown";
        }
        else if (direction == Direction.Left)
        {
            return "WalkSide";
        }
        else if (direction == Direction.Right)
        {
            return "WalkSide";
        }
        return "Idle";
    }
}
