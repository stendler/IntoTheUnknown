using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform Player;
    public float TriggerDistance = 2f;
    public float Velocity = 2.5f;
    public enum MovementDirection
    {
        Horizontal = 0,
        Vertial = 1,
        Both = 2
    }
    public MovementDirection movementDirection = MovementDirection.Horizontal;
    public bool restrictMovementToAxis = false;
	// Use this for initialization
	void Start () {

	}

    private Vector3 getRndDirection()
    {
        switch(this.movementDirection)
        {
            case MovementDirection.Horizontal:
                return new Vector3(Random.Range(0.1f,0.5f),0f,0f);
            case MovementDirection.Vertial:
                return new Vector3(0f, Random.Range(0.1f, 0.5f),0f);
            default:
                return new Vector3(Random.Range(0.1f, 0.5f),Random.Range(0.1f,0.5f),0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (this.Player)
        {
            this.transform.LookAt(this.Player);
            if (Vector2.Distance(this.transform.position, this.Player.transform.position) <= this.TriggerDistance)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.Player.position, 1.2f * this.Velocity * Time.deltaTime);
                //Destroy(GameObject.FindGameObjectWithTag("Player"));
                // attack player
            }
            else
            {
                if (GameObject.Find("GameControl").GetComponent<InitGame>().IsNoWall((int) this.transform.position.x, (int) this.transform.position.y))
                {
                    Vector3 range = this.transform.position + getRndDirection();
                    this.transform.position = Vector2.MoveTowards(this.transform.position, range, this.Velocity / 2 * Time.deltaTime);
                }
              
            }
            this.transform.rotation = this.Player.rotation;
        }
       
	}

    void bounceBack()
    {
        this.transform.Rotate(0, -180, 0);
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.name == "Player")
        {
            GameObject.Find("GameControl").GetComponent<InitGame>().respawnPlayer();
            Destroy(this.gameObject);
        }
        else if(otherObject.gameObject.name == "ConcreteWall")
        {
            bounceBack();
        }
    }
}
