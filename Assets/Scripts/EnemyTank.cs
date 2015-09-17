using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour {

	[Header("Objects")]
	public Tank tank;

	[Header("Settings")]
	public float stoppingRange;
	public float firingRange; // GET IT?
	public float reloadDelay;

	private Body target;
	private bool inStoppingRange;
	private bool inFiringRange;
	private float reloaded;

#if UNITY_EDITOR
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, stoppingRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, firingRange);
	}
#endif

	void Update() {
		if (target == null)
			FindTarget ();

		Reload ();

		if (target) {
			float distance = Vector3.Distance (target.transform.position, transform.position);
			inStoppingRange = distance <= stoppingRange;
			inFiringRange = distance <= firingRange;

			if (!inStoppingRange) {
				tank.MoveBodyTowards (target);
			}

			if (inFiringRange) {
				Shoot();
			}
			
			tank.RotateTurretTowards (target);
		}
	}

	public void FindTarget() {
		PlayerController player = FindObjectOfType<PlayerController> ();
		if (player) {
			target = player.tank;
		}
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
