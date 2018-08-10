using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	public GameObject player;
	public Vector3 ballPosition;
	private GameObject playerGO;
	private GameObject ball;
	private Camera ballCam;
	private PhysicsManager physMan;
	private GunManager gunMan;

	private void Start() {
		playerGO = Instantiate(player);

		ball = playerGO.transform.GetChild (0).gameObject;
		ball.name = "P1";
		ball.transform.position = ballPosition;

		ballCam = playerGO.transform.GetChild (1).gameObject.GetComponent<Camera>();
		ballCam.name = "P1Cam";

		physMan = new PhysicsManager();
		gunMan = GunManager.Constructor(ball, ballCam);

		SetCamera();
	}

	private void Update() {
		SetCamera();
		physMan.ApplyForce(ball, physMan.DrawLine(ballCam));
		if (IsBallMoving()) {
			if ((Input.GetMouseButtonDown(0))) {
				gunMan.FireProjectile();
			}
		}
		IsSunk();
	}

	private void SetCamera() {
		 ballCam.transform.position = ball.transform.position + new Vector3(0,10f,0);
	}

	private bool IsSunk() {      
		RaycastHit hitInfo;
		if (Physics.Raycast(ball.transform.position, new Vector3(0, 0, -180), out hitInfo, 1)) {
			string RayTile = hitInfo.transform.gameObject.name;
			if (RayTile == "Hole") {
				print("You Win");
				return true;
			}
		}
		return false;
    }

	private bool IsBallMoving() {
		Rigidbody rb;
		rb = ball.GetComponent<Rigidbody>();		
		float speed = rb.velocity.magnitude;
		if (speed < 0.3) {
			rb.velocity = new Vector3(0, 0, 0);
			print("Stopped Moving");
			return false;
		}
		return true;
    }

	private class PhysicsManager {
		private static Vector3 EMPTY_VEC3 = new Vector3(0,0,0);
		private LineRenderer mouseLine;
		private Vector3 mousePos, startPos, endPos;
		private static Vector3 lineOffset;
		private float forceMultiplier;

		public PhysicsManager() {
			mousePos = EMPTY_VEC3;
			startPos = EMPTY_VEC3;
			endPos = EMPTY_VEC3;
			lineOffset = new Vector3(0,5f,0);
			forceMultiplier = 100f;
		}

		public void CreateLineObject() {
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

		public Vector3 DrawLine(Camera cam) {
			if (Input.GetMouseButtonDown(0)) {
				if (mouseLine == null) {
					CreateLineObject();
				}
				SetMousePosition(cam, 0);
				mouseLine.SetPosition(1, mousePos);
				startPos = mousePos;
			}

			else if (Input.GetMouseButton(0) && mouseLine) {
				SetMousePosition(cam, 1);
			}

			else if (Input.GetMouseButtonUp(0) && mouseLine) {
				SetMousePosition(cam, 1);
				endPos = mousePos;
				Destroy(mouseLine);

				return ScalarCalculation(startPos, endPos);
			}
			return EMPTY_VEC3;
		}

		private void SetMousePosition(Camera cam, byte index) {
			mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - lineOffset;
			mouseLine.SetPosition(index, mousePos);
		}

		private Vector3 ScalarCalculation(Vector3 start, Vector3 end) {
			float xDiff, zDiff, xSign, zSign, xForce, zForce; 

			xDiff = Mathf.Abs(start.x - end.x);
			zDiff = Mathf.Abs(start.z - end.z);

			xSign = (xDiff != 0) ? (start.x - end.x) / xDiff : 1;  
			zSign = (zDiff != 0) ? (start.z - end.z) / zDiff : 1;  

			xForce = xSign * xDiff * forceMultiplier;
			zForce = zSign * zDiff * forceMultiplier;

			return new Vector3(xForce, 0, zForce);
    	}

		public void ApplyForce(GameObject ball, Vector3 force) {
			Rigidbody rb;
			rb = ball.GetComponent<Rigidbody>();
			if (!float.IsNaN(force.x) && !float.IsNaN(force.z)) {
				rb.AddForce(force.x, force.y, force.z, ForceMode.Force);
			}
		}
	}
}
