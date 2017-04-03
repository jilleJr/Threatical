using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour {

	private static EffectsController i;

	[Header("Visual effects")]
	public GameObject v_explosion;
	public GameObject v_toonExplosion;
	public GameObject v_firework;
	public List<GameObject> v_craters = new List<GameObject>();

	[Header("Audio effects")]
	public GameObject a_mortarRound;
	public GameObject a_tankFiring;
	public GameObject a_gunShot;
	public GameObject a_tankReload;

	private void Awake() {
		i = this;
	}

	public static void VisualExplosion(Vector3 location, float minRadius = 12, float maxRadius = 15) {
		VisualExplosion(location, Random.Range(minRadius, maxRadius));
	}

	public static void VisualExplosion(Vector3 location, float radius) {
		GameObject clone = Instantiate(i.v_explosion, location, Random.rotation);
		clone.transform.localScale = Vector3.one * radius;
		Destroy(clone, 3);
	}

	public static void VisualToonExplosion(Vector3 location, float minRadius = 8, float maxRadius = 12) {
		VisualToonExplosion(location, Random.Range(minRadius, maxRadius));
	}

	public static void VisualToonExplosion(Vector3 location, float radius) {
		GameObject clone = Instantiate(i.v_toonExplosion, location, Random.rotation);
		clone.transform.localScale = Vector3.one * radius;
		Destroy(clone, 3);
	}

	public static void VisualFirework(Vector3 location, float minRadius = 3, float maxRadius = 5) {
		VisualFirework(location, Random.Range(minRadius, maxRadius));
	}

	public static void VisualFirework(Vector3 location, float radius) {
		GameObject clone = Instantiate(i.v_firework, location, Random.rotation);
		clone.transform.localScale = Vector3.one * radius;
		Destroy(clone, 3);
	}


	public static void VisualCrater(Vector3 location, Vector3 normal) {
		Instantiate(i.v_craters[Random.Range(0, i.v_craters.Count)], location + normal * 10, Quaternion.LookRotation(-normal));
	}

	public static void VisualCrater(Vector3 location, Vector3 normal, LayerMask ignoreLayers) {
		GameObject clone = Instantiate(i.v_craters[Random.Range(0, i.v_craters.Count)], location + normal * 10, Quaternion.LookRotation(-normal));
		Projector proj = clone.GetComponent<Projector>();
		proj.ignoreLayers = ignoreLayers;
	}


	public static void AudioMortarRound(Vector3 location) {
		GameObject clone = Instantiate(i.a_mortarRound, location, Quaternion.identity);
		Destroy(clone, 5);
	}
	public static void AudioMortarRound(Transform parent) {
		GameObject clone = Instantiate(i.a_mortarRound, parent.position, parent.rotation);
		clone.transform.SetParent(parent, true);
		Destroy(clone, 5);
	}

	public static void AudioTankFiring(Vector3 location) {
		GameObject clone = Instantiate(i.a_tankFiring, location, Quaternion.identity);
		Destroy(clone, 5);
	}
	public static void AudioTankFiring(Transform parent) {
		GameObject clone = Instantiate(i.a_tankFiring, parent.position, parent.rotation);
		clone.transform.SetParent(parent, true);
		Destroy(clone, 5);
	}

	public static void AudioGunShot(Vector3 location) {
		GameObject clone = Instantiate(i.a_gunShot, location, Quaternion.identity);
		Destroy(clone, 5);
	}
	public static void AudioGunShot(Transform parent) {
		GameObject clone = Instantiate(i.a_gunShot, parent.position, parent.rotation);
		clone.transform.SetParent(parent, true);
		Destroy(clone, 5);
	}

	public static void AudioTankReload(Vector3 location) {
		GameObject clone = Instantiate(i.a_tankReload, location, Quaternion.identity);
		Destroy(clone, 5);
	}
	public static void AudioTankReload(Transform parent) {
		GameObject clone = Instantiate(i.a_tankReload, parent.position, parent.rotation);
		clone.transform.SetParent(parent, true);
		Destroy(clone, 5);
	}

}
