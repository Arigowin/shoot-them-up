using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public int spawnCount;
	public GameObject minion;
	private float rate = 1f;
	private float cooldown;
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
		cooldown = 0f;
	}

	void FixedUpdate ()
	{
		if (!paused) {
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			else
				Spawn ();
		}
	}

	public bool CanSpawn {
		get {
			return cooldown <= 0f;
		}
	}

	void Spawn ()
	{
		for (int i = 0; i < spawnCount; i++) {
			Vector3 spawnPosition = GetSpawnPosition ();

			if (spawnPosition != Vector3.zero)
				Instantiate (minion, spawnPosition, new Quaternion ());
		}
		cooldown = rate;
	}

	Vector3 GetSpawnPosition ()
	{
		Vector3 spawnPosition = new Vector3 ();
		float startTime = Time.realtimeSinceStartup;
		bool ok = false;

		while (ok == false) {
			Vector3 stageDimensions = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
			Vector2 spawnPositionRaw = Random.insideUnitCircle * stageDimensions.x;

			spawnPosition = new Vector3 (spawnPositionRaw.x, (stageDimensions.y) + 5, spawnPositionRaw.y);
			ok = !Physics.CheckSphere (spawnPosition, 0.75f);

			if (Time.realtimeSinceStartup - startTime > 0.5f) {
				Debug.Log ("Time out placing Minion!");
				return Vector3.zero;
			}
		}
		return spawnPosition;
	}
}