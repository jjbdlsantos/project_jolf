using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public Rigidbody rb;
    public float appliedForce = 500;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 2000;

    // Use this for initialization
    void Start () {
        Debug.Log("Hello World");
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float ballX = gameObject.GetComponent<Transform>().position.x;
        float ballY = gameObject.GetComponent<Transform>().position.y;
        //movement(ballX, ballY);
        /*
        if (Input.GetMouseButtonDown(0))
        {
            rigidForce();
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE");
            FireBullet();
        }
    }

    private void FireBullet()
    {
        Rigidbody rigid;
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(currentMousePos * 100);
        
    }

    private void aimQuadrant()
    {

    }


    private void movement(float x, float y)
    {
        float bx = x + 0.01f;
        float by = y + 0.01f;
        gameObject.GetComponent<Transform>().position = new Vector3(bx, by,0);
    }
    
    private void rigidForce()
    {
        rb.AddForce(appliedForce, appliedForce, 0, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            //Debug.Log("WALL");
        }
    }
    
}
