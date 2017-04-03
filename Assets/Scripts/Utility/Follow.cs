using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public bool ignoreX;
	public bool ignoreY;
	public bool ignoreZ;
	private Vector3 offset;
	[System.NonSerialized]
	public Vector3 lookAt;
	private Vector3 _lookAt;
	public float farthestLookRadius = 50;

	void Start() {
		offset = target.position - transform.position;
		_lookAt = target.position;
	}

	void LateUpdate() {
		if (target) {
			Vector3 pos = transform.position;
			Vector3 newPos = target.position - offset;

			if (!ignoreX)
				pos.x = newPos.x;
			if (!ignoreY)
				pos.y = newPos.y;
			if (!ignoreZ)
				pos.z = newPos.z;

			transform.position = pos;
			
			lookAt.y = target.position.y;
			lookAt = Vector3.ClampMagnitude(lookAt - target.position, farthestLookRadius) + target.position;
			_lookAt = Vector3.Lerp(_lookAt, Vector3.Lerp(target.position, lookAt, (lookAt - target.position).magnitude / farthestLookRadius), Time.deltaTime);

			transform.LookAt(_lookAt);
		}
	}

}
