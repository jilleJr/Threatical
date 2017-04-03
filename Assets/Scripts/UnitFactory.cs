using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnitFactory : Building {

	[Header("Factory fields")]
	public GameObject particlesParent;
	public GameObject unitPrefab;
	
	public void SpawnUnit() {
		StartCoroutine(SpawnRoutine());
	}

	private IEnumerator SpawnRoutine() {
		yield return new WaitForSeconds(Random.Range(0f, 3f));

		foreach (var part in particlesParent.GetComponentsInChildren<ParticleSystem>()) {
			var em = part.emission;
			em.enabled = false;

			var sub = part.subEmitters;
			sub.enabled = false;
		}

		Instantiate(unitPrefab, transform.position, transform.rotation);

		yield return new WaitForSeconds(4);

		foreach (var part in particlesParent.GetComponentsInChildren<ParticleSystem>()) {
			var em = part.emission;
			em.enabled = true;

			var sub = part.subEmitters;
			if (sub.subEmittersCount > 0)
				sub.enabled = true;
		}
	}

}
