﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
	public Vector2 speed = new Vector2 (2, 2);
	public Vector2 direction = new Vector2 (-1, 0);
	public bool isLinkedToCamera = false;
	public bool isLooping = false;
	private List<Transform> backgroundPart;
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
		if (isLooping) {
			backgroundPart = new List<Transform> ();

			for (int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild (i);

				if (child.GetComponent<Renderer> () != null)
					backgroundPart.Add (child);
			}

			backgroundPart = backgroundPart.OrderBy (t => t.position.y).ToList ();
		}
	}

	void Update ()
	{
		if (!paused) {
			Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);

			movement *= Time.deltaTime;
			transform.Translate (movement);

			if (isLinkedToCamera)
				Camera.main.transform.Translate (movement);

			if (isLooping) {
				Transform firstChild = backgroundPart.FirstOrDefault ();

				if (firstChild != null) {
					if (firstChild.position.y < Camera.main.transform.position.y) {
						if (firstChild.GetComponent<Renderer> ().IsVisibleFrom (Camera.main) == false) {
							Transform lastChild = backgroundPart.LastOrDefault ();
							Vector3 lastPosition = lastChild.transform.position;
							Vector3 lastSize = (lastChild.GetComponent<Renderer> ().bounds.max - lastChild.GetComponent<Renderer> ().bounds.min);

							firstChild.position = new Vector3 (firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);

							backgroundPart.Remove (firstChild);
							backgroundPart.Add (firstChild);
						}
					}
				}
			}
		}
	}
}