using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour {

    public static int totalPlayers = 2;
    public GameObject playerPrefab;
    public static PlayerProperties[] multiplayers;
    private static int currentPlayer = 0;

	void Start () {
        multiplayers = new PlayerProperties[totalPlayers];
        for (int i = 1; i <= totalPlayers; i++)
        {
            string playerID = "Player" + i;
            GameObject player = Instantiate(playerPrefab);
            player.name = playerID;

            PlayerProperties properties = player.GetComponent<PlayerProperties>();
            properties.playerID = playerID;
            properties.playerNumber = i;
            properties.isTurn = false;

            GameObject playerBody = GameObject.Find("Player");
            playerBody.name = playerID + "Body";

            GameObject playerCam = GameObject.Find("Player Camera");
            playerCam.name = playerID + "Cam";
            Camera camComponent = playerCam.GetComponent<Camera>();
            camComponent.enabled = false;

            GameObject canvas = GameObject.Find("Canvas");
            canvas.name = playerID + "Canvas";

            multiplayers[i-1] = properties;
        }
        PlayerProperties p = multiplayers[currentPlayer];
        p.isTurn = true;
	}
	
	void Update () {
		bool nextTurn = true;
		PlayerProperties player;
		foreach (PlayerProperties p in multiplayers)
		{
			if (p.isTurn)
			{
				nextTurn = false;
			} 
		} 
		if (nextTurn)
		{
			currentPlayer = (currentPlayer + 1) % totalPlayers;
			player = multiplayers[currentPlayer];
			player.isTurn = true;
		}
	}
}
