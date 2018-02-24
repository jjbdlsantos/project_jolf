using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    public float multiplier = 100f;
    private LineRenderer mouseLine;
    private Vector3 mousePos;
    private Vector3 startPos = new Vector3(0, 0, 0);
    private Vector3 endPos = new Vector3(0, 0, 0);
    private PlayerProperties properties;

    private void Start()
    {
        properties = GameObject.Find("Player Controller").GetComponent<PlayerProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        properties.isMoving = isPlayerMoving();
        if (!properties.isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseLine == null)
                {
                    CreateLine();
                }

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                startPos = mousePos;

                mouseLine.SetPosition(0, mousePos);
                mouseLine.SetPosition(1, mousePos);
            }

            else if (Input.GetMouseButton(0) && mouseLine)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                mouseLine.SetPosition(1, mousePos);
            }

            else if (Input.GetMouseButtonUp(0) && mouseLine)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
                endPos = mousePos;

                mouseLine.SetPosition(1, mousePos);

                //Debug.Log("Start: " + startPos + " End: " + endPos);
                ScalarCalculation(startPos, endPos);
                Destroy(mouseLine);
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

    private void ApplyForce(float xForce, float yForce)
    {
        Rigidbody rb;
        var player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        Debug.Log("xForce: " + xForce + " zForce: " + yForce);
        rb.AddForce(xForce, yForce, 0, ForceMode.Force);
    }

    private void ScalarCalculation(Vector3 start, Vector3 end)
    {
        //calculating total force 
        float xDiff = Mathf.Abs(start.x - end.x);
        float yDiff = Mathf.Abs(start.y - end.y);

        float xSign = (start.x - end.x) / xDiff;
        float ySign = (start.y - end.y) / yDiff;

        //Debug.Log("xSign: " + xSign + " ySign: " + ySign);

        ApplyForce((xSign * xDiff * multiplier), (ySign * yDiff * multiplier));
    }

    private bool isPlayerMoving()
    {
        Rigidbody rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        float speed = rb.velocity.magnitude;
        if(speed < 0.3)
        {
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log("PLAYER IS NOT MOVING");
            return false;
        }
        Debug.Log("PLAYER IS MOVING");
        return true;
    }
}
