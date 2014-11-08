using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public Sprite leftSprite, rightSprite, upSprite;

    private Sprite downSprite;
    private SpriteRenderer sprRenderer;

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
            this.transform.Translate(new Vector3(0f, -1f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.sprRenderer.sprite = this.upSprite;
            this.transform.Translate(new Vector3(0f, 1f));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.sprRenderer.sprite = this.leftSprite;
            this.transform.Translate(new Vector3(-1f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.sprRenderer.sprite = this.rightSprite;
            this.transform.Translate(new Vector3(1f, 0f));
        }
        if (Input.anyKey)
        {
            foreach (GameObject objEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
               
            }
        }
	}
}
