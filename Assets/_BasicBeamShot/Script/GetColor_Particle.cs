using UnityEngine;
using System.Collections;

public class GetColor_Particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem.MainModule ps = this.gameObject.GetComponent<ParticleSystem>().main;
		ps.startColor = this.transform.root.gameObject.GetComponent<BeamParam>().BeamColor;
		var size = ps.startSize;
		size.constant *= this.transform.root.gameObject.GetComponent<BeamParam>().Scale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
