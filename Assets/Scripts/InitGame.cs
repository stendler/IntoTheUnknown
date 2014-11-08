using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {
    
    GameObject[,] arrayField = new GameObject[64,64];
    GameObject objDummy = new GameObject();

    public GameObject brickWall;
	// Use this for initialization
    public void Awake()
    {
           // dot tween
        for (int i = 0; i < 64; i++){
            for (int j = 0; j < 64; j++){
                if (Random.Range(0, 2) == 1)
                {
                    arrayField[i, j] = Instantiate(brickWall, new Vector2(i, j), this.transform.rotation) as GameObject;
                }
                else
                {
                    arrayField[i, j] = objDummy;
                }
                
            }
        }
    }
    void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
