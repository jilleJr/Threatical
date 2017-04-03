using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FactoryTimer : MonoBehaviour {

	public List<UnitFactory> factories = new List<UnitFactory>();

	public float timeLeft = 30;
	public float waveTime = 90;

	private Text text;

#if UNITY_EDITOR
	private void OnValidate() {
		waveTime = Mathf.Max(waveTime, 0.01f);
	}
#endif

	private void Awake() {
		text = GetComponent<Text>();
	}

	private void Update() {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			foreach (var fac in factories)
				if (fac != null)
					fac.SpawnUnit();
			timeLeft += waveTime;
			waveTime = Mathf.Max(waveTime-1, 30);
		}
		
		text.text = FormatSeconds();
	}

	public string FormatSeconds() {
		int minutes = Mathf.Min(Mathf.FloorToInt(timeLeft / 60), 99);
		int seconds = Mathf.FloorToInt(timeLeft) % 60;

		return '.'
			+ (minutes < 10 ? '0' + minutes.ToString() : minutes.ToString())
			+ '.'
			+ (seconds < 10 ? '0' + seconds.ToString() : seconds.ToString())
			+ '.';
	}

}
