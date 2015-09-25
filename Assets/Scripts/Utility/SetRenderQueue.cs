using UnityEngine;
using System.Collections;

[AddComponentMenu("Effects/SetRenderQueue")]

public class SetRenderQueue : MonoBehaviour {
	
	public Renderer ren;
	public int queue = 1;

	public int[] queues;
	
	protected void Start() {
		SetQueue ();
	}

#if UNITY_EDITOR
	void OnValidate() {
		SetQueue ();
	}
#endif

	void SetQueue() {
		if (!ren || !ren.sharedMaterial || queues == null)
			return;
		ren.sharedMaterial.renderQueue = queue;
		for (int i = 0; i < queues.Length && i < ren.sharedMaterials.Length; i++)
			ren.sharedMaterials[i].renderQueue = queues[i];
	}
	
}