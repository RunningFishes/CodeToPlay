using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SetLinked : MonoBehaviour
{
    public void Linked()
    {
        Collider2D closestCollider = GetClosestCollider();

        if (closestCollider == null) return;

        GameObject mainObjects = closestCollider.gameObject;
        GameObject linkedObjects = gameObject;

        Command mainObjectsCommand = mainObjects.GetComponent<Command>();
        Command linkedObjectsCommand = linkedObjects.GetComponent<Command>();

        if (linkedObjects.tag == "Function") return;

        // set parent in scene
        linkedObjectsCommand.transform.parent = mainObjects.transform;

        if (mainObjects.tag == "Loop")
        {
            Loop mainObjectsLoop = mainObjects.GetComponent<Loop>();
            // loop no indent
            if (Mathf.Abs(mainObjects.transform.position.x - linkedObjects.transform.position.x) <= 1.0f)
            {
                Debug.Log("loop no indent");
                linkedObjectsCommand.SetParentCommand(mainObjectsLoop.parentCommand);

                // insert command after loop
                if (mainObjectsLoop.nextLinkedCommand != null)
                {
                    Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();
                    ConnectCommand(lastLinkedObjectCommand, mainObjectsLoop.nextLinkedCommand);
                }
                ConnectCommand(mainObjectsLoop, linkedObjectsCommand);
            }
            // loop with indent
            else
            {
                Debug.Log("loop with indent");
                linkedObjectsCommand.SetParentCommand(mainObjectsLoop);

                // insert command after loop
                if (mainObjectsLoop.linkedLoopCommand != null)
                {
                    Command oldLinkedLoopCommand = mainObjectsLoop.linkedLoopCommand;
                    Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();

                    ConnectCommand(lastLinkedObjectCommand, oldLinkedLoopCommand);
                }
                mainObjectsLoop.SetLinkedLoopCommand(linkedObjectsCommand);
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

                // move all command after loop depend on size of added command in loop
                if (mainObjectsLoop.nextLinkedCommand != null)
                {
                    int commands = mainObjectsLoop.linkedLoopCommand.GetSizeCommands() + 1;
                    mainObjectsLoop.nextLinkedCommand.transform.position = new Vector3(mainObjectsLoop.transform.position.x, mainObjectsLoop.transform.position.y - commands * 1.05f, mainObjectsLoop.transform.position.z);
                }
            }
        }
        else if (mainObjects.tag == "Command")
        {
            // command
            Debug.Log("command");
            linkedObjectsCommand.SetParentCommand(mainObjectsCommand.parentCommand);

            // insert command after command
            if (mainObjectsCommand.nextLinkedCommand != null)
            {
                Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();
                ConnectCommand(lastLinkedObjectCommand, mainObjectsCommand.nextLinkedCommand);
            }
            ConnectCommand(mainObjectsCommand, linkedObjectsCommand);
        }
        else if (mainObjects.tag == "Function")
        {
            // function
            Debug.Log("function");
            Function mainObjectsFunction = mainObjects.GetComponent<Function>();

            linkedObjectsCommand.SetParentCommand(mainObjectsFunction);

            // insert command after function
            if (mainObjectsFunction.linkedFunctionCommand != null)
            {
                Command oldLinkedFunctionCommand = mainObjectsFunction.linkedFunctionCommand;
                Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();

                ConnectCommand(lastLinkedObjectCommand, oldLinkedFunctionCommand);
            }
            mainObjectsFunction.SetLinkedFunctionCommand(linkedObjectsCommand);
            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

            // move all command after function depend on size of added command in function
            if (mainObjectsFunction.nextLinkedCommand != null)
            {
                int commands = mainObjectsFunction.linkedFunctionCommand.GetSizeCommands() + 1;
                mainObjectsFunction.nextLinkedCommand.transform.position = new Vector3(mainObjectsFunction.transform.position.x, mainObjectsFunction.transform.position.y - commands * 1.05f, mainObjectsFunction.transform.position.z);
            }
        }
    }

    private void ConnectCommand(Command prevCommand, Command nextCommand)
    {
        nextCommand.transform.parent = prevCommand.transform;

        int commands = 1;
        if(prevCommand.gameObject.tag == "Loop")
        {
            commands += prevCommand.GetComponent<Loop>().GetSizeLinkedLoopCommand();
        }
        nextCommand.transform.position = new Vector3(prevCommand.transform.position.x, prevCommand.transform.position.y - commands * 1.05f, prevCommand.transform.position.y);

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
