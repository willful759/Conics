using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    // Start is called before the first frame update
    public Color color;
    public Color mouseOverColor;

    float distance;
    bool dragging = false;
 
   
    void OnMouseEnter()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = mouseOverColor;
    }
 
    void OnMouseExit()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        if (! dragging ) {
          sRenderer.color = color;
        }
    }
 
    void OnMouseDown()
    {
        dragging = true;
    }
 
    void OnMouseUp()
    {
        dragging = false;
    }
 
    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }}
