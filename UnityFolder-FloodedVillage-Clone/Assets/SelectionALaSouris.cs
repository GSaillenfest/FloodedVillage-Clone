using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectionALaSouris : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ClickSelection();
    }

    void ClickSelection()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mousePos.origin, mousePos.direction * 100, Color.yellow, 2f) ;
        RaycastHit2D hit = Physics2D.GetRayIntersection(mousePos);
        if (hit.collider != null) Debug.Log(hit.collider.gameObject.name);
    }

    
}
