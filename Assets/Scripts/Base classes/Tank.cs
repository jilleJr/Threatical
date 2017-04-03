using UnityEngine;
using System.Collections;

public class Tank : Body {

	[Header("Objects")]
	public GameObject tankBase;
	public GameObject tankTurret;
	public GameObject tankGunEnd;
	public GameObject tankShellPrefab;
	public Rigidbody rbody;

	[Header("Settings")]
	public float rotationSpeed;
	public float movementSpeed;
	public float reverseSpeed;
	public float turretSpeed;
	public float turretAngleOffset;

	public override void Die() {
		// Move all meshes away
		foreach (var filter in GetComponentsInChildren<MeshFilter>()) {
			if (!filter.GetComponent<MeshRenderer>()) continue;
			filter.transform.SetParent(null, true);
			filter.gameObject.layer = 1; // TransparentFX

			var col = filter.gameObject.AddComponent<MeshCollider>();
			col.sharedMesh = filter.sharedMesh;
			col.convex = true;

			var rb = filter.gameObject.AddComponent<Rigidbody>();
			rb.AddExplosionForce(5, transform.position, 15);

			Destroy(filter.gameObject, Random.Range(2f, 5f));
		}

		EffectsController.VisualExplosion(transform.position);
		EffectsController.AudioMortarRound(transform.position);
		base.Die();
	}

	#region GetTurretRotation
	/// <summary>
	/// Get the rotation of the turret with the <c>turretAngleOffset</c> in mind.
	/// </summary>
	/// <returns>The turret rotation.</returns>
	public Quaternion GetTurretRotation() {
		Vector3 euler = tankTurret.transform.eulerAngles;
		euler.y = euler.y - turretAngleOffset;
		return Quaternion.Euler (euler);
	}
	#endregion

	#region MoveTurretTowards
	public void RotateTurretTowards(float angle) {
		Vector3 euler = tankTurret.transform.localEulerAngles;
		euler.y = Mathf.MoveTowardsAngle (euler.y, angle - rbody.rotation.eulerAngles.y, turretSpeed * Time.deltaTime);
		tankTurret.transform.localEulerAngles = euler;

		//tankTurret.transform.rotation = Quaternion.Slerp (tankTurret.transform.rotation, angle, turretSpeed * Time.deltaTime);
	}

	public void RotateTurretTowards(Vector3 target) {
		// Calculate angle towards target
		Vector3 delta = target - transform.position;
		if (Mathf.Approximately(delta.magnitude, 0f)) delta = tankBase.transform.forward;

		//RotateTurretTowards (Mathf.Atan2(delta.x,delta.z) * Mathf.Rad2Deg);
		RotateTurretTowards (Quaternion.LookRotation (delta, tankBase.transform.up).eulerAngles.y);
	}

	public void RotateTurretTowards(Transform target) {
		RotateTurretTowards (target.position);
	}

	public void RotateTurretTowards(Body target) {
		RotateTurretTowards (target.transform.TransformPoint(target.mainCollider.center));
	}
	#endregion

	#region MoveBodyTowards
	public void MoveBodyTowards(Vector3 target) {
		// Rotate towards target
		RotateBodyTowards (target);

		// Move forward towards target
		MoveBodyForward ();
	}

	public void MoveBodyTowards(Transform target) {
		MoveBodyTowards (target.position);
	}

	public void MoveBodyTowards(Body target) {
		MoveBodyTowards (target.transform);
	}
	#endregion

	#region RotateBodyTowards
	public void RotateBodyTowards(float angle) {
		Vector3 euler = rbody.rotation.eulerAngles;
		euler.y = Mathf.MoveTowardsAngle (euler.y, angle, rotationSpeed * Time.fixedDeltaTime);

		//Vector3 euler = Vector3.zero;
		//euler.y = Mathf.MoveTowardsAngle (euler.y, angle - rbody.rotation.eulerAngles.y, rotationSpeed * Time.fixedDeltaTime);

		//rbody.angularVelocity = euler;
		//rbody.AddRelativeTorque (euler * rotationSpeed);
		rbody.MoveRotation (Quaternion.Euler (euler));
	}

	public void RotateBodyTowards(Vector3 target) {
		// Calculate angle towards target
		Vector3 delta = target - transform.position;
		RotateBodyTowards (Mathf.Atan2 (delta.x, delta.z) * Mathf.Rad2Deg);
	}
	#endregion

	#region RotateBody
	/// <summary>
	/// <para>Rotates the body of the tank object.</para>
	/// <para></para>
	/// <para>/<paramref name="direction"/>/ value goes from <c>-1</c> to <c>1</c>, where <c>-1</c> represents left and <c>1</c> represents right.</para>
	/// <para></para>
	/// <para>Example of /<paramref name="direction"/>/: A value of <c>0.5</c> would move right at 1/2 of the max speed, and a value of <c>-0.25</c> would move left at 1/4 of the max speed.</para>
	/// </summary>
	/// <seealso cref="Tank.RotateBodyTowards"/>
	/// <param name="direction"></param>
	public void RotateBody(float direction) {
		Vector3 euler = rbody.rotation.eulerAngles;
		euler.y += rotationSpeed * Time.deltaTime * Mathf.Clamp (direction, -1f, 1f);
		rbody.MoveRotation (Quaternion.Euler (euler));
	}
	#endregion

	#region MoveBody
	public void MoveBody(float amount) {
		float speed = (amount > 0 ? movementSpeed : reverseSpeed) * Mathf.Clamp (amount, -1f, 1f);
		//rbody.velocity = transform.forward * speed * Time.fixedDeltaTime;
		//rbody.AddForce (transform.forward * speed * Time.deltaTime);
		rbody.MovePosition (rbody.position + transform.forward * speed * Time.deltaTime);
	}
	#endregion

	#region MoveBodyForward
	public void MoveBodyForward() {
		MoveBody (1f);
	}
	#endregion

	#region MoveBodyBackward
	public void MoveBodyBackward() {
		MoveBody (-1f);
	}
	#endregion
}
