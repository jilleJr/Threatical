using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour {

	[Header("Objects")]
	public Tank tank;

	[Header("Settings")]
	public float stoppingRange;
	public float firingRange; // GET IT?
	public float reloadDelay;

	private Body target;
	private float reloaded;

#if UNITY_EDITOR
	void OnDrawGizmosSelected() {
		Vector3 center;

		if (tank != null) {
			if (tank.mainCollider)
				center = tank.transform.position + tank.mainCollider.center;
			else
				center = tank.transform.position;
		} else
			center = transform.position;

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(center, stoppingRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(center, firingRange);
	}
#endif

	void Update() {
		// No target or if its out of range, then find a new target
		if (target == null || InFiringRange(target))
			target = GetNearestBody ();

		Reload ();

		if (target) {
			bool inStoppingRange = InStoppingRange(target);
			bool inFiringRange = InFiringRange(target);

			if (!inStoppingRange) {
				tank.MoveBodyTowards (target);
			}

			if (inFiringRange) {
				Shoot();
			}
			
			tank.RotateTurretTowards (target);
		}
	}

	// Find the nearest target
	public Body GetNearestBody() {
		// Get the nearest enemy body
		Body nearestBody = null;
		float nearestDist = 0;

		foreach (var body in FindObjectsOfType<Body>()) {
			// Friend or foe?
			if (body.team != tank.team) {

				float distance = Body.GetDistance(tank, body);

				if (nearestBody == null) {
					// Set the first one as nearest for others to have something to compare to
					nearestBody = body;
					nearestDist = distance;
				} else if (distance < nearestDist) {
					// Found a body thats even closer...
					nearestBody = body;
					nearestDist = distance;
				}
			}
		}

		return nearestBody;
	}

	public bool InFiringRange(Body other) {
		return Body.GetDistance(tank, other) <= firingRange;
	}

	public bool InStoppingRange(Body other) {
		return Body.GetDistance(tank, other) <= stoppingRange;
	}

	void Shoot() {
		if (reloaded <= 0) {
			// Reload complete
			reloaded = reloadDelay;
			Shell.FireShell(tank);
		}
	}

	void Reload() {
		if (reloaded > 0) {
			// Reloading
			reloaded -= Time.deltaTime;
		}
	}

}
