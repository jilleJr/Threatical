using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	[Header("Objects")]
	public Rigidbody rbody;
	public TrailRenderer trail;

	[Header("Settings")]
	public Team team;
	public float velocity;
	public int damage;

	private bool _dead;
	public bool dead {
		get { return _dead; }
		set { if (!_dead && value) Die ();
			_dead = value; }
	}

	void OnTriggerEnter(Collider other) {
		// Get the gameobject of the collider. If it has a rigidbody get the gameobject of that one instead
		GameObject main = other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : other.gameObject;

		if (main.tag == "Terrain") {
			dead = true;
		} else if (main.tag == "Tank") {
			Tank tank = main.GetComponent<Tank>();

			if (tank.team != team) {
				tank.Damage(damage);
				dead = true;
			}
		}
	}

	void Die() {
		Destroy (gameObject);

		if (trail != null)
			trail.transform.parent = null;
	}

	public static void FireShell(GameObject prefab, Vector3 position, Quaternion rotation) {
		// Create object from prefab
		GameObject clone = Instantiate (prefab, position, rotation) as GameObject;

		// Add force via the Shell scripts reference to the rigidbody
		Shell script = clone.GetComponent<Shell> ();
		script.rbody.AddRelativeForce (Vector3.forward * script.velocity, ForceMode.Impulse);
	}

	public static void FireShell(Tank tank) {
		FireShell (tank.tankShellPrefab, tank.tankGunEnd.transform.position, tank.GetTurretRotation());
		if (tank.cannonParticles != null)
			tank.cannonParticles.Play ();
	}

}
