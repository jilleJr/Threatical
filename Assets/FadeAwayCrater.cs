using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayCrater : MonoBehaviour {

	public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(5, 1));
	public float dieAfter = 5;

	private Vector3 start;
	private float time;

	private void Start() {
		start = transform.position;
		time = Time.time;
		Destroy(gameObject, dieAfter);
	}

	void Update () {
		transform.position = start - transform.forward * curve.Evaluate(Time.time - time);
	}
}
