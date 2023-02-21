using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracket : MonoBehaviour
{
    public List<GameObject> child1;
    void Awake()
    {
        child1 = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childI = transform.GetChild(i).gameObject;
            if (childI.transform.parent == transform)
            {
                child1.Add(childI);
            }
        }
        SortListOnY();
    }

    public void SortListOnY()
    {
        for (int i = 0; i < child1.Count; i++)
        {
            for (int j = 0; j < child1.Count; j++)
            {
                if (child1[i].transform.position.y > child1[j].transform.position.y)
                {
                    GameObject temp = child1[i];
                    child1[i] = child1[j];
                    child1[j] = temp;
                }
            }
        }
    }
}
