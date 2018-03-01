using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour {

    public static int totalPlayers = 2;
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        for (int i = 1; i <= totalPlayers; i++)
        {
            string playerID = "Player" + i;
            GameObject player = Instantiate(playerPrefab);
            player.name = playerID;

            PlayerProperties properties = player.GetComponent<PlayerProperties>();
            properties.playerID = playerID;
            properties.playerNumber = i;

            GameObject playerBody = GameObject.Find("Player");
            playerBody.name = playerID + "Body";

            GameObject playerCam = GameObject.Find("Player Camera");
            playerCam.name = playerID + "Cam";

            GameObject canvas = GameObject.Find("Canvas");
            canvas.name = playerID + "Canvas";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
