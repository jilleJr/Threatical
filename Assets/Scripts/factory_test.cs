using UnityEngine;
using System.Collections;

public class factory_test : MonoBehaviour {

	public Animator anim;
	public bool building;

	void OnValidate() {
		if (anim != null && Application.isPlaying)
			anim.SetBool ("Building", building);
	}
}
