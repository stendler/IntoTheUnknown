using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {
    
	GameObject[,] stage;
	int[,] roomList;
    //GameObject objDummy = new GameObject(); //TODO delete @unused

    public GameObject wall;
	public GameObject floor;
	// Use this for initialization
    public void Awake()
    {
		newStage ();

           // dot tween
    }

	public void newStage()
	{
		int numberOfRooms = 4;
		roomList = new int[numberOfRooms, 5];
		// 0 - roomX: Breite des Raumes
		// 1 - roomY: Hoehe des Raumes
		// 2 - X Position im Stage
		// 3 - Y Position im Stage
		// 4 - Array Liste von verbundenen Raeumen

		int stageSizeXY = 100;
		stage = new GameObject[stageSizeXY,stageSizeXY]; //TODO: clearStage, wenn vorher bereits eine stage da war!!
		//int stageSizeY = 100;


		//alle Raeume erstellen und in die Raumliste eintragen
		for (int roomNr = 0; roomNr < numberOfRooms; roomNr++) {
			//neuen Raum erstellen
			int roomX = Random.Range(2,10); // Breite des Raumes
			int roomY = Random.Range(2,10); // Hoehe des Raumes
			int posX = Random.Range(1,stageSizeXY-roomX-1); //Position des Raumes auf der X-Achse in der Stage -- einbezogen die Raumgroesse
			int posY = Random.Range(1,stageSizeXY-roomY-1); //Position des Raumes auf der Y-Achse in der Stage

			roomList[roomNr,0] = roomX;
			roomList[roomNr,1] = roomY;
			roomList[roomNr,2] = posX;
			roomList[roomNr,3] = posY;

			//Raeume in die stage einfuegen
			for(int x = 0; x<roomX;x++){
				for(int y = 0; y < roomY; y++){
					//Destroy(stage[posX+x,posY+y]);
					stage[posX+x,posY+y] = Instantiate(floor, new Vector2(posX+x,posY+y), this.transform.rotation) as GameObject;
				}
			}

		}

		//Stage mit Mauern befuellen
		for (int x = 0; x < stageSizeXY; x++) {
			for (int y = 0; y < stageSizeXY; y++) {
				if(stage[x,y] == null){
					stage[x,y] = Instantiate(wall, new Vector2(x, y), this.transform.rotation) as GameObject;		
				}
			}
		}
	//TODO: Raeume (virtuell) verbinden



	}

    void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
