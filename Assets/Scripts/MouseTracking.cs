using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracking : MonoBehaviour {

    private Transform spawnerTransform;
    private Vector3 mousePos;

    private float rotateSpeed = 5f;
    private float radius = 0.6f;
    private Vector3 _centre;
    private float _angle;
    private float lockZRotation = 0;

    public float angle;

	// Use this for initialization
	void Start () {
        spawnerTransform = GetComponent<Transform>();
        _centre = transform.localPosition;
        angle = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //raw input mouse position (0,0 @ lower left corner)
        Vector3 pos = Input.mousePosition;
        //converts input.mouseposition into position relative to main camera
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float a1 = Mathf.Atan2(transform.position.y - mousePos.y, transform.position.x - mousePos.x) * 180 / Mathf.PI;
        
        _angle = -(90f + a1) * Mathf.PI/180;

        var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle), 0) * radius;
        transform.localPosition = _centre + offset;
        
        
    }
}
