using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {
    
	protected GameObject[,] stage;

	int[,] roomList;
	int[,] floorTilesList;

    public GameObject wall;
	public GameObject floor;
    public GameObject enemy;
    protected GameObject spawnPoint;

    protected GameObject[] arrayEnemies;
    int stageSizeXY = 64;

    public GameObject player, objDiamond;
    public GameObject objStairs;
    public static int highscore;
    public static int Level = 1;
    public int amountDiamonds = 4;
    public int lifePoints = 100;
    public Texture gameOverOverlay;
    public bool gameOver = false;
	// Use this for initialization
    public void Awake()
    {
		newStage ();
		Start ();
           // dot tween
    }
    public void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 120, 0, 100, 40), "Your score: " + highscore);
        GUI.Box(new Rect(0, 0, 120, 40), "Your health: " + lifePoints);
        if (this.lifePoints <= 0)
        {
            gameOver = false;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"");
            GUI.TextField(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 50, 100, 50), "GAME OVER!");
        }
    }
	public bool IsNoWall(int x, int y)
	{
		return (stage[x,y].tag == "Ground");
	}

	public void newStage()
	{
		int numberOfRooms = 5;
		roomList = new int[numberOfRooms, 6];
		// 0 - roomX: Breite des Raumes
		// 1 - roomY: Hoehe des Raumes
		// 2 - X Position im Stage
		// 3 - Y Position im Stage
		// 4 - verbundener Raum 1
		// 5 - verbundener Raum 2

		stage = new GameObject[stageSizeXY,stageSizeXY]; //TODO: clearStage, wenn vorher bereits eine stage da war!!
		//int stageSizeY = 100;


		//alle Raeume erstellen und in die Raumliste eintragen
		for (int roomNr = 0; roomNr < numberOfRooms; roomNr++) {
			//neuen Raum erstellen
			int roomX = Random.Range(16,20); // Breite des Raumes
			int roomY = Random.Range(16,20); // Hoehe des Raumes
			int posX = Random.Range(2,stageSizeXY-roomX-1); //Position des Raumes auf der X-Achse in der Stage -- einbezogen die Raumgroesse
			int posY = Random.Range(2,stageSizeXY-roomY-1); //Position des Raumes auf der Y-Achse in der Stage

			roomList[roomNr,0] = roomX;
			roomList[roomNr,1] = roomY;
			roomList[roomNr,2] = posX;
			roomList[roomNr,3] = posY;

			//Raeume in die stage einfuegen
			for(int x = 0; x<roomX;x++){
				for(int y = 0; y < roomY; y++){
					//Destroy(stage[posX+x,posY+y]);
					stage[posX+x,posY+y] = Instantiate(floor, new Vector2(posX+x,posY+y), this.transform.rotation) as GameObject;
                    stage[posX + x, posY + y].tag = "Ground";
				}
			}

		}

		
		//TODO: Raeume (virtuell) verbinden
		foreach(int room in roomList){
			if (roomList[room,4] == null){
				int rndRoom = Random.Range(0,numberOfRooms-1);
				roomList[room,4] = rndRoom;
			}
			if (roomList[room,5] == null){
				int rndRoom = Random.Range(0,numberOfRooms-1);
				roomList[room,5] = rndRoom;
			}
		}

		//kuerzester Weg zwischen den verbundenen Raeumen



		//floors einfuegen

		//Stage mit Mauern befuellen
		for (int x = 0; x < stageSizeXY; x++) {
			for (int y = 0; y < stageSizeXY; y++) {
				if(stage[x,y] == null){
					stage[x,y] = Instantiate(wall, new Vector2(x, y), this.transform.rotation) as GameObject;		
				}
			}
		}

	//TODO: Raeume (virtuell) verbinden

		
		// TODO: array mit liste der floors
		//menge der floor tiles im stage array zaehlen
		//floorTilesList = new int[menge,2]
		//befuellen-> floorTilesList[numInList,0 -> x 1-> y]
		
		//Skellet spawnen in random von flooTilesList

	}
    public void respawnPlayer()
    {
       // Instantiate(this.player,spawnPoint.transform.position,Quaternion.identity);
        this.player.transform.position = spawnPoint.transform.position;
        refocusCamera(false);
    }
    void Start()
    {
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground");
        spawnPoint = groundTiles[Random.Range(1, (int) groundTiles.Length)];
        spawnPoint.GetComponent<SpawnControl>().isPlayerSpawnPoint = true;

        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint.transform.position;
        refocusCamera(true);
        createEnemies();
        amountDiamonds = 4;
        placeElements();
	}
    void placeElements()
    {
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground");

        for (int i = 1; i <= this.amountDiamonds; i++)
        {
            int j = Random.Range(1, (int)groundTiles.Length);
            Instantiate(this.objDiamond, groundTiles[j].transform.position, Quaternion.identity);
           
        }
            
       
    }
    void refocusCamera(bool bStart)
    {
        Transform transCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        if (bStart)
        {
            transCamera.position = spawnPoint.transform.position + CameraTracking.relativeDelta + new Vector3(0f, 0f, -1f);
        }
        else
        {
            transCamera.position = Vector3.Lerp(transCamera.position, spawnPoint.transform.position + CameraTracking.relativeDelta + new Vector3(0f, 0f, -1f),
            10 * Time.deltaTime);
        }
    }
    public void createEnemies()
    {
        this.arrayEnemies = new GameObject[15 + (2 * InitGame.Level)];
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground");
        for (int i = 0; i < this.arrayEnemies.Length; i++)
        {
            int rndIndex = Random.Range(0, groundTiles.Length);
            if (!groundTiles[rndIndex].GetComponent<SpawnControl>().isPlayerSpawnPoint)
            {
                this.enemy.GetComponent<Enemy>().movementDirection = (Enemy.MovementDirection) Random.Range(0, 2);
                Instantiate(this.enemy,groundTiles[rndIndex].transform.position,this.transform.rotation);
            }
        }
           
    }
	// Update is called once per frame
	void Update () {
        if (this.amountDiamonds <= 0)
        {
            GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground");
            objStairs.transform.position = groundTiles[Random.Range(1, (int)groundTiles.Length)].transform.position;
            this.amountDiamonds += 1;
        }
	}
}
