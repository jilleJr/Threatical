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

	private void Start() {
		// Start off un-reloaded
		reloaded = reloadDelay;
	}

	void Update() {
		// No target or if its out of range, then find a new target
		if (target == null || !InFiringRange(target))
			target = GetNearestBody ();
		else if (!(target is Tank)) {
			var doubleCheck = GetNearestBody();
			if (doubleCheck is Tank)
				target = doubleCheck;
		}

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

				bool yesAnyway = 
					// No nearest to begin with
					nearestBody == null
					// Nearest isn't a tank, but this one is :o
				||	(body is Tank && !(nearestBody is Tank))
				;

				if (yesAnyway || distance < nearestDist) {
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
