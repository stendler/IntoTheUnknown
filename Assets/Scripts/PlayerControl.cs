using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector3(0f, -1f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector3(0f, 1f));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.Translate(new Vector3(-1f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.Translate(new Vector3(1f, 0f));
        }

	}
}
