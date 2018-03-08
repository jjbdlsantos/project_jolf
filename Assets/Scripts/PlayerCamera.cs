using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Transform target;
    private GameObject player;
    private GameObject aCam;
    private PlayerProperties properties;
    private string playerID;

	void Start () {
        properties = this.gameObject.transform.parent.gameObject.GetComponent<PlayerProperties>();
        playerID = properties.playerID;
        player = GameObject.Find(playerID + "Body");
        target = player.transform;
        aCam = GameObject.Find(playerID + "Cam");

        Camera camComponent = aCam.GetComponent<Camera>();
        camComponent.targetDisplay = 0;
        //camComponent.targetDisplay = properties.playerNumber;

    }
	
	void Update () {
        Vector3 cam = new Vector3(0, 0, 0);
        cam = new Vector3(target.position.x, target.position.y, target.position.z + 15f);
        aCam.transform.position = cam;
    }
}
