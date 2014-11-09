using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    public Transform trackingObject;
    public float trackingSpeed = 3;
    public static Vector3 relativeDelta = new Vector3(0, 2.5f, 0);
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        if (this.trackingObject)
        {
            Vector3 destination = this.trackingObject.position + CameraTracking.relativeDelta;
            
            destination.z = this.transform.position.z;

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                destination, this.trackingSpeed * Time.deltaTime);
        }
    }
}
