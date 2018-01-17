using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForce : MonoBehaviour {

    //reference to LineRenderer component
    private LineRenderer line;
    //car to store mouse position on the screen
    private Vector3 mousePos;
    private Vector3 startLinePos;
    private Vector3 endLinePos;

    void Update()
    {
        //Create new Line on left mouse click(down)
        if (Input.GetMouseButtonDown(0))
        {
            //check if there is no line renderer created
            if (line == null)
            {
                //create the line
                createLine();
            }
            //get the mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startLinePos = mousePos;
            //set the z co ordinate to 0 as we are only interested in the xy axes
            mousePos.z = 0;
            //set the start point and end point of the line renderer
            line.SetPosition(0, mousePos);
            line.SetPosition(1, mousePos);
        }
        //if line renderer exists and left mouse button is click exited (up)
        else if (Input.GetMouseButtonUp(0) && line)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endLinePos = mousePos;
            mousePos.z = 0;
            //set the end point of the line renderer to current mouse position
            line.SetPosition(1, mousePos);
            
            Destroy(line);
            
            Debug.Log("Start: " + startLinePos + ". End: " + endLinePos);
            Debug.Log("Magnitude: " + (endLinePos - startLinePos).magnitude);
            scalarCalculation(startLinePos, endLinePos);
        }
        //if mouse button is held clicked and line exists
        else if (Input.GetMouseButton(0) && line)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            //set the end position as current position but dont set line as null as the mouse click is not exited
            line.SetPosition(1, mousePos);
        }
    }

    private void applyForce(float xForce, float yForce)
    {
        Rigidbody rb;
        var ball = GameObject.Find("player");
        rb = ball.GetComponent<Rigidbody>();
        Debug.Log("xForce: " + xForce + " yForce: " + yForce);
        rb.AddForce(xForce, yForce, 0, ForceMode.Force);
    }

    //method to create line
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.GetComponent<LineRenderer>().startColor = Color.white;
        line.GetComponent<LineRenderer>().endColor = Color.white;
        line.sortingLayerName = "Foreground";

        //set the number of points to the line
        line.positionCount = 2;
        //set the width
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;
    }

    public float multiplier = 100;
    private void scalarCalculation(Vector3 start, Vector3 end)
    {
        float x1 = start.x;
        float x2 = end.x;
        float y1 = start.y;
        float y2 = end.y;

        float xForce = (Mathf.Abs(x1 - x2)) * multiplier;
        float yForce = (Mathf.Abs(y1 - y2)) * multiplier;


        if(x1 > x2 && y1 > y2)
        {
            //+x +y
            applyForce(xForce, yForce);
        }
        else if(x1 > x2 && y1 < y2)
        {
            //+x -y
            applyForce(xForce, -yForce);
        }
        else if(x1 < x2 && y1 < y2)
        {
            //-x -y
            applyForce(-xForce, -yForce);
        }
        else if(x1 < x2 && y1 > y2)
        {
            //-x +y
            applyForce(-xForce, yForce);
        }
    }
}
