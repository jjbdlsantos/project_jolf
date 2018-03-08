using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPhysics : MonoBehaviour {

    public float multiplier = 100f;
    private LineRenderer mouseLine;
    private Vector3 mousePos;
    private Vector3 startPos = new Vector3(0, 0, 0);
    private Vector3 endPos = new Vector3(0, 0, 0);
    private PlayerProperties properties;
    private string playerID;
    private Camera aCam;
    private bool isShot = false;

    private void Start()
    {
        properties = this.gameObject.GetComponent<PlayerProperties>();
        playerID = properties.playerID;

        aCam = GameObject.Find(playerID + "Cam").GetComponent<Camera>();
    }

    void Update()
    {
        properties.isMoving = IsPlayerMoving();
        if (!properties.isMoving && properties.isTurn && !isShot)
        {
            aCam.enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseLine == null)
                {
                    CreateLine();
                }

                mousePos = aCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                startPos = mousePos;

                mouseLine.SetPosition(0, mousePos);
                mouseLine.SetPosition(1, mousePos);
            }

            else if (Input.GetMouseButton(0) && mouseLine)
            {
                mousePos = aCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                mouseLine.SetPosition(1, mousePos);
            }

            else if (Input.GetMouseButtonUp(0) && mouseLine)
            {
                mousePos = aCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                endPos = mousePos;

                mouseLine.SetPosition(1, mousePos);
                Destroy(mouseLine);

                isShot = ScalarCalculation(startPos, endPos);
            }
        }
        else if (properties.isTurn && isShot)
        {
            if (!IsPlayerMoving())
            {
                properties.isTurn = false;
                isShot = false;
                aCam.enabled = false;
            }
            else
            {
                isSunk();
            }
        }

    }

    private void CreateLine()
    {
        mouseLine = new GameObject("Line").AddComponent<LineRenderer>();
        mouseLine.material = new Material(Shader.Find("Particles/Additive"));
        mouseLine.GetComponent<LineRenderer>().startColor = Color.red;
        mouseLine.GetComponent<LineRenderer>().endColor = Color.red;
        mouseLine.sortingLayerName = "Foreground";

        mouseLine.positionCount = 2;
        mouseLine.startWidth = 0.05f;
        mouseLine.endWidth = 0.05f;
        mouseLine.useWorldSpace = true;
    }

    private bool ApplyForce(float xForce, float yForce)
    {
        Rigidbody rb;
        var player = GameObject.Find(playerID + "Body");
        rb = player.GetComponent<Rigidbody>();
        Debug.Log("xForce: " + xForce + " zForce: " + yForce);
        if (!float.IsNaN(xForce) && !float.IsNaN(yForce))
        {
            rb.AddForce(xForce, yForce, 0, ForceMode.Force);
            return true;
        }
        return false;
    }

    private bool ScalarCalculation(Vector3 start, Vector3 end)
    {
        float xDiff = Mathf.Abs(start.x - end.x);
        float yDiff = Mathf.Abs(start.y - end.y);

        float xSign = (start.x - end.x) / xDiff;
        float ySign = (start.y - end.y) / yDiff;

        return ApplyForce((xSign * xDiff * multiplier), (ySign * yDiff * multiplier));
    }

    private bool IsPlayerMoving()
    {
        Rigidbody rb = GameObject.Find(playerID + "Body").GetComponent<Rigidbody>();
        float speed = rb.velocity.magnitude;
        if(speed < 0.3)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return false;
        }
        return true;
    }

    private bool isSunk()
    {
        GameObject player = GameObject.Find(playerID + "Body");
        RaycastHit hitInfo;
        if (Physics.Raycast(player.transform.position, new Vector3(0, 0, -180), out hitInfo, 1))
        {
            string RayTile = hitInfo.transform.gameObject.name;
            if (RayTile == "Hole")
            {
                print("You win");
                return true;
            }
        }
        return false;
    }
}
