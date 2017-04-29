using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
	bool pause = false;

	void OnPauseGame ()
	{
		pause = true;
	}

	void OnResumeGame ()
	{
		pause = false;
	}

	void Update ()
	{
		if (pause == false) {
			// key Down
			if (Input.GetKeyDown (KeyCode.LeftArrow))
				EventManager.TriggerEvent ("Left_down");
			if (Input.GetKeyDown (KeyCode.RightArrow))
				EventManager.TriggerEvent ("Right_down");
			if (Input.GetKeyDown (KeyCode.UpArrow))
				EventManager.TriggerEvent ("Up_down");
			if (Input.GetKeyDown (KeyCode.DownArrow))
				EventManager.TriggerEvent ("Down_down");

			// key Up
			if (!(Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow))) {
				if (Input.GetKeyUp (KeyCode.LeftArrow))
					EventManager.TriggerEvent ("Left_up");
				if (Input.GetKeyUp (KeyCode.RightArrow))
					EventManager.TriggerEvent ("Right_up");
				if (Input.GetKeyUp (KeyCode.UpArrow))
					EventManager.TriggerEvent ("Up_up");
				if (Input.GetKeyUp (KeyCode.DownArrow))
					EventManager.TriggerEvent ("Down_up");
			}

			// Special Key
			if (Input.GetKey (KeyCode.Space))
				EventManager.TriggerEvent ("Space");
			if (Input.GetKeyDown (KeyCode.LeftShift))
				EventManager.TriggerEvent ("Shift_down");
			if (Input.GetKeyUp (KeyCode.LeftShift))
				EventManager.TriggerEvent ("Shift_up");

			if (Input.GetKey (KeyCode.Escape)) {
				pause = true;
				EventManager.TriggerEvent ("Pause");
			}
		}
	}
}