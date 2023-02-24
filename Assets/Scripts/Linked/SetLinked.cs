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
                if (mainObjectsLoop.linkedLoopCommand != null)
                {
                    Command oldLinkedLoopCommand = mainObjectsLoop.linkedLoopCommand;
                    Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();

                    ConnectCommand(lastLinkedObjectCommand, oldLinkedLoopCommand);
                }
                mainObjectsLoop.SetLinkedLoopCommand(linkedObjectsCommand);
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

                if(mainObjectsLoop.nextLinkedCommand != null)
                {
                    int commands = mainObjectsLoop.GetSizeCommands();
                    mainObjectsLoop.nextLinkedCommand.transform.position = new Vector3(mainObjectsLoop.transform.position.x, mainObjectsLoop.transform.position.y - commands * 1.05f, mainObjectsLoop.transform.position.z);
                }
            }
        }
        else if (mainObjects.tag == "Command")
        {
            // command
            Debug.Log("command");
            linkedObjectsCommand.SetParentCommand(mainObjectsCommand.parentCommand);
            if(mainObjectsCommand.nextLinkedCommand != null)
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
            if (mainObjectsFunction.linkedFunctionCommand != null)
            {
                Command oldLinkedLoopCommand = mainObjectsFunction.linkedFunctionCommand;
                Command lastLinkedObjectCommand = linkedObjectsCommand.GetBottomCommand();

                ConnectCommand(lastLinkedObjectCommand, oldLinkedLoopCommand);
            }
            mainObjectsFunction.SetLinkedFunctionCommand(linkedObjectsCommand);
            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);

            if (mainObjectsFunction.nextLinkedCommand != null)
            {
                int commands = mainObjectsFunction.GetSizeCommands();
                mainObjectsFunction.nextLinkedCommand.transform.position = new Vector3(mainObjectsFunction.transform.position.x, mainObjectsFunction.transform.position.y - commands * 1.05f, mainObjectsFunction.transform.position.z);
            }
        }
    }

    private void ConnectCommand(Command command1, Command command2)
    {
        command2.transform.parent = command1.transform;

        int commands = 1;
        if(command1.gameObject.tag == "Loop")
        {
            commands += command1.GetComponent<Loop>().GetSizeLinkedLoopCommand();
        }
        command2.transform.position = new Vector3(command1.transform.position.x, command1.transform.position.y - commands * 1.05f, command1.transform.position.y);

        command1.SetNextLinkedCommand(command2);
        command2.SetPrevLinkedCommand(command1);
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
