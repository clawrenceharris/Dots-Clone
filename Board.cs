using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] dots;
    public GameObject[,] allDots;
    public int offset;

    void Start()
    {
        allDots = new GameObject[width, height];
        
        SetUp();
    }

    
    void SetUp()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // offset applied so dots can slide from top of frame to their assigned row
                Vector2 position = new Vector2(i, j+ offset);
                
                int dotToUse = Random.Range(0, dots.Length);
                GameObject dot = Instantiate(dots[dotToUse], position, Quaternion.identity) as GameObject;
                
                dot.transform.parent = this.transform;
                dot.name = "("+i +", "+j+")";
     
                dot.GetComponent<Dot>().column = i;
                dot.GetComponent<Dot>().row = j;
                
                allDots[i,j] = dot;

                
            }
        }
    }

    
    public IEnumerator DecreaseRowCo()
    {

        int nullCount = 0;
        for(int i = 0; i < width; i ++)
        {
            for(int j = 0; j < height; j++)
            {
                if (allDots[i,j] == null)
                {
                    nullCount++;
                }
                else if (nullCount  > 0)
                {

                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i,j] = null;
                }
            }
            nullCount = 0;
        }
         yield return new WaitForSeconds(.5f);
         RefillBoard();
    }



    public void RefillBoard()
    {
        
        for(int i = 0; i < width; i ++)
        {
            for(int j = 0; j < height; j++)
            {
                if(allDots[i,j] == null)
                {
                    int dotToUse = Random.Range(0, dots.Length);
                    Vector2 position = new Vector2(i, j + height);
                    GameObject dot = Instantiate(dots[dotToUse], position, Quaternion.identity) as GameObject;
                    
                    dot.transform.parent = this.transform;
                    dot.name = "("+i +", "+j+")";

                    dot.GetComponent<Dot>().column = i;
                    dot.GetComponent<Dot>().row = j;
                    
                    allDots[i,j] = dot;
                    Debug.Log(i +", "+j);

                }
            }
        }
    }

    
}
