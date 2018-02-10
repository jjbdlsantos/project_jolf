using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPhysics : MonoBehaviour {

    private GameObject gun;

	// Update is called once per frame
	void Update () {

        var player = GameObject.Find("Player");
        Vector3 mousePos;

        if (gun == null)
        {
            // Create gun
            gun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gun.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));

        AngleCalculations(player.transform.position, mousePos);
    }

    private void AngleCalculations(Vector3 origin, Vector3 mouse)
    {
        Vector3 gunPos = new Vector3(0,0,0);
        float radius = 1f;
        float angle;

        float xDiff = mouse.x - origin.x;
        float yDiff = mouse.y - origin.y;
        angle = Mathf.Atan2(yDiff, xDiff);

        gunPos.x = radius * Mathf.Cos(angle);
        gunPos.y = radius * Mathf.Sin(angle);

        gun.transform.position = origin + gunPos;
    }
}
