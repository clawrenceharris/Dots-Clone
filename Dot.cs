using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private Board board;
    public int row;
    public int column;
    private int targetX;
    private int targetY;
    public SpriteRenderer sprite;
    public Color color;

    
    void Start()
    {
        board = FindObjectOfType<Board>();
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    void Update()
    {
       
        targetX = column;
        targetY = row;

        if(Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //move towards targetY position
            Vector2 tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .1f);
        }
        else
        {
            //Directly set the position 
            Vector2 tempPosition = new Vector2( transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;

        }
        

    }

    public bool IsValidNeighbor(Dot dot){
        
        int rowDiff = Mathf.Abs(dot.row - this.row);
        int colDiff = Mathf.Abs(dot.column - this.column);
        if (this.gameObject.tag != dot.gameObject.tag){
            return false;
        }

        else if( colDiff > 1 || rowDiff > 1){
            return false;
        }

        else if(colDiff > 0 && rowDiff > 0){
            return false;
        }
        
        return true;
 
    }
  
 
}
