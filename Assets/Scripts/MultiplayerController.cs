using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerController : MonoBehaviour {

    public int totalPlayers = 2;
    public GameObject playerPrefab;
    private Transform aTransform;
    private int spawnPosition = 37;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting game...");
        for (int i = 1; i <= totalPlayers; i++)
        {
            GameObject player = Instantiate(playerPrefab);
            player.name = "PlayerGroup" + i;

            GameObject playerObject = GameObject.Find("Player");
            playerObject.name = "Player" + i;
            playerObject.transform.localPosition = new Vector3(spawnPosition, 4, 20);

            GameObject playerCamObject = GameObject.Find("Player Camera");
            playerCamObject.name = "Player" + i + "Cam";

            GameObject playerControllerObject = GameObject.Find("Player Controller");
            playerControllerObject.name = "Player" + i + "Controller";

            spawnPosition += 3;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
