using UnityEngine;

public class MoveScript : MonoBehaviour {

	public Vector2 speed = new Vector2(10, 10);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;
	protected bool paused = false;

	void OnPauseGame ()
	{
		paused = true;
	}

	void OnResumeGame ()
	{
		paused = false;
	}

	void Update()
	{
		if (!paused)
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
	}

	void FixedUpdate()
	{
		if (!paused)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}
}
