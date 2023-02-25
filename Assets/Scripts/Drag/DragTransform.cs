using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragTransform : MonoBehaviour
{
    public Color mouseOverColor;
    
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private bool dragging = false;
    private float distance;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        if (Core.instance.isGameRunning()) return;
        spriteRenderer.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        if (Core.instance.isGameRunning()) return;
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        if (DragCamera.instance != null)
            DragCamera.instance.isDragSomething = true;
        
        if (Core.instance.isGameRunning()) return;
        distance = Vector2.Distance(transform.position, Camera.main.transform.position);
        dragging = true;

        // make on top
        transform.position += new Vector3(0, 0, -1);
    }

    void OnMouseUp()
    {
        if (DragCamera.instance != null)
            DragCamera.instance.isDragSomething = false;
        
        if (Core.instance.isGameRunning()) return;
        dragging = false;
        GetComponent<ManageLinked>().UnLinked();
        GetComponent<SetLinked>().Linked();

        // make default
        transform.position += new Vector3(0, 0, 1);
    }

    void Update()
    {
        if (Core.instance.isGameRunning()) return;
        
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, transform.position.z);
        }
    }
}
