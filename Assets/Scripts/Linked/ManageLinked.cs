using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLinked : MonoBehaviour
{
    public void UpdateLinked()
    {
        if (gameObject.tag == "Main") return;

        Command thisCommand = GetComponent<Command>();
        Command parentCommand = thisCommand.parentCommand;
        Command prevCommand = thisCommand.prevLinkedCommand;

        if (parentCommand == null) return;
        if (prevCommand != null) // another objects
        {
            prevCommand.SetNextLinkedCommand(null);
            thisCommand.SetPrevLinkedCommand(null);
            thisCommand.SetParentCommand(null);
        }
        else if (prevCommand == null) // first objects in bracket
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
}
