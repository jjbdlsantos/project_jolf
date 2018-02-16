using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPhysics : MonoBehaviour {

    private GameObject gun;


    private void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        Vector3 mousePos;

        if (gun == null)
        {
            // Create gun
            gun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gun.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Physics.IgnoreCollision(gun.GetComponent<Collider>(), player.GetComponent<Collider>());
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        Debug.Log("Current Mouse Position: " + mousePos);

        AngleCalculations(player.transform.position, mousePos);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, gun.transform.position);
        lineRenderer.SetPosition(1, mousePos);
        
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
        Debug.Log("Angle: " + angle);
        Debug.Log("Origin: " + origin + " GunPos: " + gunPos + " Added: " + gun.transform.position);
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
