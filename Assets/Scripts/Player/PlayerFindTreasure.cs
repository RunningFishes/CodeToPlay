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
        GameObject treasure = CheckTreasureInRadius();
        if (treasure == null)
            return;

        // Open treasure
        treasure.GetComponent<Treasure>().Open();
    }


    private GameObject CheckTreasureInRadius()
    {
        GameObject[] treasures = GameObject.FindGameObjectsWithTag("Treasure");
  
        if (treasures.Length == 0)
            return null;

        foreach (GameObject treasure in treasures)
        {
            if (Vector3.Distance(transform.position, treasure.transform.position) < findTresureRadius)
            {
                return treasure;
            }
        }
        return null;
    }
}
