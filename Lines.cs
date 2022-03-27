using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    private Board board;
    private List<LineRenderer> activeLines = new List<LineRenderer>();
    private ConnectionManager connectionManager;

    void Start()
    {
        board = FindObjectOfType<Board>();
        connectionManager = FindObjectOfType<ConnectionManager>();
    }
    

    void Update()
    {
        var connections = connectionManager.connections;
        while( activeLines.Count < connections.Count){

            GameObject connector = Pool.instance.Get();
            activeLines.Add(connector.GetComponent<LineRenderer>());

        }

        while (activeLines.Count > connections.Count){
            Pool.instance.Return(activeLines[0].gameObject);
            activeLines.RemoveAt(0);

        }

        if(connections.Count > 0){

            DrawLines(connections);

        }

    }


    private void DrawLines(List<Dot> connections)
    {
        
        LineRenderer line = null;
        int start = 0;
        
        for (int i = 0; i < connections.Count; i++)
        {
            
             line = activeLines[i];
             line.color = connections[0].color;
             line.startPos = connections[start].transform.position;

              // If we're not at the last connection
            if (i != connections.Count - 1){
                 // Set second position to the next connection
                line.endPos = connections[i + 1].transform.position;
                
                //set the index for the start position of the next line
                start = i + 1;

            }    
        }
       
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        // Set the last line to draw it's final point to the mouse
        line.endPos = mousePos;
    }

    

}
