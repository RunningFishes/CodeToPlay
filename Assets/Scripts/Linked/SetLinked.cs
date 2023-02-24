using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
                mainObjectsCommand.SetNextLinkedCommand(linkedObjectsCommand);
                linkedObjectsCommand.SetPrevLinkedCommand(mainObjectsCommand);
                linkedObjectsCommand.SetParentCommand(mainObjectsCommand.parentCommand);

                int commandInLoop = mainObjectsLoop.GetSizeLinkedLoopCommand();
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y - (commandInLoop + 1) * 1.05f, mainObjects.transform.position.z);
            }
            // loop with indent
            else
            {
                Debug.Log("loop with indent");
                mainObjectsLoop.SetLinkedLoopCommand(linkedObjectsCommand);
                linkedObjectsCommand.SetParentCommand(mainObjectsCommand);

                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);
            }

        }
        else if (mainObjects.tag == "Command")
        {
            // command
            Debug.Log("command");
            mainObjectsCommand.SetNextLinkedCommand(linkedObjectsCommand);
            linkedObjectsCommand.SetPrevLinkedCommand(mainObjectsCommand);
            linkedObjectsCommand.SetParentCommand(mainObjectsCommand.parentCommand);

            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);
        }
        else if (mainObjects.tag == "Function")
        {
            // function
            Debug.Log("function");
            mainObjectsCommand.GetComponent<Function>().SetLinkedFunctionCommand(linkedObjectsCommand);
            linkedObjectsCommand.SetParentCommand(mainObjectsCommand);

            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);
        }
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
