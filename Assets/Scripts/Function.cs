using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : Command
{
    public Command linkedFunctionCommand;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        clockPerExecute = Core.instance.clockPerExecute;
    }

    public override void Execute()
    {
        StartCoroutine(Functioning());
    }

    IEnumerator Functioning()
    {
        // funtion is running
        Core.instance.SetBool(true);

        // create inner status
        int layer = Core.instance.Size();
        Core.instance.Push(false);

        
        spriteRenderer.color = ColorExecute.instance.onExecuteColor;
        yield return new WaitForSeconds(clockPerExecute);
        spriteRenderer.color = originalColor;

        // work on command
        linkedFunctionCommand?.Execute();
        // wait until all command complete
        while (Core.instance.GetBool(layer))
        {
            yield return null;
        }

        // remove inner status
        Core.instance.Pop();

        // if have next work on next command
        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        // no next command so this indent is complete
        else
            Core.instance.SetBool(false);
    }

    public void SetLinkedFunctionCommand(Command command)
    {
        linkedFunctionCommand = command;
    }

    public int GetSizeLinkedFunctionCommand()
    {
        int count = 0;
        Command command = linkedFunctionCommand;
        while (command != null)
        {
            if (command.gameObject.tag == "Loop")
            {
                count += command.GetComponent<Loop>().GetSizeLinkedLoopCommand() + 1;
            }
            else
            {
                count += 1;
            }
            command = command.nextLinkedCommand;
        }
        return count;
    }
}
