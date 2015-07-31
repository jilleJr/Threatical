using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour {

	[Header("Objects")]
	public Tank tank;

	[Header("Settings")]
	public float range;

	private Body target;

	void Update() {
		if (target == null)
			FindTarget ();

		if (target) {
			if (Vector3.Distance (target.transform.position, transform.position) > range) {
				tank.MoveBodyTowards (target);
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

}
