using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	private WeaponScript[] weapons;
	private bool hasSpawn;
	private MoveScript moveScript;
	protected bool paused = false;

	void OnPauseGame ()
	{
		paused = true;
	}

	void OnResumeGame ()
	{
		paused = false;
	}

	void Awake ()
	{
		weapons = GetComponentsInChildren<WeaponScript> ();
		moveScript = GetComponent<MoveScript> ();
	}

	void Start ()
	{
		hasSpawn = false;
		GetComponent<Collider2D> ().enabled = false;
		moveScript.enabled = false;
		foreach (WeaponScript weapon in weapons)
			weapon.enabled = false;
	}

	void Update ()
	{
		if (!paused) {
			if (hasSpawn == false) {
				if (GetComponent<Renderer> ().IsVisibleFrom (Camera.main))
					Spawn ();
			} else {
				foreach (WeaponScript weapon in weapons) {
					if (weapon != null && weapon.enabled && weapon.CanAttack) {
						weapon.isEnemy = true;
						weapon.Attack ();
					}
				}
				if (GetComponent<Renderer> ().IsVisibleFrom (Camera.main) == false)
					Destroy (gameObject);
			}
		}
	}

	private void Spawn ()
	{
		hasSpawn = true;
		GetComponent<Collider2D> ().enabled = true;
		moveScript.enabled = true;
		foreach (WeaponScript weapon in weapons)
			weapon.enabled = true;
	}

	void OnDestroy ()
	{
	}
}
