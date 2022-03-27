using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public List<Dot> connections = new List<Dot>();
    List<Dot> dotsToClear = new List<Dot>();
    private bool isSquare;
    private Board board;


    void Start()
    {
        DotTouchIO.onDotSelected += OnDotSelected;
        DotTouchIO.onSelectionEnded += OnSelectionEnded;
        board = GetComponent<Board>();


    }


    private void OnDotSelected(Dot dot){

        bool isValid = false;
        
        
        if (connections.Count == 0)
        {
            isSquare = false;
            isValid = true;
        }
        
        
        //If the dot is a valid neighbor,and we connected more than 3 dots, its a square 
        else if(connections[connections.Count - 1].IsValidNeighbor(dot) 
                //if it's already in our connections 
                && connections.Contains(dot) 
                //if we connected more than 3 dots
                && connections.Count > 3 
                //if we are not on the second to last connected dot
                && connections[connections.Count - 2] != dot 
                //if we are not selecting the last dot again
                &&  connections[connections.Count - 1] != dot)
        {
            isSquare = true;
            isValid = true;
            
        }



        //if selected a dot we already on, or the dot before it 
        else if(connections[connections.Count - 1] == dot ||
                    connections[Mathf.Clamp(connections.Count - 2, 0, int.MaxValue)] == dot)
        {
            //undo the last connection
            connections.RemoveAt(connections.Count -1);

        }

        else
        {
            //the dot is right next to this dot, is the same color and is not this dot 
            isValid = connections[connections.Count - 1].IsValidNeighbor(dot) && connections[connections.Count - 1] != dot;
        }
        
        
        if(isValid)
        {
            connections.Add(dot);
                    
        }

        if(isSquare){
            HandleSquare(dot);
        }   
    }


    //adds all like colored dots to a list to cleared later
    private void HandleSquare(Dot dot){
        for(int i = 0; i < board.width; i++){
            for(int j = 0; j < board.height; j++){
                if(board.allDots[i,j] != null){
                    if(dot.gameObject.tag == board.allDots[i,j].tag){
                        dotsToClear.Add(board.allDots[i,j].GetComponent<Dot>());
                    }
                }
            }
        }
    }


    private void ClearSquare(){
        foreach(Dot dot in dotsToClear){
            Destroy(dot.gameObject);
            board.allDots[dot.column, dot.row] = null;

        }
    }



    private void OnSelectionEnded()
    {
        if(connections.Count > 1)
        {
            foreach(Dot dot in connections)
            {
                Destroy(dot.gameObject);
                board.allDots[dot.column, dot.row] = null;
            }
        }

        if(isSquare){
            ClearSquare();

        }

        StartCoroutine(board.DecreaseRowCo());
        connections.Clear();
        dotsToClear.Clear();
    }

    
}
