using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool instance;
    [SerializeField]
    private GameObject linePrefab;
    private Queue<GameObject> linePool = new Queue<GameObject>();
    private int poolSize;
    private Board board;
    // Start is called before the first frame update
    
    void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    void Start()
    {


        board = FindObjectOfType<Board>();
        poolSize = board.width * board.height;

        for(int i = 0; i < poolSize; i++){
            GameObject line = Instantiate(linePrefab);
            line.transform.parent = this.transform;

            linePool.Enqueue(line);

            line.SetActive(false);

        }

        

    }
    public GameObject Get(){
        if (linePool.Count > 0){
            GameObject line = linePool.Dequeue();
            line.SetActive(true);
            return line;

        }

        else{
            return null;
        }
    }


    public void Return(GameObject line){
        linePool.Enqueue(line);
        line.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
