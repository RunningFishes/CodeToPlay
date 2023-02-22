using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTransform : MonoBehaviour
{
    public Color mouseOverColor;
    
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool dragging = false;
    private float distance;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        GetComponent<SetLinked>().isActive = false;
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        if (!dragging)
        {
            GetComponent<SetLinked>().isActive = false;
            Destroy(rb);
        }
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void OnMouseUp()
    {
        dragging = false;
        GetComponent<SetLinked>().isActive = true;
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}
