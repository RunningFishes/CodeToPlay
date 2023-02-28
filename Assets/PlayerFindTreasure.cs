using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFindTreasure : MonoBehaviour
{
    [SerializeField]
    private float findTresureRadius;

    private void OnEnable()
    {
        EventManager.instance.onCommandComplete += OpenTreasureInRadius;
    }

    private void OnDisable()
    {
        EventManager.instance.onCommandComplete -= OpenTreasureInRadius;
    }

    
    private void OpenTreasureInRadius()
    {
        if (!CheckTreasureInRadius())
            return;

        // Open treasure
        Debug.Log("Open treasure");
    }


    private bool CheckTreasureInRadius()
    {
        GameObject[] treasures = GameObject.FindGameObjectsWithTag("Treasure");

        if (treasures == null)
            return false;

        foreach (GameObject treasure in treasures)
        {
            Debug.Log(Vector3.Distance(transform.position, treasure.transform.position));
            if (Vector3.Distance(transform.position, treasure.transform.position) < findTresureRadius)
            {
                return true;
            }
        }
        return false;
    }
}
