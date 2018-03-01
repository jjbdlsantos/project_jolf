using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPhysics : MonoBehaviour {

    private GameObject gun;
    private GameObject playerCam;
    private GameObject playerController;
    private PlayerProperties properties;
    private string playerID;
    private int bullets = 0;


    private void Start()
    {
        GameObject parent = this.gameObject.transform.parent.gameObject;
        properties = parent.GetComponent<PlayerProperties>();
        playerID = properties.playerID;

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        playerCam = GameObject.Find(playerID + "Cam");
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;


    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find(playerID + "Body");
        Vector3 mousePos;

        if (gun == null)
        {
            // Create gun
            gun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gun.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Physics.IgnoreCollision(gun.GetComponent<Collider>(), player.GetComponent<Collider>());
        }

        mousePos = playerCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, playerCam.transform.position.z));

        AngleCalculations(player.transform.position, mousePos);

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, gun.transform.position);
        lineRenderer.SetPosition(1, mousePos);


        if (properties.isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireProjectile();
                GameObject canvas = GameObject.Find(playerID + "Canvas");
                GameObject text = canvas.transform.GetChild(1).gameObject;
                Text moreText = text.GetComponent<Text>();
                moreText.text = "" + bullets++;
            }
        }
    }

    Vector3 gunPos = new Vector3(0, 0, 0);
    private void AngleCalculations(Vector3 origin, Vector3 mouse)
    {
        float angle;
        float radius = 1f;   

        float xDiff = mouse.x - origin.x;
        float yDiff = mouse.y - origin.y;
        angle = Mathf.Atan2(yDiff, xDiff);

        gunPos.x = radius * Mathf.Cos(angle);
        gunPos.y = radius * Mathf.Sin(angle);
        
        gun.transform.position = origin + gunPos;
        //Debug.Log("Angle: " + angle);
        //Debug.Log("Origin: " + origin + " GunPos: " + gunPos + " Added: " + gun.transform.position);
    }
    
    public GameObject bulletPrefab;
    public float bulletSpeed = 2000;

    private void FireProjectile()
    {
        Rigidbody rigid;
        var bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
        rigid = bullet.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet.GetComponent<Collider>());
        rigid.AddForce(bulletSpeed * gunPos.x, bulletSpeed * gunPos.y, 0);
    }

}
