using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLinked : MonoBehaviour
{
    public bool isActive = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActive) return;

        GameObject mainObjects = collision.gameObject;
        GameObject linkedObjects = gameObject;

        if (mainObjects.tag == "Loop")
        {
            // loop no indent
            if (Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x) <= 1.0f)
            {
                mainObjects.GetComponent<Loop>().SetNextLinkedCommand(linkedObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetPrevLinkedCommand(mainObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>().parentCommand);
                
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y-1.05f, mainObjects.transform.position.z);
            }
            // loop with indent
            else
            {
                mainObjects.GetComponent<Loop>().SetLinkedLoopCommand(linkedObjects.GetComponent<Command>());
                linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>());
                
                linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x+1, mainObjects.transform.position.y-1.05f, mainObjects.transform.position.z);
            }
            Destroy(linkedObjects.GetComponent<Rigidbody2D>());

        }
        if (mainObjects.tag == "Command")
        {
            // command no indent
            mainObjects.GetComponent<Command>().SetNextLinkedCommand(linkedObjects.GetComponent<Command>());
            linkedObjects.GetComponent<Command>().SetPrevLinkedCommand(mainObjects.GetComponent<Command>());
            linkedObjects.GetComponent<Command>().SetParentCommand(mainObjects.GetComponent<Command>().parentCommand);

            linkedObjects.transform.position = new Vector3(mainObjects.transform.position.x, mainObjects.transform.position.y-1.05f, mainObjects.transform.position.z);
            Destroy(linkedObjects.GetComponent<Rigidbody2D>());
        }
    }
}
