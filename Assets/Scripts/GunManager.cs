using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour{
	private GameObject gun;
	private static GameObject ball;
	private static Camera ballCam;
	private GameObject gunGO;
	private Vector3 gunPos = new Vector3(0,0,0);
	private static float orbitRadius = 0.5f;

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
	}

	public void Update() {
		Vector3 mousePos = ballCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, ballCam.transform.position.y, 0));
		AngleCalculations(ball.transform.position, mousePos);
	}

    private void AngleCalculations(Vector3 origin, Vector3 mouse) {
        float radius = 1f;   

        float xDiff = mouse.x - origin.x;
        float zDiff = mouse.z - origin.z;

        float angle = Mathf.Atan2(zDiff, xDiff);

        gunPos.x = radius * Mathf.Cos(angle);
        gunPos.z = radius * Mathf.Sin(angle);
        
        gun.transform.position = origin + gunPos;
	}

    public void FireProjectile() {
		float bulletVelocity = 2000;
        var bullet = Instantiate(gunGO, gun.transform.position, gun.transform.rotation);

		Physics.IgnoreCollision(gun.GetComponent<Collider>(), bullet.GetComponent<Collider>());
		bullet.transform.localScale = new Vector3(15f, 15f, 15f);

		Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletVelocity * gunPos.x, 0,  bulletVelocity * gunPos.z);
    }
}
