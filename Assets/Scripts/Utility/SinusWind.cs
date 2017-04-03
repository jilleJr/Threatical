using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WindZone))]
public class SinusWind : MonoBehaviour {

	public float highest = 5;
	public float lowest = .5f;
	public float waveLength = 5;

	WindZone zone;

#if UNITY_EDITOR
	private void OnValidate() {
		lowest = Mathf.Min(lowest, highest);
		highest = Mathf.Max(lowest, highest);
	}
#endif

	private void Awake() {
		zone = GetComponent<WindZone>();
	}

	void Update () {
		zone.windMain = Mathf.Lerp(lowest, highest, .5f + .5f * Mathf.Sin(Time.time * 2 * Mathf.PI / waveLength));

	}
}
