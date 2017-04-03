using UnityEngine;
using System.Collections;

public class GeroBeamHit : MonoBehaviour {
	private GameObject ParticleA;
	private GameObject ParticleB;
	private GameObject HitFlash;
	
	private float PatA_rate;
	private float PatB_rate;

	private ParticleSystem PatA;
	private ParticleSystem PatB;
    public Color col;
	public void SetViewPat(bool b)
	{
		if(b) {
			SetEmission(PatA, PatA_rate);
			SetEmission(PatB, PatB_rate);

			HitFlash.GetComponent<Renderer>().enabled = true;
		}else {
			SetEmission(PatA, 0);
			SetEmission(PatB, 0);
			HitFlash.GetComponent<Renderer>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
        col = new Color(1, 1, 1);
		ParticleA = transform.FindChild("GeroParticleA").gameObject;
		ParticleB = transform.FindChild("GeroParticleB").gameObject;
		HitFlash = transform.FindChild("BeamFlash").gameObject;
		PatA = ParticleA.gameObject.GetComponent<ParticleSystem>();
		PatB = ParticleB.gameObject.GetComponent<ParticleSystem>();
		PatA_rate = PatA.emission.rateOverTime.constant;
		SetEmission(PatA, 0);
		PatB_rate = PatB.emission.rateOverTime.constant;
		SetEmission(PatB, 0);


		HitFlash.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		var mainA = PatA.main;
		mainA.startColor = col;
		var mainB = PatB.main;
		mainB.startColor = col;
        HitFlash.GetComponent<Renderer>().material.SetColor("_Color", col*1.5f);
    }

	private static void SetEmission(ParticleSystem ps, float constant) {
		var rate = ps.emission.rateOverTime;
		rate.constant = constant;
		var em = ps.emission;
		em.rateOverTime = rate;
	}
}
