using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotTouchIO : MonoBehaviour
{
    private Dot dot;
    
    public delegate void OnSelectionStarted();
    public static event OnSelectionStarted onSelectionStarted;

    public delegate void OnDotSelected(Dot dot);
    public static event OnDotSelected onDotSelected;

    public delegate void OnSelectionEnded();
    public static event OnSelectionEnded onSelectionEnded;

    
    private static bool hasSelection;


    void Start()
    {
       dot = GetComponent<Dot>();
       
    }

    

    void OnMouseUp(){
        if(hasSelection){
            hasSelection = false;

            onSelectionEnded?.Invoke();
           
        }
    }

    void OnMouseDown(){
        if(!hasSelection){
            hasSelection = true;
        
            onSelectionStarted?.Invoke();
            OnMouseEnter();
        }
    }

    void OnMouseEnter(){
        if (hasSelection){
            onDotSelected?.Invoke(dot);

            
        }
    }

   

}
