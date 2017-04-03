using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Objects")]
	public Tank tank;
	public Follow cameraFollow;
	public Animator reloadAnim;

	[Header("Settings")]
	public LayerMask raycastLayer;

	private const float shootCooldown = 3.9f;
	private bool canShoot;

	private void Start() {
		StartCoroutine(WaitForReload());
	}

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
		tank.RotateBody (Input.GetAxisRaw("Vertical")>=0 ? horizontal : -horizontal);
	}

	void Shoot() {
		// Read input
		bool fire = Input.GetButtonDown ("Fire");

		if (fire && canShoot) {
			// Fire shell from this tank
			Shell.FireShell(tank);
			StartCoroutine(WaitForReload());
		}
	}

	IEnumerator WaitForReload() {
		canShoot = false;
		reloadAnim.SetTrigger("Reload");
		EffectsController.AudioTankReload(transform);
		yield return new WaitForSeconds(shootCooldown);
		canShoot = true;
	}

	void Raycast() {
		// Raycast mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, raycastLayer)) {
			// Call raycast hit handlers
			tank.RotateTurretTowards(hit.point);
			if (cameraFollow)
				cameraFollow.lookAt = hit.point;
		} else if (cameraFollow) {
			cameraFollow.lookAt = transform.position;
		}
	}
}
