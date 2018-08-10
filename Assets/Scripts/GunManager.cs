using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour{

	private GameObject gun;
	private static GameObject ball;
	private static Camera ballCam;
	private GameObject gunGO;
	Vector3 gunPos = new Vector3(0,0,0);

	public static GunManager Constructor(GameObject player, Camera cam) {
		GunManager gunMan = player.AddComponent<GunManager>();

		ball = player;
		ballCam = cam;

		return gunMan;
	}

	private void Start() {
		gunGO = (GameObject)Resources.Load("Gun", typeof(GameObject));
		gun = Instantiate(gunGO);
		gun.transform.parent = ball.transform;

		Physics.IgnoreCollision(gun.GetComponent<Collider>(), ball.GetComponent<Collider>());

		// var map = GameObject.Find("Tutorial Map");
		// var floor = map.transform.GetChild(1).gameObject;
		// var hole = floor.transform.GetChild(0).gameObject;
		// Physics.IgnoreCollision(gun.GetComponent<Collider>(), floor.GetComponent<Collider>());
		// Physics.IgnoreCollision(gun.GetComponent<Collider>(), hole.GetComponent<Collider>());
	}

	public void Update() {
		Vector3 mousePos;

		mousePos = ballCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, ballCam.transform.position.y, 0));
		AngleCalculations(ball.transform.position, mousePos);
	}

    private void AngleCalculations(Vector3 origin, Vector3 mouse) {
        float angle;
        float radius = 1f;   

        float xDiff = mouse.x - origin.x;
        float zDiff = mouse.z - origin.z;

        angle = Mathf.Atan2(zDiff, xDiff);

        gunPos.x = radius * Mathf.Cos(angle);
        gunPos.z = radius * Mathf.Sin(angle);
        
        gun.transform.position = origin + gunPos;
	}

    public void FireProjectile() {
        Rigidbody rigid;
		float bulletSpeed = 2000;
        var bullet = Instantiate(gunGO, gun.transform.position, gun.transform.rotation);
		bullet.transform.localScale = new Vector3(15f, 15f, 15f);
        rigid = bullet.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet.GetComponent<Collider>());
        rigid.AddForce(bulletSpeed * gunPos.x, 0,  bulletSpeed * gunPos.z);
    }
}
