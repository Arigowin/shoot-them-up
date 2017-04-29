using UnityEngine;

public class ShotScript : MonoBehaviour
{
	public int damage = 1;
	public bool isEnemyShot = false;
	public Vector2 speed = new Vector2 (10, 10);
	protected bool paused = false;

	void OnPauseGame ()
	{
		paused = true;
	}

	void OnResumeGame ()
	{
		paused = false;
	}

	void Start ()
	{
		Destroy (gameObject, 3);
	}

	void FixedUpdate ()
	{
		if (!paused)
			GetComponent<Rigidbody2D> ().velocity = speed;
	}
}