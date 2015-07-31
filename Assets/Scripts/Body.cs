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

	private bool _dead;
	public bool dead {
		get { return _dead; }
		set { if (!_dead && value) Die ();
			_dead = value; }
	}

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
}
