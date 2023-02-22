using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : Command
{
    public int loopCount;
    public Command linkedLoopCommand;
    
    public override void Execute()
    {
        StartCoroutine(Looping());
    }

    IEnumerator Looping()
    {
        // loop is running
        Core.instance.SetBool(true);

        // create inner status
        int layer = Core.instance.GetIsRunningCount();
        Core.instance.AddToIsRunning(false);


        for (int i = 0; i < loopCount; i++)
        {
            // work on this command
            linkedLoopCommand?.Execute();
            // wait until upper command complete
            while (Core.instance.GetBool(layer))
            {
                yield return null;
            }
        }

        // remove inner status
        Core.instance.RemoveLastIndexFromIsRunning();

        // if have next work on next command
        if (nextLinkedCommand != null)
            nextLinkedCommand.Execute();
        // no next command so this indent is complete
        else
            Core.instance.SetBool(false);
    }
    
    public void SetLinkedLoopCommand(Command command)
    {
        linkedLoopCommand = command;
    }
}
