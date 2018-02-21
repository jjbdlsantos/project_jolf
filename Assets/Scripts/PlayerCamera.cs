using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    Transform target;
    GameObject player;
    GameObject aCam;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        target = player.transform;
        aCam = GameObject.Find("Player Camera");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cam = new Vector3(0, 0, 0);
        cam = new Vector3(target.position.x, target.position.y, target.position.z + 15f);
        aCam.transform.position = cam;
    }
}
