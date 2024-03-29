﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public Sprite leftSprite, rightSprite, upSprite;

    private Sprite downSprite;
    private SpriteRenderer sprRenderer;

	public InitGame initGame;

	// Use this for initialization
	void Start () {
        this.sprRenderer = GetComponent<SpriteRenderer>();
        this.downSprite = this.sprRenderer.sprite;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.sprRenderer.sprite = this.downSprite;
			if(initGame.IsNoWall((int)this.transform.position.x,(int)this.transform.position.y-1)){
          	  this.transform.Translate(new Vector3(0f, -1f));
			}
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.sprRenderer.sprite = this.upSprite;
			if(initGame.IsNoWall((int)this.transform.position.x,(int)this.transform.position.y+1)){
            	this.transform.Translate(new Vector3(0f, 1f));
			}
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.sprRenderer.sprite = this.leftSprite;
			if(initGame.IsNoWall((int)this.transform.position.x-1,(int)this.transform.position.y)){
      	     	 this.transform.Translate(new Vector3(-1f, 0f));
			}
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.sprRenderer.sprite = this.rightSprite;
			if(initGame.IsNoWall((int)this.transform.position.x+1,(int)this.transform.position.y)){
           		 this.transform.Translate(new Vector3(1f, 0f));
			}
        }
	}

    public void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Item")
        {
            Destroy(otherObj.gameObject);
            InitGame.highscore += 10;
            GameObject.Find("GameControl").GetComponent<InitGame>().amountDiamonds -= 1;
        }
        else if (otherObj.gameObject.tag == "Enemy")
        {
            Destroy(otherObj.gameObject);
            GameObject.Find("GameControl").GetComponent<InitGame>().lifePoints -= 20;
        }
        else if (otherObj.gameObject.name == "Stairs")
        {
            InitGame.Level++;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
