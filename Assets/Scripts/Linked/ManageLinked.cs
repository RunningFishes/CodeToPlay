using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLinked : MonoBehaviour
{
    public void UnLinked()
    {
        if (gameObject.tag == "Function") return;

        Command thisCommand = GetComponent<Command>();
        Command parentCommand = thisCommand.parentCommand;
        Command prevCommand = thisCommand.prevLinkedCommand;

        // reset parent in scene
        thisCommand.transform.parent = null;

        //shift up by size of this command
        int removeCommandSize = thisCommand.GetSizeCommands();
        ShiftCommandUp(parentCommand, removeCommandSize);

        if (prevCommand != null) // not first command in bracket -> unlink prev, this, parent
        {
            prevCommand.SetNextLinkedCommand(null);
            thisCommand.SetPrevLinkedCommand(null);
            thisCommand.SetParentCommand(null);
        }
        else if (prevCommand == null && parentCommand != null) // first command in bracket -> unlink parent
        {
            if (parentCommand.tag == "Loop")
            {
                Loop parentLoop = parentCommand.GetComponent<Loop>();
                parentLoop.SetLinkedLoopCommand(null);
                thisCommand.SetParentCommand(null);
            }
            else if (parentCommand.tag == "Function")
            {
                Function parentFunction = parentCommand.GetComponent<Function>();
                parentFunction.SetLinkedFunctionCommand(null);
                thisCommand.SetParentCommand(null);
            }
        }
    }
    private void ShiftCommandUp(Command command, int commandsSize)
    {
        while(command != null)
        {
            if(command.nextLinkedCommand != null)
            {
                command.nextLinkedCommand.transform.position += new Vector3(0, commandsSize * 1.05f, 0);
            }
            command = command.parentCommand;
        }
    }
}
