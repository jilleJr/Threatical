using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public bool ignoreX;
	public bool ignoreY;
	public bool ignoreZ;
	private Vector3 offset;

	void Start() {
		offset = target.position - transform.position;
	}

	void Update() {
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
		}
	}

}
