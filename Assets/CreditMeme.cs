using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMeme : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
}
