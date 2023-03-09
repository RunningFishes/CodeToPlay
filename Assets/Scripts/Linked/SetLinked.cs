using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLinked : MonoBehaviour
{
    public void Linked()
    {
        Collider2D closestCollider = GetClosestCollider();
        if (closestCollider == null) return;

        GameObject mainObjects = closestCollider.gameObject;
        GameObject linkedObjects = gameObject;
        
        Linked(mainObjects, linkedObjects);
    }
    public void Linked(GameObject mainObjects, GameObject linkedObjects)
    {
        Command mainObjectsCommand = mainObjects.GetComponent<Command>();
        Command linkedObjectsCommand = linkedObjects.GetComponent<Command>();
 
        // function can't be linked as next command
        if (linkedObjects.tag == "Function") return;

        // set parent in scene
        linkedObjects.transform.parent = mainObjects.transform;

        // Shift below command down by add command size
        int addCommandSize = linkedObjectsCommand.GetSizeCommands();
        ShiftCommandDown(mainObjectsCommand.parentCommand, addCommandSize);

        if (mainObjects.tag == "Command")
        {
            linkedObjectsCommand.SetParentCommand(mainObjectsCommand.parentCommand);

            // insert command
            if (mainObjectsCommand.nextLinkedCommand != null)
                ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsCommand.nextLinkedCommand);

            ConnectCommand(mainObjectsCommand, linkedObjectsCommand);
        }
        else if (mainObjects.tag == "Loop")
        {
            Loop mainObjectsLoop = mainObjects.GetComponent<Loop>();

            if (Mathf.Abs(mainObjects.transform.position.x - linkedObjects.transform.position.x) <= 1.0f)
            {
                linkedObjectsCommand.SetParentCommand(mainObjectsLoop.parentCommand);

                // insert command
                if (mainObjectsLoop.nextLinkedCommand != null)
                    ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsLoop.nextLinkedCommand);

                ConnectCommand(mainObjectsLoop, linkedObjectsCommand);
            }
            else
            {
                linkedObjectsCommand.SetParentCommand(mainObjectsLoop);

                // insert command
                if (mainObjectsLoop.linkedLoopCommand != null)
                    ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsLoop.linkedLoopCommand);

                mainObjectsLoop.SetLinkedLoopCommand(linkedObjectsCommand);
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

                // move all command next loop by add size command
                if (mainObjectsLoop.nextLinkedCommand != null)
                    mainObjectsLoop.nextLinkedCommand.transform.position += new Vector3(0, -addCommandSize * 1.05f, 0);
            }
        }
        else if (mainObjects.tag == "If")
        {
            If mainObjectsIf = mainObjects.GetComponent<If>();

            if (Mathf.Abs(mainObjects.transform.position.x - linkedObjects.transform.position.x) <= 1.0f)
            {
                linkedObjectsCommand.SetParentCommand(mainObjectsIf.parentCommand);

                // insert command
                if (mainObjectsIf.nextLinkedCommand != null)
                    ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsIf.nextLinkedCommand);

                ConnectCommand(mainObjectsIf, linkedObjectsCommand);
            }
            else
            {
                linkedObjectsCommand.SetParentCommand(mainObjectsIf);

                // insert command
                if (mainObjectsIf.linkedIfCommand != null)
                    ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsIf.linkedIfCommand);

                mainObjectsIf.SetLinkedIfCommand(linkedObjectsCommand);
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

                // move all command next if  by add size command
                if (mainObjectsIf.nextLinkedCommand != null)
                    mainObjectsIf.nextLinkedCommand.transform.position += new Vector3(0, -addCommandSize * 1.05f, 0);
            }
        }
        else if (mainObjects.tag == "Function")
        {
            Function mainObjectsFunction = mainObjects.GetComponent<Function>();

            linkedObjectsCommand.SetParentCommand(mainObjectsFunction);

            // insert command after function
            if (mainObjectsFunction.linkedFunctionCommand != null)
                ConnectCommand(linkedObjectsCommand.GetBottomCommand(), mainObjectsFunction.linkedFunctionCommand);

            mainObjectsFunction.SetLinkedFunctionCommand(linkedObjectsCommand);
            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

            // move all command next function by add size command
            if (mainObjectsFunction.nextLinkedCommand != null)
                mainObjectsFunction.nextLinkedCommand.transform.position += new Vector3(0, -addCommandSize * 1.05f, 0);
        }
    }
    private void ShiftCommandDown(Command command, int commandsSize)
    {
        while (command != null)
        {
            if (command.nextLinkedCommand != null)
                command.nextLinkedCommand.transform.position += new Vector3(0, -commandsSize * 1.05f, 0);
            command = command.parentCommand;
        }
    }
    private void ConnectCommand(Command prevCommand, Command nextCommand)
    {
        nextCommand.transform.parent = prevCommand.transform;

        int commands = 1;
        if (prevCommand.gameObject.tag == "Loop")
        {
            commands += prevCommand.GetComponent<Loop>().GetSizeLinkedLoopCommand();
        }
        else if(prevCommand.gameObject.tag == "If")
        {
            commands += prevCommand.GetComponent<If>().GetSizeLinkedIfCommand();
        }
        nextCommand.transform.position = new Vector3(prevCommand.transform.position.x, prevCommand.transform.position.y - commands * 1.05f, prevCommand.transform.position.z);

        prevCommand.SetNextLinkedCommand(nextCommand);
        nextCommand.SetPrevLinkedCommand(prevCommand);
    }
    private Collider2D GetClosestCollider()
    {
        Collider2D[] colliders = new Collider2D[100];
        int size = Physics2D.OverlapCollider(GetComponent<Collider2D>(), new ContactFilter2D(), colliders);

        // find closet colliders
        float distance = 1e9f;
        int idx = -1;
        for (int i = 0; i < size; i++)
        {
            if (colliders[i].gameObject.GetComponent<Command>() == null) continue;

            float newDistance = Vector2.Distance(transform.position, colliders[i].transform.position);
            if (distance > newDistance)
            {
                distance = newDistance;
                idx = i;
            }
        }
        if (idx == -1) return null;
        return colliders[idx];
    }
}
