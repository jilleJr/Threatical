using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Objects")]
	public Tank tank;

	[Header("Settings")]
	public LayerMask raycastLayer;

	void Update() {
		Movement ();
		Raycast ();
		Shoot ();
	}

	void Movement() {
		// Read input
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		
		// Move forward/backward
		tank.MoveBody (vertical);
		
		// Rotate. If moving backwards the rotation is inverted
		tank.RotateBody (vertical>0 ? horizontal : -horizontal);
	}

	void UpdateTurret(RaycastHit hit) {
		tank.RotateTurretTowards(hit.point);
	}

	void Shoot() {
		// Read input
		bool fire = Input.GetButtonDown ("Fire");

		if (fire) {
			// Fire shell from this tank
			Shell.FireShell(tank);
		}
	}

	void Raycast() {
		// Raycast mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, raycastLayer)) {
			// Call raycast hit handlers
			UpdateTurret (hit);
		}
	}
}
