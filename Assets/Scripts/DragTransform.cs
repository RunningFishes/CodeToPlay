using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragTransform : MonoBehaviour
{
    public Color mouseOverColor;
    
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool dragging = false;
    private float distance;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        distance = Vector2.Distance(transform.position, Camera.main.transform.position);
        dragging = true;

        // make on top
        transform.position += new Vector3(0, 0, -1);
    }

    void OnMouseUp()
    {
        dragging = false;
        GetComponent<ManageLinked>().UnLinked();
        GetComponent<SetLinked>().Linked();

        // make default
        transform.position += new Vector3(0, 0, 1);
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, transform.position.z);
        }
    }
}
