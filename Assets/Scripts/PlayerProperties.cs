using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {

    private bool _moving = false;

    public string displayName
    {
        get;
        set;
    }

    public int playerNumber
    {
        get;
        set;
    }

    public bool isMoving
    {
        get
        {
            return _moving;
        }
        set
        {
            _moving = value;
        }
    }

    public bool isTurn
    {
        get;
        set;
    }

    public int strokes
    {
        get;
        set;
    }

    public int score
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
