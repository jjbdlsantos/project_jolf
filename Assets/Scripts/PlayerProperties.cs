using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {

    private bool _moving = false;
    private string _playedID;
    private GameObject[] players;
    private GameObject playerCamera;
    private GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    public string playerID
    {
        get
        {
            return _playedID;
        }
        set
        {
            _playedID = value;
        }
    }

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
}
