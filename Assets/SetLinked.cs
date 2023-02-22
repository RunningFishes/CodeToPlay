using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLinked : MonoBehaviour
{
    public void Linked()
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
        if (idx == -1) return;

        GameObject mainObjects = colliders[idx].gameObject;
        GameObject linkedObjects = gameObject;
        if (mainObjects.tag == "Loop")
        {
            // loop no indent
            if (Mathf.Abs(mainObjects.transform.position.x - linkedObjects.transform.position.x) <= 1.0f)
            {
                Debug.Log("loop no indent");
                mainObjects.GetComponent<Command>().SetNextLinkedCommand(linkedObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetPrevLinkedCommand(mainObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>().parentCommand);

                int commandInLoop = mainObjects.GetComponent<Loop>().GetSizeLinkedLoopCommand();
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y - (commandInLoop + 1) * 1.05f, mainObjects.transform.position.z);
            }
            // loop with indent
            else
            {
                Debug.Log("loop with indent");
                mainObjects.GetComponent<Loop>().SetLinkedLoopCommand(linkedObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>());

                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x + 1, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);
            }

        }
        if (mainObjects.tag == "Command")
        {
            // command no indent
            Debug.Log("command no indent");
            mainObjects.GetComponent<Command>().SetNextLinkedCommand(linkedObjects.GetComponent<Command>());
            linkedObjects.GetComponent<Command>().SetPrevLinkedCommand(mainObjects.GetComponent<Command>());
            linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>().parentCommand);

            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y - 1.05f, mainObjects.transform.position.z);
        }
    }
}
