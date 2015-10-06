using UnityEngine;
using System.Collections;

/// <summary>
/// Game faction. Is the unit of team red? Team blue? This is the main variable to keep track of it.
/// </summary>
public enum Team { Blue, Red };

public class Body : MonoBehaviour {

	[Header("Body variables")]
	public int health = 1;
	public int maxHealth = 1;
	public Team team;
	public BoxCollider mainCollider;

	private bool _dead;
	public bool dead {
		get { return _dead; }
		set { if (!_dead && value) Die ();
			_dead = value; }
	}

	#if UNITY_EDITOR
	void OnValidate() {
		health = Mathf.Clamp(health, 0, maxHealth);
		maxHealth = Mathf.Max(maxHealth, 0);
	}
	#endif

	public virtual void Die() {
		Destroy (gameObject);
	}

	protected virtual void HealthChange(int delta) {
		health = Mathf.Clamp (health + delta, 0, maxHealth);

		if (health == 0) {
			dead = true;
		}
	}

	public virtual void Damage(int amount) {
		HealthChange (-amount);
	}

	public virtual void Heal(int amount) {
		HealthChange (amount);
	}

	public static float GetDistance(Body A, Body B) {
		Vector3 ACenter = A.transform.position + A.mainCollider.center;
		Vector3 BCenter = B.transform.position + B.mainCollider.center;

		return Vector3.Distance(ACenter, BCenter);
	}
}
