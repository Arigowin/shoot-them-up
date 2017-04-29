using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Vector2 speed = new Vector2 (10, 10);
	private Vector2 default_speed;
	private Vector2 movement;
	private int[] lvl = { 0, 2, 4, 6 };
	protected bool paused = false;

	void OnPauseGame ()
	{
		paused = true;
	}

	void OnResumeGame ()
	{
		paused = false;
	}

	void OnEnable ()
	{
		EventManager.StartListening ("Left_down", left_fct);
		EventManager.StartListening ("Left_up", reset);
		EventManager.StartListening ("Right_down", right_fct);
		EventManager.StartListening ("Right_up", reset);
		EventManager.StartListening ("Up_down", up_fct);
		EventManager.StartListening ("Up_up", reset);
		EventManager.StartListening ("Down_down", down_fct);
		EventManager.StartListening ("Down_up", reset);
		EventManager.StartListening ("Shift_down", shift_fct);
		EventManager.StartListening ("Shift_up", shift_up_fct);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("Left_down", left_fct);
		EventManager.StopListening ("Left_up", reset);
		EventManager.StopListening ("Right_down", right_fct);
		EventManager.StopListening ("Right_up", reset);
		EventManager.StopListening ("Up_down", up_fct);
		EventManager.StopListening ("Up_up", reset);
		EventManager.StopListening ("Down_down", down_fct);
		EventManager.StopListening ("Down_up", reset);
		EventManager.StopListening ("Shift_down", shift_fct);
		EventManager.StopListening ("Shift_up", shift_up_fct);
	}

	void Start ()
	{
		default_speed = speed;

		WeaponScript[] weapons = GetComponentsInChildren<WeaponScript> ();
		foreach (WeaponScript weapon in weapons)
			weapon.enabled = false;
	}

	void Update ()
	{
		if (!paused) {
			var dist = (transform.position - Camera.main.transform.position).z;
			var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
			var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
			var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
			var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;

			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, leftBorder, rightBorder), Mathf.Clamp (transform.position.y, topBorder, bottomBorder), transform.position.z);

			ActiveWeapon ();
		}
	}

	void ActiveWeapon()
	{
		LevelScript lvlScript = GetComponentInParent<LevelScript> ();
		if (lvlScript.level <= 3) {
			WeaponScript[] weapons = GetComponentsInChildren<WeaponScript> ();

			int nb = lvl [lvlScript.level];

			for (int i = 0; i <= nb && i < 6; i++) {
				weapons [i].enabled = true;
			}
		}
	}

	void left_fct ()
	{
		Vector2 direction = new Vector2 (-1, 0);
		move (direction);
	}

	void right_fct ()
	{
		Vector2 direction = new Vector2 (1, 0);
		move (direction);
	}

	void up_fct ()
	{
		Vector2 direction = new Vector2 (0, 1);
		move (direction);
	}

	void down_fct ()
	{
		Vector2 direction = new Vector2 (0, -1);
		move (direction);
	}

	void move (Vector2 direction)
	{
		Vector2 vel = GetComponent<Rigidbody2D> ().velocity;
		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		GetComponent<Rigidbody2D> ().velocity = movement;
	}

	void reset ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	}

	void shift_fct ()
	{
		speed = new Vector2 (speed.x / 2, speed.y / 2);
	}

	void shift_up_fct ()
	{
		speed = default_speed;
		reset ();
	}

	void OnDestroy()
	{
		EventManager.TriggerEvent ("GameOver");
	}
}
