using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderer : MonoBehaviour
{
    private Vector3 initialScale;
    private SpriteRenderer sprite;
    public Color color;
    public Vector3 startPos;
    public Vector3 endPos;

    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(0.2f, 0.2f, 0);
        initialScale = transform.localScale;


    }

    void Update()
    {
        sprite.color = color;
        
        SetPosition();
    }

    
    
    
    
    public void SetPosition(){

        float distance = Vector2.Distance(startPos, endPos);
        transform.localScale = new Vector3(initialScale.x, distance);
        
        Vector3 middlePoint = (startPos + endPos) /2f;
        transform.position = middlePoint;
        
        float angle = -Mathf.Atan2(endPos.x - startPos.x, endPos.y - startPos.y ) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);      
            


        
    }
            


        
    
    
}
