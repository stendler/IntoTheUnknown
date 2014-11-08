using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Transform objPlayer;
    public float TriggerDistance = 5;
    public float Velocity = 2.5f;
	// Use this for initialization
	void Start () {
        this.objPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(this.objPlayer);
        if (Vector2.Distance(this.transform.position, this.objPlayer.transform.position) <= this.TriggerDistance)
        {
            Debug.Log("Player has been seen");
            // attack player
        }
        else
        {
            this.transform.position += this.transform.forward * this.Velocity * Time.deltaTime;
        }
	}
}
